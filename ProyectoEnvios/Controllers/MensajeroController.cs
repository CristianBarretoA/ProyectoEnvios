using ProyectoEnvios.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoEnvios.Controllers
{
    public class MensajeroController : Controller
    {

        EnvioAD eAD = new EnvioAD();

        // GET: Mensajero
        public ActionResult Index(int id)
        {
            try
            {
                return View(eAD.listarEnviosMensajero(id));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al consultar envios, ", ex.Message);
                return View();
            }
        }

        public ActionResult consultarEnvios(int id)
        {
            try
            {
                return View(eAD.listarEnviosMensajero(id));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al consultar envios, ", ex.Message);
                return View();
            }


        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }
    }
}