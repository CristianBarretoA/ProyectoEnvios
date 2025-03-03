﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ProyectoEnvios.Models;

namespace ProyectoEnvios.Datos
{
    public class ClienteAD : db
    {
        public ClienteCS login(string usr, string pass)
        {
            ClienteCS cS = new ClienteCS();
            using (SqlCommand com = new SqlCommand("loginUsuario", connection()))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@user", usr);
                com.Parameters.AddWithValue("@pass", pass);
                var reader = com.ExecuteReader();
                while (reader.Read())
                {
                    cS.NombreUsuario = Convert.ToString(reader["nombreUsuario"]);
                    cS.ApellidoUsuario = Convert.ToString(reader["apellidoUsuario"]);
                }
            }
            return cS;
        }

        public List<ClienteCS> consultarClientes()
        {
            using (SqlCommand com = new SqlCommand("buscarUsuarios", connection()))
            {
                com.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(com);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                List<ClienteCS> clientList = new List<ClienteCS>();
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    ClienteCS cS = new ClienteCS();
                    cS.IdentificacionUsuario = Convert.ToInt32(dataRow["identificacionUsuario"]);
                    cS.NombreUsuario = Convert.ToString(dataRow["nombreUsuario"]);
                    cS.ApellidoUsuario = Convert.ToString(dataRow["apellidoUsuario"]);
                    cS.EdadUsuario = Convert.ToInt32(dataRow["edadUsuario"]);
                    cS.TipoDocumento = Convert.ToInt64(dataRow["tipoDocUsuario"]);

                    clientList.Add(cS);
                }

                return clientList;
            }
        }

        public ClienteCS consultarClienteID(int id)
        {
            using (SqlCommand com = new SqlCommand("buscarUsuarioId", connection()))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@idCliente", id);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(com);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                ClienteCS cS = new ClienteCS();
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    cS.IdentificacionUsuario = Convert.ToInt32(dataRow["identificacionUsuario"]);
                    cS.NombreUsuario = Convert.ToString(dataRow["nombreUsuario"]);
                    cS.ApellidoUsuario = Convert.ToString(dataRow["apellidoUsuario"]);
                    cS.EdadUsuario = Convert.ToInt32(dataRow["edadUsuario"]);
                    cS.TipoDocumento = Convert.ToInt64(dataRow["tipoDocUsuario"]);
                }

                return cS;
            }
        }

        public string agregarCliente(ClienteCS c)
        {
            string msg;
            try
            {
                using (var com = new SqlCommand("agregarUsuario", connection()))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@idCliente", c.IdentificacionUsuario);
                    com.Parameters.AddWithValue("@nombre", c.NombreUsuario);
                    com.Parameters.AddWithValue("@apellido", c.ApellidoUsuario);
                    com.Parameters.AddWithValue("@edad", c.EdadUsuario);
                    com.Parameters.AddWithValue("@tipoDocumento", c.TipoDocumento);
                    com.Parameters.AddWithValue("@usuario", c.Usuario);
                    com.Parameters.AddWithValue("@pass", c.Pass);
                    var filas = com.ExecuteNonQuery();
                    if (filas != 0)
                    {
                        msg = "Se ha registrado exitosamente al cliente";
                    }
                    else
                    {
                        msg = "Hubo un error en el registro, por favor valide sus datos";
                    }

                }
            }
            catch (Exception ex)
            {
                msg = "Excepcion!! error de tipo: " + ex;
            }
            return msg;
        }

        public string editarCliente(ClienteCS cS)
        {
            string msg;
            try
            {
                using (SqlCommand com = new SqlCommand("actualizarCliente", connection()))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@idCliente", cS.IdentificacionUsuario);
                    com.Parameters.AddWithValue("@nombreCliente", cS.NombreUsuario);
                    com.Parameters.AddWithValue("@apellidoCliente", cS.ApellidoUsuario);
                    com.Parameters.AddWithValue("@edad", cS.EdadUsuario);
                    com.Parameters.AddWithValue("@tipoDocumento", cS.TipoDocumento);
                    var filas = com.ExecuteNonQuery();
                    if (filas != 0)
                    {
                        msg = "Se ha modificado exitosamente al cliente";
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

        public string borrarCliente(int id)
        {
            string msg;
            try
            {
                using (SqlCommand com = new SqlCommand("borrarCliente", connection()))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@idCliente", id);
                    var filas = com.ExecuteNonQuery();
                    if (filas != 0)
                    {
                        msg = "Se ha borrado exitosamente al cliente";
                    }
                    else
                    {
                        msg = "Hubo un error en la eliminacion del cliente, por favor valide sus datos";
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "Excepcion!! error de tipo: " + ex;
            }
            return msg;
        }
    }
}