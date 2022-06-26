using LogisticaCAIR.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticaCAIR.BD
{
    public class ExecProcedures
    {
        public SqlCommand Comando()
        {
            SqlConnectionStringBuilder Conect = new SqlConnectionStringBuilder();
            var context = new DB_LOGISTICA_CAIRContext();
            context.Database.OpenConnection();
            Conect.ConnectionString = context.Database.GetDbConnection().ConnectionString;
            var conexion = Conect.ConnectionString;
            conexion = conexion.Replace("ID=sa;", "ID=sa;Password=inginf98;");
            SqlConnection ExecSP = (SqlConnection)context.Database.GetDbConnection();
            SqlCommand ExecCommand = ExecSP.CreateCommand();
            return ExecCommand;
        }
        public DataTable SP_BUSCA_FACTURAS(string DateInit = "", string DateEnd = "", string NameParam = "", int Flag = 0)
        {
            DataTable DataResult = new DataTable();
            SqlCommand ExecCommand = Comando(); ;
            ExecCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ExecCommand.CommandText = "SP_BUSCA_FACTURAS";
            ExecCommand.Parameters.Add("@BANDERA", SqlDbType.Int).Value = Flag;
            if ((!String.IsNullOrEmpty(DateInit) && !String.IsNullOrEmpty(DateEnd)))
            {
                ExecCommand.Parameters.Add("@FECHA1", SqlDbType.Date).Value = DateInit;
                ExecCommand.Parameters.Add("@FECHA2", SqlDbType.Date).Value = DateEnd;
            }
            ExecCommand.Parameters.Add("@PARAMETRO", SqlDbType.VarChar, 50).Value = NameParam;
            DataResult.TableName = "SP_BUSCA_FACTURAS";
            try
            {
                DataResult.Load(ExecCommand.ExecuteReader());
            }
            catch (Exception ex)
            {
                DataResult.TableName = "Exception";
                DataResult.Columns.Add("Mensaje");
                DataRow row = DataResult.NewRow();
                row["Mensaje"] = ex.Message;
                DataResult.Rows.Add(row);
            }
            return DataResult;
        }
        public DataTable SP_ACTUALIZA_ESTATUS_FACTURAS(int Bandera = 0, string TF = "", int NumFact = 0, Double Monto = 0.0, string SC = "", string Estatus = "", string Fecha = "")
        {
            DataTable DataResult = new DataTable();
            SqlCommand ExecCommand = Comando();
            ExecCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ExecCommand.CommandText = "SP_ACTUALIZA_ESTATUS_FACTURAS";
            ExecCommand.Parameters.Add("@BANDERA", SqlDbType.Int).Value = Bandera;
            if (!String.IsNullOrEmpty(Fecha))
            {
                ExecCommand.Parameters.Add("@FECHA", SqlDbType.Date).Value = Fecha;
            }
            ExecCommand.Parameters.Add("@TIPO_FACTURA", SqlDbType.VarChar, 50).Value = TF;
            ExecCommand.Parameters.Add("@ID_FACTURA", SqlDbType.Int).Value = NumFact;
            ExecCommand.Parameters.Add("@MONTO", SqlDbType.Float).Value = Monto;
            ExecCommand.Parameters.Add("@SC", SqlDbType.VarChar, 50).Value = SC;
            ExecCommand.Parameters.Add("@ESTATUS", SqlDbType.VarChar, 50).Value = Estatus;

            DataResult.TableName = "SP_ACTUALIZA_ESTATUS_FACTURAS";
            try
            {
                DataResult.Load(ExecCommand.ExecuteReader());
            }
            catch (Exception ex)
            {
                DataResult.TableName = "Exception";
                DataResult.Columns.Add("Mensaje");
                DataRow row = DataResult.NewRow();
                row["Mensaje"] = ex.Message;
                DataResult.Rows.Add(row);
            }
            return DataResult;
        }
        public DataTable SP_CARGAS_PAGOS(int Bandera = 0, string TF = "")
        {
            DataTable DataResult = new DataTable();
            SqlCommand ExecCommand = Comando();
            ExecCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ExecCommand.CommandText = "SP_CARGAS_PAGOS";
            ExecCommand.Parameters.Add("@BANDERA", SqlDbType.Int).Value = Bandera;
            ExecCommand.Parameters.Add("@TIPO_CARGA", SqlDbType.VarChar, 50).Value = TF;
            DataResult.TableName = "SP_CARGAS_PAGOS";
            try
            {
                DataResult.Load(ExecCommand.ExecuteReader());
            }
            catch (Exception ex)
            {
                DataResult.TableName = "Exception";
                DataResult.Columns.Add("Mensaje");
                DataRow row = DataResult.NewRow();
                row["Mensaje"] = ex.Message;
                DataResult.Rows.Add(row);
            }
            return DataResult;
        }
    }
}
