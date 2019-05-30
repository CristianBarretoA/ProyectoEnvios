using ProyectoEnvios.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

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
                msg = "Excepcion!! error de tipo: " + ex;
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

        public string actualizarEstado(EnvioCS eS)
        {
            string msg;
            try
            {
                eS.fechaEntrega = DateTime.Now;
                using (SqlCommand com = new SqlCommand("actualizarEstado", connection()))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@idEnvio", eS.idEnvio);
                    com.Parameters.AddWithValue("@fechaEntrega", eS.fechaEntrega);
                    com.Parameters.AddWithValue("@idEstado", eS.idEstado);
                    var filas = com.ExecuteNonQuery();
                    if (filas != 0)
                    {
                        msg = "Se ha cambiado el estado exitosamente";
                    }
                    else
                    {
                        msg = "Hubo un error en la actualizacion, por favor valide sus datos";
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "Excepcion!! error de tipo: " + ex;
            }
            return msg;

        }

        public List<EnvioCS> listarEnvios()
        {
            using (SqlCommand com = new SqlCommand("consultarEnvios", connection()))
            {
                com.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(com);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                List<EnvioCS> enviosList = new List<EnvioCS>();

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    EnvioCS eS = new EnvioCS();
                    eS.idEnvio = Convert.ToInt32(dataRow["idEnvio"]);
                    eS.fechaRecepcion = Convert.ToDateTime(dataRow["fechaRecepcion"]);
                    eS.fechaEntrega = Convert.ToDateTime(dataRow["fechaEntrega"]);
                    eS.nombreDestinatario = Convert.ToString(dataRow["destinatario"]);
                    eS.destino = Convert.ToString(dataRow["ciudadDestino"]);
                    eS.direccionDestino = Convert.ToString(dataRow["direccionDestino"]);
                    eS.estado = Convert.ToString(dataRow["estado"]);
                    enviosList.Add(eS);
                }

                return enviosList;
            }
        }

        public List<EnvioCS> listarEnviosMensajero(int idMensajero)
        {
            using (SqlCommand com = new SqlCommand("consultarEnviosMensajero", connection()))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@idMensajero", idMensajero);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(com);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                List<EnvioCS> enviosList = new List<EnvioCS>();

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    EnvioCS eS = new EnvioCS();
                    eS.idEnvio = Convert.ToInt32(dataRow["idEnvio"]);
                    eS.fechaRecepcion = Convert.ToDateTime(dataRow["fechaRecepcion"]);
                    eS.fechaEntrega = Convert.ToDateTime(dataRow["fechaEntrega"]);
                    eS.destino = Convert.ToString(dataRow["ciudadDestino"]);
                    eS.direccionDestino = Convert.ToString(dataRow["direccionDestino"]);
                    eS.estado = Convert.ToString(dataRow["estado"]);
                    enviosList.Add(eS);
                }

                return enviosList;
            }
        }

        public List<Ciudades> listaCiudades()
        {

            using (var com = new SqlCommand("SELECT * FROM Ciudades;", connection()))
            {
                com.CommandType = CommandType.Text;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(com);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                List<Ciudades> ciudadesList = new List<Ciudades>();

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    Ciudades ciudades = new Ciudades();

                    ciudades.idCiudades = Convert.ToInt32(dataRow["idCiudad"]);
                    ciudades.nombreCiudades = Convert.ToString(dataRow["nombreCiudad"]);

                    ciudadesList.Add(ciudades);
                }
                return ciudadesList;
            }
        }

        public List<Estado> listaEstado()
        {

            using (var com = new SqlCommand("SELECT * FROM Estado;", connection()))
            {
                com.CommandType = CommandType.Text;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(com);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                List<Estado> estadoList = new List<Estado>();

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    Estado estado = new Estado();

                    estado.idEstado = Convert.ToInt32(dataRow["idEstado"]);
                    estado.estado = Convert.ToString(dataRow["estado"]);

                    estadoList.Add(estado);
                }
                return estadoList;
            }
        }

        public List<TipoProducto> listaProductos()
        {

            using (var com = new SqlCommand("SELECT * FROM TipoProducto;", connection()))
            {
                com.CommandType = CommandType.Text;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(com);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                List<TipoProducto> tipoProductosList = new List<TipoProducto>();

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    TipoProducto tipoProducto = new TipoProducto();

                    tipoProducto.idTipoProducto = Convert.ToInt32(dataRow["idTipoProducto"]);
                    tipoProducto.nombreTipoProducto = Convert.ToString(dataRow["tipoProducto"]);

                    tipoProductosList.Add(tipoProducto);
                }
                return tipoProductosList;
            }
        }


    }



}