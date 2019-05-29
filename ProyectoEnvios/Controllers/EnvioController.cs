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
                    //TempData["Exito"] = msg;
                    //return RedirectToAction("registrarEnvio");
                    ViewData["Exito"] = msg;
                    return View();
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
                
                ModelState.AddModelError("Error al crear el cliente, ", ex.Message);
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

        ClienteAD cAD = new ClienteAD();

        //public ActionResult _consultarCliente()
        //{
        //    return PartialView();
        //}


        public ActionResult _consultarCliente(int id)
        {
            try
            {
                ClienteCS cS = cAD.consultarClienteID(id);
                return PartialView(cS);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al traer informacion del cliente, ", ex.Message);
                return PartialView();
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }


    }
}