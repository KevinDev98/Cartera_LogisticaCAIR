using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LogisticaCAIR.Context;
using LogisticaCAIR.Models;
using ClosedXML.Excel;
using System.Data;
using System.IO;
using System.Data.Common;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Microsoft.Data.SqlClient;

namespace LogisticaCAIR.Controllers
{
    public class CobroClientesController : Controller
    {
        DB_LOGISTICA_CAIRContext bd = new DB_LOGISTICA_CAIRContext();
        public static string NameParam;
        public static int NumerDoc;
        public static List<T> EjecutaSentencia<T>(string query, Func<DbDataReader, T> map)
        {
            using (var context = new DB_LOGISTICA_CAIRContext())
            {
                using (var command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    context.Database.OpenConnection();

                    using (var result = command.ExecuteReader())
                    {
                        var entities = new List<T>();

                        while (result.Read())
                        {
                            entities.Add(map(result));
                        }

                        return entities;
                    }
                }
            }
        }
        //public IActionResult Index()
        //{
        //    var conect = new DB_LOGISTICA_CAIRContext();
        //    //var data = bd.CobroClientes.ToList();
        //    var data = bd.ViInfoFacturaClientes.ToList().OrderBy(dataindex => dataindex.NumDoc).ToList();
        //    return View("../CobrosPagos/ListCobrosClientes", data);
        //}
        //public async Task<IActionResult> Index(string sortOrder, string searchString)
        public async Task<IActionResult> Index(string DateInit, string DateEnd, string Name)
        {
            //ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["ParamFechaInicio"] = DateInit; //== "Date" ? "date_Init" : "Date";
            ViewData["ParamFechaVencimiento"] = DateEnd; // == "Date" ? "date_End" : "Date";
            ViewData["Name"] = Name;

            TempData["DateInit"] = DateInit;
            TempData["DateFin"] = DateEnd;
            NameParam = Name;

            var data = bd.ViInfoFacturaClientes.ToList().OrderBy(dataindex => dataindex.NumDoc).ToList();
            var datafilter = from s in data
                             select s;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    datafilter = datafilter.Where(s => s.Clentes.Contains(searchString)
            //                           || s.TipoMoneda.Contains(searchString)
            //                           || s.NumDoc.ToString().Contains(searchString)
            //                           || s.Estatus.Contains(searchString)
            //                           );
            //}

            if (!String.IsNullOrEmpty(DateInit) && !String.IsNullOrEmpty(DateEnd))
            {
                DateInit = DateInit.Replace("-", "/");
                DateInit = Convert.ToDateTime(DateInit, null).ToShortDateString();
                DateEnd = DateEnd.Replace("-", "/");
                DateEnd = Convert.ToDateTime(DateEnd, null).ToShortDateString();
                ViewData["ParamFechaInicio"] = DateInit;
                ViewData["ParamFechaVencimiento"] = DateEnd;

                datafilter = from filt in data
                             where Convert.ToDateTime(filt.FechaFinal) >= Convert.ToDateTime(DateInit)
     && Convert.ToDateTime(filt.FechaFinal) <= Convert.ToDateTime(DateEnd)
                             select filt;
            }
            if (!String.IsNullOrEmpty(NameParam))
            {
                datafilter = from filt in datafilter
                             where filt.Clentes.ToUpper().Contains(NameParam.ToUpper())
                             select filt;
            }
            DataTable dtable = new DataTable();
            try
            {
                if ((!String.IsNullOrEmpty(DateInit) && !String.IsNullOrEmpty(DateEnd)) || (!String.IsNullOrEmpty(NameParam)))
                {
                    if (String.IsNullOrEmpty(NameParam))
                    {
                        NameParam = "";
                    }
                    if ((String.IsNullOrEmpty(DateInit) && String.IsNullOrEmpty(DateEnd)))
                    {
                        DateInit = "";
                        DateEnd = "";
                    }
                    else
                    {
                        DateInit = DateInit.Replace("-", "/");
                        DateInit = Convert.ToDateTime(DateInit, null).ToShortDateString();
                        DateEnd = DateEnd.Replace("-", "/");
                        DateEnd = Convert.ToDateTime(DateEnd, null).ToShortDateString();
                        ViewData["ParamFechaInicio"] = DateInit;
                        ViewData["ParamFechaVencimiento"] = DateEnd;
                    }
                    //var DataFound = bd.ViInfoFacturaClientes.FromSqlRaw("EXEC dbo.SP_BUSCA_FACTURAS {0}",
                    //                                                    new SqlParameter("@BANDERA", SqlDbType.Int).Value = 0,
                    //                                                    new SqlParameter("@FECHA1", SqlDbType.VarChar).Value = DateInit,
                    //                                                    new SqlParameter("@FECHA2", SqlDbType.VarChar).Value = DateEnd,
                    //                                                    new SqlParameter("@PARAMETRO", SqlDbType.VarChar).Value = NameParam);
                    SqlConnectionStringBuilder Conect = new SqlConnectionStringBuilder();
                    var context = new DB_LOGISTICA_CAIRContext();
                    context.Database.OpenConnection();
                    Conect.ConnectionString = context.Database.GetDbConnection().ConnectionString;
                    var conexion = Conect.ConnectionString;
                    conexion = conexion.Replace("ID=sa;", "ID=sa;Password=inginf98;");
                    SqlConnection ExecSP = (SqlConnection)context.Database.GetDbConnection();
                    SqlCommand ExecCommand = ExecSP.CreateCommand();
                    //ExecSP.Open();
                    ExecCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    ExecCommand.CommandText = "SP_BUSCA_FACTURAS";
                    ExecCommand.Parameters.Add("@BANDERA", SqlDbType.Int).Value = 0;
                    if ((!String.IsNullOrEmpty(DateInit) && !String.IsNullOrEmpty(DateEnd)))
                    {
                        ExecCommand.Parameters.Add("@FECHA1", SqlDbType.Date).Value = DateInit;
                        ExecCommand.Parameters.Add("@FECHA2", SqlDbType.Date).Value = DateEnd;
                    }
                    ExecCommand.Parameters.Add("@PARAMETRO", SqlDbType.VarChar, 50).Value = NameParam;
                    dtable.TableName = "SP_BUSCA_FACTURAS";
                    ViInfoFacturaCliente DataFound = new ViInfoFacturaCliente();
                    try
                    {
                        dtable.Load(ExecCommand.ExecuteReader());
                    }
                    catch (Exception ex)
                    {
                        dtable.TableName = "Exception";
                        dtable.Columns.Add("Mensaje");
                        DataRow row = dtable.NewRow();
                        row["Mensaje"] = ex.Message;
                        dtable.Rows.Add(row);
                    }
                    IEnumerable<DataRow> query = (IEnumerable<DataRow>)dtable;
                    datafilter = (IEnumerable<ViInfoFacturaCliente>)query;
                    ExecSP.Close();
                }
            }
            catch (Exception)
            {

            }
            return View("../CobrosPagos/ListCobrosClientes", datafilter.ToList());
            //return View(await datafilter.ToListAsync());
        }
        public IActionResult Menu()
        {
            return View("../CobrosPagos/MenuClientes");
        }
        public IActionResult Create()
        {
            List<CatCliente> list = (  ///Modelo
                    from d in bd.CatClientes //Tabla 
                    select new CatCliente
                    {
                        CardCode = d.CardName, //Campos con los que se llenará el combo
                        CardName = d.CardName
                    }
                    ).ToList();
            List<SelectListItem> itemsC = list.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.CardName.ToString(),
                    Value = d.CardCode.ToString(),
                    Selected = false
                };
            }
             );
            ViewBag.FcIdclie = itemsC;
            List<Usuario> lista = ( //Modelo 
                    from d in bd.Usuarios//tabla
                    select new Usuario
                    {
                        IdUsuario = (int)d.IdUsuario, //Campos con los que se llenará el combo
                        NombreEmpleado = d.NombreEmpleado
                    }
                    ).ToList();
            List<SelectListItem> itemsU = lista.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.NombreEmpleado.ToString(),
                    Value = d.IdUsuario.ToString(),
                    Selected = false
                };
            }
             );
            ViewBag.Verificador = itemsU;
            //return View("../CobrosPagos/CreaPagoCliente");
            return View("../CobrosPagos/Factura_Clientes");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(CobroCliente CCliente)
        //{
        //    try
        //    {
        //        var getIdCliente = from CC in bd.CatClientes where CC.CardName == CCliente.Cliente select CC.CardCode;
        //        CCliente.Cliente = getIdCliente.FirstOrDefault();
        //        bd.Add(CCliente);
        //        await bd.SaveChangesAsync();
        //        ViewBag.Action = "Correcto";
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Action = "Incorrecto";
        //        TempData["ErrorCobroCliente"] = "Verificar los datos ingresados. " + ex.InnerException.ToString();
        //    }
        //    return View("../CobrosPagos/CreaPagoCliente");
        //}
        public async Task<IActionResult> Create(FacturasCliente CCliente)
        {
            try
            {
                var getIdCliente = from CC in bd.CatClientes where CC.CardName == CCliente.FcIdclie select CC.CardCode;
                var ValidaNumFacCair = from FC in bd.FacturasClientes where FC.FcNumFactCair == CCliente.FcNumFactCair select FC;
                if (ValidaNumFacCair.Count() > 0)
                {
                    ViewBag.Action = "Incorrecto";
                    TempData["ErrorCobroCliente"] = "Numero de factura ya existente";
                }
                else
                {
                    CCliente.FcIdclie = getIdCliente.FirstOrDefault();
                    bd.Add(CCliente);
                    await bd.SaveChangesAsync();
                    ViewBag.Action = "Correcto";
                }
                
            }
            catch (Exception ex)
            {
                ViewBag.Action = "Incorrecto";
                TempData["ErrorCobroCliente"] = "Verificar los datos ingresados. " + ex.InnerException.ToString();
            }
            return View("../CobrosPagos/Factura_Clientes");
        }
        // GET: Usuarios/Edit/5
        //[HttpGet("Usuarios/Detalle/DocNum")]
        [HttpGet]
        public async Task<IActionResult> Detalle(int DocNum)
        {
            try
            {
                if (DocNum == 0)
                {
                    DocNum = NumerDoc;
                }
                //var search1 = await bd.ViInfoFacturaClientes.FindAsync(DocNum);
                ViInfoFacturaCliente DataFound = new ViInfoFacturaCliente();
                var data = bd.ViInfoFacturaClientes.ToList();//.OrderBy(dataindex => dataindex.NumDoc).ToList();
                var datafilter = from s in data
                                 where s.NumDoc == DocNum
                                 select s;
                var search1 = await bd.ViInfoFacturaClientes.FirstOrDefaultAsync(data => data.NumDoc == DocNum); ///FindAsync recibe busca por primary key, firstordefault no Keylless
                return View("../CobrosPagos/VisualizaCobros", search1);
            }
            catch (Exception e)
            {

                throw;
            }

        }
        public IActionResult ExportarExcel()
        {
            //string excelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string DateInit = null;
            string DateEnd = null;
            if (TempData["DateInit"] != null && TempData["DateInit"] != null)
            {
                DateInit = TempData["DateInit"].ToString();
                DateEnd = TempData["DateFin"].ToString();
            }

            var CobroClientesData = bd.ViInfoFacturaClientes.ToList();
            var datafilter = from s in CobroClientesData
                             select s;
            if (!String.IsNullOrEmpty(DateInit) && !String.IsNullOrEmpty(DateEnd))
            {
                DateInit = DateInit.Replace("-", "/");
                DateInit = Convert.ToDateTime(DateInit, null).ToShortDateString();
                DateEnd = DateEnd.Replace("-", "/");
                DateEnd = Convert.ToDateTime(DateEnd, null).ToShortDateString();
                //CobroClientesData = (List<ViInfoFacturaCliente>)(from data in CobroClientesData select data);                
                datafilter = (from filt in datafilter
                              where Convert.ToDateTime(filt.FechaFinal) >= Convert.ToDateTime(DateInit)
                                      && Convert.ToDateTime(filt.FechaFinal) <= Convert.ToDateTime(DateEnd)
                              select filt);
                CobroClientesData = datafilter.ToList();
            }
            if (!String.IsNullOrEmpty(CobroClientesController.NameParam))
            {
                datafilter = from filt in datafilter
                             where filt.Clentes.ToUpper().Contains(NameParam.ToUpper())
                             select filt;
                CobroClientesData = datafilter.ToList();
            }
            Int32 cantcolumn = CobroClientesData.Count();
            DataTable dt = new DataTable("Pagos recibidos de clientes");
            dt.Columns.AddRange(new DataColumn[10] { new DataColumn("Número de factura"),
                                        new DataColumn("Cliente"),
                                        new DataColumn("Tipo de moneda"),
                                        new DataColumn("Subtotal"),
                                        new DataColumn("Iva"),
                                        new DataColumn("Retención"),
                                        new DataColumn("Monto total de pago"),
                                        new DataColumn("Fecha de Pago"),
                                        new DataColumn("Fecha de vencimiento"),
                                        new DataColumn("Estatus")
                                        });

            var PagosClie = datafilter;

            foreach (var data in PagosClie)
            {
                dt.Rows.Add(data.NumDoc, data.Clentes, data.TipoMoneda, data.SubTotal, data.Iva, data.Retencion, data.MontoTotal, data.FechaPago, data.FechaFinal, data.Estatus);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte de Pago de Clientes.xlsx");
                }
            }
        }
        public IActionResult ExportarPDF(ViInfoFacturaCliente Info)
        {
            try
            {
                string parametrobuscar="PDF_";
                try
                {//Actualiza facturas cuando inicia sesión
                    DataTable DataFactura = new DataTable();
                    var Ejecuta = new BD.ExecProcedures();
                    parametrobuscar = Convert.ToString(Info.NumDoc) + "-" + Info.Clentes;
                    DataFactura = Ejecuta.SP_BUSCA_FACTURAS(NameParam: parametrobuscar);
                    Info.FormaPago = DataFactura.Rows[0]["FormaPago"].ToString();
                    Info.TipoMoneda = DataFactura.Rows[0]["TipoMoneda"].ToString();
                    Info.MontoTotal = DataFactura.Rows[0]["MontoTotal"].ToString();
                    Info.Estatus = DataFactura.Rows[0]["Estatus"].ToString();
                    Info.SubTotal = DataFactura.Rows[0]["SubTotal"].ToString();
                }
                catch (Exception)
                {
                }
                // Creamos el documento con el tamaño de página tradicional
                Document doc = new Document(PageSize.LETTER);
                string folderPath = @"C:\DocumentoCAIR";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);

                }
                // Indicamos donde vamos a guardar el documento
                PdfWriter writer = PdfWriter.GetInstance(doc,
                                            new FileStream((folderPath + @"\Factura Cliente - " + parametrobuscar + ".pdf"), FileMode.Create));
                // Le colocamos el título y el autor
                // **Nota: Esto no será visible en el documento
                doc.AddTitle("Factura de clientes");
                doc.AddCreator("LogisticaCair");
                // Abrimos el archivo
                doc.Open();
                // Creamos el tipo de Font que vamos utilizar
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.ITALIC, BaseColor.BLACK);

                // Escribimos el encabezamiento en el documento
                doc.Add(new Paragraph(Info.Clentes));
                doc.Add(Chunk.NEWLINE);

                // Declaramos que se va agregar una tabla nueva
                PdfPTable tbllInfo1 = new PdfPTable(3);
                tbllInfo1.WidthPercentage = 100;

                // Configuramos el título de las columnas de la tabla
                PdfPCell DtePago = new PdfPCell(new Phrase("Fecha de pago", _standardFont));
                DtePago.BorderWidth = 0;
                DtePago.BorderWidthBottom = 0.75f;

                PdfPCell Uverifica = new PdfPCell(new Phrase("Forma de pago", _standardFont));
                Uverifica.BorderWidth = 0;
                Uverifica.BorderWidthBottom = 0.75f;

                PdfPCell RFC = new PdfPCell(new Phrase("Estatus", _standardFont));
                RFC.BorderWidth = 0;
                RFC.BorderWidthBottom = 0.75f;

                // Añadimos las celdas a la tabla
                tbllInfo1.AddCell(DtePago);
                tbllInfo1.AddCell(Uverifica);
                tbllInfo1.AddCell(RFC);

                // Llenamos la tabla con información
                DtePago = new PdfPCell(new Phrase(Info.FechaPago, _standardFont));
                DtePago.BorderWidth = 0;

                Uverifica = new PdfPCell(new Phrase(Info.FormaPago, _standardFont));
                Uverifica.BorderWidth = 0;

                RFC = new PdfPCell(new Phrase(Info.Estatus, _standardFont));
                RFC.BorderWidth = 0;

                // Añadimos las celdas a la tabla
                tbllInfo1.AddCell(DtePago);
                tbllInfo1.AddCell(Uverifica);
                tbllInfo1.AddCell(RFC);

                // Finalmente, añadimos la tabla al documento PDF y cerramos el documento
                doc.Add(tbllInfo1);

                doc.Add(Chunk.NEWLINE);


                // Declaramos que se va agregar una tabla nueva
                PdfPTable tbllInfo2 = new PdfPTable(2);
                tbllInfo2.WidthPercentage = 100;

                //// Configuramos el título de las columnas de la tabla
                //PdfPCell Concepto = new PdfPCell(new Phrase("Concepto a pagar", _standardFont));
                //Concepto.BorderWidth = 0;
                //Concepto.BorderWidthBottom = 0.50f;
                //Concepto.BorderWidthTop = 0.80f;

                PdfPCell Moneda = new PdfPCell(new Phrase("Moneda", _standardFont));
                Moneda.BorderWidth = 0;
                Moneda.BorderWidthBottom = 0.50f;
                Moneda.BorderWidthTop = 0.80f;

                PdfPCell Monto = new PdfPCell(new Phrase("Subtotal", _standardFont));
                Monto.BorderWidth = 0;
                Monto.BorderWidthBottom = 0.50f;
                Monto.BorderWidthTop = 0.80f;

                // Añadimos las celdas a la tabla
                //tbllInfo2.AddCell(Concepto);
                tbllInfo2.AddCell(Moneda);
                tbllInfo2.AddCell(Monto);

                //// Llenamos la tabla con información
                //Concepto = new PdfPCell(new Phrase(Info.ConceptoAPagar, _standardFont));
                //Concepto.BorderWidth = 0;

                Moneda = new PdfPCell(new Phrase(Info.TipoMoneda, _standardFont));
                Moneda.BorderWidth = 0;

                Monto = new PdfPCell(new Phrase(Info.SubTotal, _standardFont));
                Monto.BorderWidth = 0;

                // Añadimos las celdas a la tabla
                //tbllInfo2.AddCell(Concepto);
                tbllInfo2.AddCell(Moneda);
                tbllInfo2.AddCell(Monto);

                // Finalmente, añadimos la tabla al documento PDF y cerramos el documento
                doc.Add(tbllInfo2);

                doc.Add(Chunk.NEWLINE);

                //--------TABLA 3
                // Declaramos que se va agregar una tabla nueva
                PdfPTable tbllInfo3 = new PdfPTable(3);
                tbllInfo3.WidthPercentage = 100;

                // Configuramos el título de las columnas de la tabla
                PdfPCell IVA = new PdfPCell(new Phrase("IVA", _standardFont));
                IVA.BorderWidth = 0;
                IVA.BorderWidthBottom = 0.25f;
                IVA.BorderWidthTop = 0.80f;

                PdfPCell RETEN = new PdfPCell(new Phrase("Retención", _standardFont));
                RETEN.BorderWidth = 0;
                RETEN.BorderWidthBottom = 0.25f;
                RETEN.BorderWidthTop = 0.80f;

                PdfPCell TOTAL = new PdfPCell(new Phrase("Total Final", _standardFont));
                TOTAL.BorderWidth = 0;
                TOTAL.BorderWidthBottom = 0.25f;
                TOTAL.BorderWidthTop = 0.80f;
                // Añadimos las celdas a la tabla
                tbllInfo3.AddCell(IVA);
                tbllInfo3.AddCell(RETEN);
                tbllInfo3.AddCell(TOTAL);

                // Llenamos la tabla con información
                IVA = new PdfPCell(new Phrase(Info.Iva, _standardFont));
                IVA.BorderWidth = 0;

                RETEN = new PdfPCell(new Phrase(Info.Retencion, _standardFont));
                RETEN.BorderWidth = 0;

                TOTAL = new PdfPCell(new Phrase(Info.MontoTotal, _standardFont));
                TOTAL.BorderWidth = 0;

                // Añadimos las celdas a la tabla
                tbllInfo3.AddCell(IVA);
                tbllInfo3.AddCell(RETEN);
                tbllInfo3.AddCell(TOTAL);

                // Finalmente, añadimos la tabla al documento PDF y cerramos el documento
                doc.Add(tbllInfo3);

                doc.Close();
                writer.Close();
                NumerDoc = Info.NumDoc;
                ViewBag.Action = "Correcto";
                return RedirectToAction("Detalle", Info.NumDoc);
                //var data = bd.VI_INFO_COBRO_CLIENTES.ToList();
                //return View("../CobrosPagos/ListCobrosClientes", data);

            }
            catch (Exception ex)
            {
                ViewBag.Action = "Incorrecto";
                TempData["ErrorCobroCliente"] = "Error durante el proceso. " + ex.Message;
                var data = bd.ViInfoFacturaClientes.ToList().Take(100).ToList();
                return View("../CobrosPagos/ListCobrosClientes", data);
            }
        }
        public IActionResult Payment()
        {
            //return View("../CobrosPagos/CreaPagoCliente");
            return View("../CobrosPagos/PaymentLoad");
        }
        [HttpPost]
        public async Task<IActionResult> PaymentLoad([Bind] PaymentLoadModel fl)
        {
            string FileName = "";
            int correcto = 0;
            try
            {
                int factsubidas = 0;
                if (fl.Files != null)//Si recibe el archivo 
                {
                    ClaseImportData ccl = new ClaseImportData();
                    IFormFile[] file;
                    file = fl.Files;
                    string file_name = "";
                    for (int x = 0; x < file.Length; x++)
                    {
                        //correcto = correcto + 1;
                        file_name = file[x].FileName;
                        if (file_name.Contains("xlsx"))
                        {
                            correcto = 1;
                        }
                        else
                        {
                            correcto = 0;
                        }
                    }
                    for (int z = 0; z < file.Length; z++)
                    {
                        if (correcto == 1)//Valida la extension del archivo
                        {
                            string folderName = "Pagos_" + DateTime.Now.ToString("dd-MM-yyyy hh mm ssss");
                            folderName = folderName.Replace(" ", "");
                            var path_Root = "C:\\Cargas\\LogisticaCair\\Archivos";
                            var path2 = path_Root + "\\" + folderName;
                            if (!Directory.Exists(path2))
                                Directory.CreateDirectory(path2);//Crea carpeta donde se guardara temporalmente  el archivo

                            var y = file.Count();
                            string[] data = new string[y];

                            //for (int x = 0; x < file.Length; x++)
                            //{
                            path2 = path2 + "\\" + file[z].FileName;
                            var stream = new FileStream(path2, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                            file[z].CopyTo(stream);
                            data[z] = path2;

                            stream.Close();
                            //}
                           Boolean upload= ccl.InsertExceldataOk(path2); ///Carga el archivo y pasa a sql
                            //path2= path_Root + "\\" + folderName;
                            //path2 = path2.Replace("\\","/");
                            //Directory.Delete(path2);//Borra el archivo para que no guarde basura
                            if (upload)
                            {
                                ViewBag.Action = "Correcto";
                            }
                            else
                            {
                                ViewBag.Action = "Incorrecto";
                                TempData["ErrorCobroCliente"] = "Validar que el numero de columnas sea correcto, o que el numero de factura CAIR no se repita con otro ya existente en el sistema.";
                            }                           
                        }
                        else
                        {
                            ViewBag.Action = "Incorrecto";
                            TempData["ErrorCobroCliente"] = "Seleccionar solo archivos de excel";
                        }
                    }

                }
                else
                {
                    ViewBag.Action = "Incorrecto";
                    TempData["ErrorCobroCliente"] = "Seleccionar al menos un archivo de excel";
                }
                if (factsubidas == 1)
                {
                    TempData["ErrorCobroCliente"] = "Ocurrio un error durante el proceso, sin embargo se subieron algunas facturas, revisa el catalogo de facturas, para que valides cuales si se subieron.";
                }
                return View("../CobrosPagos/PaymentLoad");
                //return View();
            }
            catch (Exception e)
            {
                ViewBag.Action = "Incorrecto";
                if (correcto > 1)
                {
                    TempData["ErrorCobroCliente"] = "Seleccionar solo un archivo de excel";
                }
                else
                {
                    TempData["ErrorCobroCliente"] = "Error durante el proceso. " + e.Message.ToString();
                }

                return View("../CobrosPagos/PaymentLoad");
            }

        }
        [HttpPost]
        public async Task<IActionResult> UpdateEstatus(int NumDoc, string Clentes, string MontoTotal, string Estatus, string DtePago)
        {
            try
            {
                var Ejecuta = new BD.ExecProcedures();
                if (!String.IsNullOrEmpty(DtePago))
                {
                    DtePago = DtePago.Replace("-", "/");
                    DtePago = Convert.ToDateTime(DtePago, null).ToShortDateString();
                }
                Double MontoPagado = 0.0;
                if (!String.IsNullOrEmpty(MontoTotal))
                {
                    MontoTotal = MontoTotal.Replace("$", "");
                    MontoPagado = Convert.ToDouble(MontoTotal);
                }
                if (Estatus == "SI" && String.IsNullOrEmpty(DtePago))
                {
                    ViewBag.Action = "Incorrecto";
                    TempData["ErrorCobroCliente"] = "Seleccionar una fecha de pago";
                }
                else
                {
                    DataTable Update = new DataTable();
                    Update = Ejecuta.SP_ACTUALIZA_ESTATUS_FACTURAS(Bandera: 1, TF: "C", NumFact: NumDoc, Monto: MontoPagado, SC: Clentes, Estatus: Estatus, Fecha: DtePago);
                    string resultado = Update.Rows[0][0].ToString();
                    if (resultado == "OK")
                    {
                        ViewBag.Action = "Update Correcto";
                    }
                    else
                    {
                        ViewBag.Action = "Incorrecto";
                        TempData["ErrorCobroCliente"] = "Error durante el proceso. " + resultado;
                    }
                }


            }
            catch (Exception e)
            {
                ViewBag.Action = "Incorrecto";
                TempData["ErrorCobroCliente"] = "Error durante el proceso. " + e.Message.ToString();
            }
            var data = bd.ViInfoFacturaClientes.ToList().OrderBy(dataindex => dataindex.NumDoc).Take(100).ToList();
            return View("../CobrosPagos/ListCobrosClientes", data);
        }
        [HttpGet]
        public async Task<IActionResult> Borrar(int DocNum)
        {
            try
            {
                var Ejecuta = new BD.ExecProcedures();
                DataTable Delete = new DataTable();
                Delete = Ejecuta.SP_ACTUALIZA_ESTATUS_FACTURAS(Bandera: 2, TF: "C", NumFact: DocNum);
                string resultado = Delete.Rows[0][0].ToString();
                if (resultado == "OK")
                {
                    ViewBag.Action = "Update Correcto";
                }
                else
                {
                    ViewBag.Action = "Incorrecto";
                    TempData["ErrorCobroCliente"] = "Error durante el proceso. " + resultado;
                }
            }
            catch (Exception e)
            {

                ViewBag.Action = "Incorrecto";
                TempData["ErrorCobroCliente"] = "Error durante el proceso. " + e.Message.ToString();
            }
            var data = bd.ViInfoFacturaClientes.ToList().OrderBy(dataindex => dataindex.NumDoc).Take(100).ToList();
            return View("../CobrosPagos/ListCobrosClientes", data);

        }
    }
}
