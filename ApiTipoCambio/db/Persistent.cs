using System;
using System.Data;
using System.Data.SqlClient;

namespace ApiTipoCambio.db
{
    internal class Persistent
    {

        public SqlConnection GetConnection()
        {

            SqlConnection connection = new SqlConnection();
            String productionConnectionString;
            productionConnectionString = "Server=172.31.125.10;Database=CNVSIB;Integrated Security=SSPI;";
            try
            {
                connection = new SqlConnection(productionConnectionString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return connection;
        }


        public bool InsertandoTipoDeCambio(String moneda,string tipoCambio,string fecha, SqlConnection connection)
        {
            Boolean resp = false;
            try
            {
                SqlCommand cmdSql = new SqlCommand();
                cmdSql.Connection = connection;
                cmdSql.CommandType = CommandType.Text;                
                cmdSql.CommandText = "INSERT INTO [dbo].[CAT_CAMBIO_POR_DIA] ([Moneda],[Tipo_Cambio],[Fecha])VALUES (@moneda,@tipoCambio,@fecha);";
                cmdSql.Parameters.Add("@moneda", SqlDbType.VarChar).Value = moneda;
                cmdSql.Parameters.Add("@tipoCambio", SqlDbType.VarChar).Value = tipoCambio;
                cmdSql.Parameters.Add("@fecha", SqlDbType.Date).Value = fecha;

                if (cmdSql.ExecuteNonQuery() == 0)
                {
                    resp = true;
                }
            }
            catch (SqlException e)
            {
                System.Console.WriteLine("Err:" + e.Message);

            }
            return resp;
        }

        public String buscarSiExisteFecha(SqlConnection connection)
        {
           String estado = "" ;
            try
            {
                SqlCommand cmdSql = new SqlCommand();
                cmdSql.Connection = connection;
                cmdSql.CommandType = CommandType.Text;
                cmdSql.CommandText = "SELECT count(Fecha) AS VALOR FROM [dbo].[CAT_CAMBIO_POR_DIA] WHERE CONVERT(varchar, GETDATE(), 103) = CONVERT(varchar,Fecha, 103)";                                
                estado = Convert.ToString(cmdSql.ExecuteScalar());
                return estado;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("ERROR:" + e.Message);
            }
            return estado;

        }
    }
}
