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

        public string registrarEnvio(EnvioCS e)
        {
            string msg = "";
            try
            {
                e.fechaEntrega = DateTime.Now;
                using (var com = new SqlCommand("registrarEnvio", connection()))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@fechaRecepcion", e.fechaEntrega);
                    com.Parameters.AddWithValue("@idOrigen", e.idOrigen);
                    com.Parameters.AddWithValue("@idDestino", e.idDestino);
                    com.Parameters.AddWithValue("@idTipoProducto", e.idTipoProducto);
                    com.Parameters.AddWithValue("@peso", e.peso);
                    com.Parameters.AddWithValue("@direccionDestino", e.direccionDestino);
                    com.Parameters.AddWithValue("@idRemitente", e.idRemitente);
                    com.Parameters.AddWithValue("@idDestinatario", e.idDestinatario);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(com);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    if (dataSet != null)
                    {
                        foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                        {
                            msg = Convert.ToString(dataRow["numeroGuia"]);
                        }
                    }
                    else
                    {

                        msg = "Error al registrar el envio";
                    }
                }
            }
            catch (SqlException ex)
            {
                SqlError er = ex.Errors[0];
                
                msg = "Excepcion!! error de tipo: " + ex.Message;
            }
            return msg;
        }

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