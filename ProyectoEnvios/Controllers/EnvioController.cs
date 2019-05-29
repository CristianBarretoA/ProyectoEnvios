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