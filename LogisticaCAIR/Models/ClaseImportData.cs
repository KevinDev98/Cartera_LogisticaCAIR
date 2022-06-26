using LogisticaCAIR.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticaCAIR.Models
{
    public class ClaseImportData
    {
        public static IConfigurationRoot Configuration;
        private void CleanDataTable(ref DataTable dt,string TFactura)
        {
            var Ejecuta = new BD.ExecProcedures();
            var ColumnasTablaBD = Ejecuta.SP_CARGAS_PAGOS(TF:TFactura);
            var encabezado = true;
            int columnasTMP = dt.Columns.Count;
            int columnasCarga = ColumnasTablaBD.Rows.Count;

            // Validamos que las columnas estén iguales
            if (columnasCarga > columnasTMP)
                throw new Exception("El encabezado es incorrecto. Tiene campos extra");
            else if (columnasCarga < columnasTMP)
                throw new Exception("El encabezado es incorecto. Le faltan campos");

            // Renombramos las columnas para que coincidan con la tabla de facturas
            for (var i = 0; i <= columnasTMP - 1; i++)
                dt.Columns[i].ColumnName = ColumnasTablaBD.Rows[i][0].ToString();

            // Eliminamos la primer fila que trae el encabezado del excel
            dt.Rows.RemoveAt(0);
        }
        public Boolean InsertExceldataOk(string fullPath, string TypeLoad = "[FACTURAS_CLIENTES]")
        {
            DB_LOGISTICA_CAIRContext bd = new DB_LOGISTICA_CAIRContext();
            var context = new DB_LOGISTICA_CAIRContext();
            var hasHeader = true;
            FileInfo excelFile = new FileInfo(fullPath);

            using (var epPackage = new ExcelPackage(excelFile))
            {
                var pck = epPackage.Workbook.Worksheets.First();
                var ws = pck.Workbook.Worksheets.First();
                DataTable tbl = new DataTable();
                foreach (var firstRowCell in ws.Cells[1, 1, 1,
                ws.Dimension.End.Column])
                {
                    if (firstRowCell.Text.ToString().Contains("FECHA"))
                    {
                        tbl.Columns.Add(hasHeader ? firstRowCell.Text :
                    string.Format("Column {0:yyyy-MM-dd}", firstRowCell.Start.Column));
                    }
                    else
                    {
                        tbl.Columns.Add(hasHeader ? firstRowCell.Text :
                    string.Format("Column {0}", firstRowCell.Start.Column));
                    }
                }
                var startRow = hasHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    DataRow row = tbl.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        if (cell.Start.Column - 1 == 0 || cell.Start.Column - 1 == 1 || cell.Start.Column - 1 == 13)
                        {
                            var DateString = cell.Text;
                            DateString = DateString.Replace("/", "-");
                            //Convert.ToDateTime(DateString, null);
                            //row[cell.Start.Column - 1] = Convert.ToDateTime(Convert.ToDateTime(DateString, null).ToShortDateString().Replace("/","-"));
                            row[cell.Start.Column - 1] = DateString;
                        }
                        else
                        {
                            row[cell.Start.Column - 1] = cell.Text;
                        }

                    }
                }
                SqlConnectionStringBuilder Conect = new SqlConnectionStringBuilder();
                context.Database.OpenConnection();
                Conect.ConnectionString = context.Database.GetDbConnection().ConnectionString;
                var conexion = Conect.ConnectionString;
                conexion = conexion.Replace("ID=sa;", "ID=sa;Password=inginf98;");
                using (var sqlCopy = new SqlBulkCopy(conexion))
                {
                    try
                    {
                        sqlCopy.DestinationTableName = TypeLoad;
                        
                        foreach (DataRow FilaCol in tbl.Rows)//Valida si un registro del archivo ya se encuentra en la BD
                        {
                            if (TypeLoad == "[FACTURAS_CLIENTES]")
                            {
                                var existefactura = from data in bd.FacturasClientes
                                                    where data.FcNumFactCair.ToString() == FilaCol[2].ToString()
                                                    select data;
                                if (existefactura.Count() > 0)
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                var existefactura = from data in bd.FacturasProveedores
                                                where data.FpNumFactCair.ToString() == FilaCol[5].ToString()
                                                select data;
                                if (existefactura.Count() > 0)
                                {
                                    return false;
                                }
                            }
                        }
                        //foreach (DataColumn column in tbl.Columns) ///Funciona cuando los encabezados son iguales
                        //{
                        //    sqlCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(column.ColumnName, column.ColumnName));
                        //}
                        string tablaNombre = TypeLoad;
                        tablaNombre = tablaNombre.Replace("[","").Replace("]","");
                        CleanDataTable(ref tbl, tablaNombre);
                        for (int i = 0; i <= tbl.Columns.Count - 1; i++)
                        {
                            sqlCopy.ColumnMappings.Add(tbl.Columns[i].ColumnName, tbl.Columns[i].ColumnName);
                        }                            

                        if (sqlCopy.ColumnMappings.Count == tbl.Columns.Count)
                        {
                            sqlCopy.WriteToServer(tbl);
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                    catch (Exception eror)
                    {
                        eror.Message.ToString();
                        return false;
                    }

                }

            }
        }
    }
}
