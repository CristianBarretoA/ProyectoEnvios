using ProyectoEnvios.Datos;
using ProyectoEnvios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoEnvios.Controllers
{
    public class EnvioController : Controller
    {

        EnvioAD eAD = new EnvioAD();
        // GET: Envio
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult registrarEnvio()
        {

            return View();
        }

        [HttpPost]
        public ActionResult registrarEnvio(EnvioCS envioCS)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                string msg = eAD.registrarEnvio(envioCS);
                if (!msg.Contains("error"))
                {
                    TempData["Exito"] = "Envio registrado, N° de guia:" + msg;
                    return RedirectToAction("ConsultaGuia", new { id = msg });
                    //ViewData["Exito"] = msg;
                    //return View();
                }
                else
                {

                    if (msg.Contains("Destinatario"))
                    {
                        ViewData["Error"] = "No se encuentra registrado el destinatario";
                        return View();
                    }
                    else
                    {
                        ViewData["Error"] = "No se encuentra registrado el remitente";
                        return View();
                    }

                }
            }
            catch (Exception ex)
            {
                string msg = "Error al crear al cliente " + ex;
                ModelState.AddModelError("Error al crear el cliente, ", ex);
                ViewData["Error"] = msg;
                return View();
            }
        }

        public ActionResult ConsultaGuia(int id)
        {
            try
            {
                EnvioCS e = new EnvioCS();
                e = eAD.consultarGuia(id);
                if (e.idEnvio != 0)
                {
                    return View(e);
                }
                else
                {
                    ViewData["Error"] = "El numero de guia ingresado no existe";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al logear, ", ex.Message);
                return View();
            }
        }

        [HttpPost]
        public ActionResult ConsultaGuia(EnvioCS eS)
        {
            int id = eS.idEnvio;
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                string msg = eAD.actualizarEstado(eS);
                if (!msg.Contains("error"))
                {
                    TempData["Exito"] = msg;
                    return RedirectToAction("ConsultaGuia", new { id = eS.idEnvio });
                }
                else
                {
                    TempData["Error"] = msg;
                    return RedirectToAction("ConsultaGuia", new { id = eS.idEnvio });
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al actualizar estado, ", ex.Message);
                return RedirectToAction("ConsultaGuia", new { id = eS.idEnvio });
            }
        }

        public ActionResult consultarEnvios()
        {
            try
            {
                return View(eAD.listarEnvios());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al consultar envios, ", ex.Message);
                return View();
            }

        }




        public ActionResult _ListarCiudades(int id)
        {
            ViewData["id"] = id.ToString();
            return PartialView(eAD.listaCiudades());
        }

        public ActionResult _ListarEstados()
        {
            return PartialView(eAD.listaEstado());
        }

        public ActionResult _ListarProductos()
        {
            return PartialView(eAD.listaProductos());
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }


    }
}