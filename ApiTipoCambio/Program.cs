using ApiTipoCambio.db;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTipoCambio
{
    internal class Program
    {
        static void Main(string[] args)
        {

            #region Variables
            string monedaDolar = "Dólares de EE.UU.";
            bool result;
            String buscarSiExisteFecha = "";
            Persistent persistent = new Persistent();
            SqlConnection connection = persistent.GetConnection();
            Banguat.TipoCambio cliente = new Banguat.TipoCambio();
            var resultado = cliente.TipoCambioDia();
            var tipoCambioDolar = resultado.CambioDolar.First().referencia.ToString();
            var fechaCambioDolar = resultado.CambioDolar.First().fecha.ToString();
            #endregion

            Console.WriteLine("********************************");
            Console.WriteLine("** CONECTANDO A BANGUAT PARA INSERTAR TIPO DE CAMBIO **");

            connection.Open();
            buscarSiExisteFecha = persistent.buscarSiExisteFecha(connection);
            if (buscarSiExisteFecha == "0")
            {                
                result = persistent.InsertandoTipoDeCambio(monedaDolar, tipoCambioDolar, fechaCambioDolar, connection);
                if (result != false) { 
                   Console.WriteLine("Error al insertar fecha"+result);
                }
            }
            else
            { 
              Console.WriteLine("Si existe fecha insertada");                
            }                    
            connection.Close();
            Console.WriteLine("********************************");
            Console.WriteLine("** FIN DEL PROCESO **");
            


        }
    }
}
