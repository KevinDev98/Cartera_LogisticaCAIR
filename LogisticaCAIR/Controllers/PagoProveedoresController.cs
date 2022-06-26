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
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http;

namespace LogisticaCAIR.Controllers
{
    public class PagoProveedoresController : Controller
    {
        DB_LOGISTICA_CAIRContext bd = new DB_LOGISTICA_CAIRContext();
        public static string NameParam;
        public static int NumerDoc;
        public async Task<IActionResult> Index(string DateInit, string DateEnd, string Name)
        {
            var conect = new DB_LOGISTICA_CAIRContext();
            //context.Database.ExecuteSqlRaw("CreateStudents @p0, @p1", parameters: new[] { "Bill", "Gates" });
            //var data = bd.CobroClientes.ToList();
            //var data = bd.ViInfoPagoProvs.ToList();                      
            ViewData["ParamFechaInicio"] = DateInit; //== "Date" ? "date_Init" : "Date";
            ViewData["ParamFechaVencimiento"] = DateEnd; // == "Date" ? "date_End" : "Date";
            ViewData["Name"] = Name;

            TempData["DateInit"] = DateInit;
            TempData["DateFin"] = DateEnd;
            NameParam = Name;

            var data = bd.ViInfoFacturaProveedores.ToList().OrderBy(dataindex => dataindex.NumDoc).ToList();
            var datafilter = from s in data
                             select s;
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
                             where filt.Proveedor.ToUpper().Contains(NameParam.ToUpper())
                             select filt;
            }
            //var data = bd.ViInfoFacturaProveedores.ToList();
            return View("../CobrosPagos/ListaPagosProv", datafilter.ToList());
        }
        public IActionResult Menu()
        {
            return View("../CobrosPagos/MenuProveedores");
        }
        public IActionResult Create()
        {
            List<CatCliente> list = (  ///Modelo
                    from d in bd.CatProveedores //Tabla 
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
            ViewBag.FpIdprov = itemsC;
            List<Usuario> lista = ( //Modelo 
                    from d in bd.Usuarios//tabla
                    select new Usuario
                    {
                        IdUsuario = (int)d.IdUsuario, //Campos con los que se llenará el combo
                        NombreEmpleado = d.NombreEmpleado
                    }
                    ).ToList();
            //List<SelectListItem> itemsU = lista.ConvertAll(d =>
            //{
            //    return new SelectListItem()
            //    {
            //        Text = d.NombreEmpleado.ToString(),
            //        Value = d.IdUsuario.ToString(),
            //        Selected = false
            //    };
            //}
            // );
            //ViewBag.Verificador = itemsU;

            //return View("../CobrosPagos/CreaPagoProveedor");
            return View("../CobrosPagos/FacturaProveedor");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(PagoProveedore CProv)
        public async Task<IActionResult> Create(FacturasProveedore CProv)
        {
            try
            {
                var getIdProv = from CP in bd.CatProveedores where CP.CardName == CProv.FpIdprov select CP.CardCode;
                CProv.FpIdprov = getIdProv.FirstOrDefault();
                //CProv.DteRegistro = DateTime.Now;
                var ValidaNumFacCair = from FP in bd.FacturasProveedores where FP.FpNumFactCair == CProv.FpNumFactCair select FP;
                if (ValidaNumFacCair.Count() > 0)
                {
                    ViewBag.Action = "Incorrecto";
                    TempData["ErrorCobroCliente"] = "Numero de factura ya existente";
                }
                else
                {
                    bd.Add(CProv);
                    await bd.SaveChangesAsync();
                    ViewBag.Action = "Correcto";
                }

            }
            catch (Exception ex)
            {
                ViewBag.Action = "Incorrecto";
                TempData["ErrorCobroCliente"] = "Verificar los datos ingresados. " + ex.InnerException.ToString();
            }
            return View("../CobrosPagos/FacturaProveedor");
        }
        // GET: Usuarios/Edit/5
        //[HttpGet("Usuarios/Detalle/DocNum")]
        public async Task<IActionResult> Detalle(int DocNum)
        {
            if (DocNum == 0)
            {
                DocNum = NumerDoc;
            }
            //var search1 = await bd.ViInfoPagoProvs.FindAsync(DocNum);
            var search1 = await bd.ViInfoFacturaProveedores.FirstOrDefaultAsync(id => id.NumDoc == DocNum);
            return View("../CobrosPagos/VisualizaPagos", search1);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEstatus(int NumDoc, string Proveedor, string MontoTotal, string Estatus, string DtePago)
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
                    Update = Ejecuta.SP_ACTUALIZA_ESTATUS_FACTURAS(Bandera: 1, TF: "P", NumFact: NumDoc, Monto: MontoPagado, SC: Proveedor, Estatus: Estatus, Fecha: DtePago);
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
            var data = bd.ViInfoFacturaProveedores.ToList().OrderBy(dataindex => dataindex.NumDoc).Take(100).ToList();
            return View("../CobrosPagos/ListaPagosProv", data);
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

            var PagoProvData = bd.ViInfoFacturaProveedores.ToList();
            var datafilter = from s in PagoProvData
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
                PagoProvData = datafilter.ToList();
            }
            if (!String.IsNullOrEmpty(CobroClientesController.NameParam))
            {
                datafilter = from filt in datafilter
                             where filt.Proveedor.ToUpper().Contains(NameParam.ToUpper())
                             select filt;
                PagoProvData = datafilter.ToList();
            }
            Int32 cantcolumn = PagoProvData.Count();
            DataTable dt = new DataTable("Pagos a proveedores");
            dt.Columns.AddRange(new DataColumn[10] { new DataColumn("Número de factura"),
                                        new DataColumn("Proveedor"),
                                        new DataColumn("Tipo de moneda"),
                                        new DataColumn("Subtotal"),
                                        new DataColumn("Iva"),
                                        new DataColumn("Retención"),
                                        new DataColumn("Monto total de pago"),
                                        new DataColumn("Fecha de Pago"),
                                        new DataColumn("Estatus"),
                                        new DataColumn("FormaPago")
                                        });

            var PagosClie = datafilter;

            foreach (var data in PagosClie)
            {
                dt.Rows.Add(data.NumDoc, data.Proveedor, data.TipoMoneda, data.SubTotal, data.Iva, data.Retencion, data.MontoTotal, data.Estatus, data.FechaPago, data.FormaPago);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte de Pago de proveedores.xlsx");
                }
            }
        }
        public IActionResult ExportarPDF(ViInfoFacturaProveedore Info)
        {
            try
            {
                string parametrobuscar = "PDF_";
                try
                {//Actualiza facturas cuando inicia sesión
                    DataTable DataFactura = new DataTable();
                    var Ejecuta = new BD.ExecProcedures();
                    parametrobuscar = Convert.ToString(Info.NumDoc) + "-" + Info.Proveedor;
                    DataFactura = Ejecuta.SP_BUSCA_FACTURAS(NameParam: parametrobuscar, Flag: 1);
                    Info.FormaPago = DataFactura.Rows[0]["FormaPago"].ToString();
                    Info.TipoMoneda = DataFactura.Rows[0]["TipoMoneda"].ToString();
                    Info.MontoTotal = DataFactura.Rows[0]["MontoTotal"].ToString();
                    Info.Estatus = DataFactura.Rows[0]["Estatus"].ToString();
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
                                            new FileStream((folderPath + @"\Factura Proveedor - " + parametrobuscar + ".pdf"), FileMode.Create));
                // Le colocamos el título y el autor
                // **Nota: Esto no será visible en el documento
                doc.AddTitle("Factura de clientes");
                doc.AddCreator("LogisticaCair");
                // Abrimos el archivo
                doc.Open();
                // Creamos el tipo de Font que vamos utilizar
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.ITALIC, BaseColor.BLACK);

                // Escribimos el encabezamiento en el documento
                doc.Add(new Paragraph(Info.Proveedor));
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
                PdfPTable tbllInfo2 = new PdfPTable(3);
                tbllInfo2.WidthPercentage = 100;

                PdfPCell Moneda = new PdfPCell(new Phrase("Moneda", _standardFont));
                Moneda.BorderWidth = 0;
                Moneda.BorderWidthBottom = 0.50f;
                Moneda.BorderWidthTop = 0.80f;

                PdfPCell Monto = new PdfPCell(new Phrase("Monto", _standardFont));
                Monto.BorderWidth = 0;
                Monto.BorderWidthBottom = 0.50f;
                Monto.BorderWidthTop = 0.80f;

                Moneda = new PdfPCell(new Phrase(Info.TipoMoneda, _standardFont));
                Moneda.BorderWidth = 0;

                Monto = new PdfPCell(new Phrase(Info.SubTotal, _standardFont));
                Monto.BorderWidth = 0;

                // Añadimos las celdas a la tabla
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
                return RedirectToAction("Detalle", Info.NumDoc);
                //var data = bd.VI_INFO_PAGO_PROV.ToList();
                //return View("../CobrosPagos/ListaPagosProv", data);

            }
            catch (Exception)
            {
                var data = bd.ViInfoPagoProvs.ToList();
                return View("../CobrosPagos/ListaPagosProv", data);
            }
        }
        public IActionResult Payment()
        {
            return View("../CobrosPagos/SupplierPaymentLoad");
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
                    ClaseImportData cpp = new ClaseImportData();
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
                            Boolean upload = cpp.InsertExceldataOk(path2, "[FACTURAS_PROVEEDORES]"); ///Carga el archivo y pasa a sql
                            //path2= path_Root + "\\" + folderName;
                            //path2 = path2.Replace("\\","/");
                            //Directory.Delete(path2);//Borra el archivo para que no guarde basura
                            if (upload)
                            {
                                factsubidas = 1;
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
                return View("../CobrosPagos/SupplierPaymentLoad");
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
        [HttpGet]
        public async Task<IActionResult> Borrar(int DocNum)
        {
            try
            {
                var Ejecuta = new BD.ExecProcedures();
                DataTable Delete = new DataTable();
                Delete = Ejecuta.SP_ACTUALIZA_ESTATUS_FACTURAS(Bandera: 2, TF: "P", NumFact: DocNum);
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
            var data = bd.ViInfoFacturaProveedores.ToList().OrderBy(dataindex => dataindex.NumDoc).Take(100).ToList();
            return View("../CobrosPagos/ListaPagosProv", data);

        }
    }
}
