using ProyectoEnvios.Datos;
using ProyectoEnvios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoEnvios.Controllers
{
    public class HomeController : Controller
    {

        ClienteAD cAD = new ClienteAD();
        EnvioAD eAD = new EnvioAD();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(EnvioCS es)
        {
            return RedirectToAction("ConsultaGuia", new { id = es.idEnvio });
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


        //No Borrarj
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}