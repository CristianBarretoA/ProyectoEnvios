using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ProyectoEnvios.Models;

namespace ProyectoEnvios.Datos
{
    public class EnvioAD : db
    {
        public EnvioCS consultarGuia(int idGuia)
        {
            using (SqlCommand com = new SqlCommand("consultarGuia", connection()))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@idGuia", idGuia);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(com);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                EnvioCS eS = new EnvioCS();
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    eS.idEnvio = Convert.ToInt32(dataRow["idEnvio"]);
                    eS.nombreRemitente = Convert.ToString(dataRow["nombreRemitente"]);
                    eS.nombreDestinatario = Convert.ToString(dataRow["nombreDestinatario"]);
                    eS.fechaRecepcion = Convert.ToDateTime(dataRow["fechaRecepcion"]);
                    eS.fechaEntrega = Convert.ToDateTime(dataRow["fechaEntrega"]);
                    eS.fechaEntrega = Convert.ToDateTime(dataRow["fechaEntrega"]);
                    eS.origen = Convert.ToString(dataRow["ciudadOrigen"]);
                    eS.destino = Convert.ToString(dataRow["ciudadDestino"]);
                    eS.tipoProducto = Convert.ToString(dataRow["tipoProducto"]);
                    eS.peso = Convert.ToInt32(dataRow["peso"]);
                    eS.estado = Convert.ToString(dataRow["estado"]);
                    eS.direccionOrigen = Convert.ToString(dataRow["direccionOrigen"]);
                    eS.direccionDestino = Convert.ToString(dataRow["direccionDestino"]);
                }
                return eS;
            }

        }


    }



}