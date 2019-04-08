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

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ClienteCS cS)
        {
            try
            {
                ClienteCS c = new ClienteCS();
                c = cAD.login(cS.Usuario, cS.Pass);
                if (c.NombreUsuario != null)
                {
                    Session["userID"] = c.NombreUsuario + " " + c.ApellidoUsuario;
                    return RedirectToAction("consultarClientes");
                }
                else
                {
                    ViewData["Error"] = "Usuario o contraseña erroneo, por favor valide";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al logear, ", ex.Message);
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }

        public ActionResult consultarClientes()
        {
            try
            {
                return View(cAD.consultarClientes());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al consultar clientes, ", ex.Message);
                return View();
            }


        }

        public ActionResult agregarCliente()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult agregarCliente(ClienteCS cS)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                string msg = cAD.agregarCliente(cS);
                if (!msg.Contains("error"))
                {
                    ViewData["Exito"] = msg;
                }
                else
                {
                    ViewData["Error"] = msg;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al crear el cliente, ", ex.Message);
            }
            return View();
        }



        public ActionResult editarCliente(int id)
        {
            try
            {
                ClienteCS cS = cAD.consultarClienteID(id);
                return View(cS);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al traer informacion del cliente, ", ex.Message);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult editarCliente(ClienteCS cS)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                string msg = cAD.editarCliente(cS);
                if (!msg.Contains("error"))
                {
                    ViewData["Exito"] = msg;
                }
                else
                {
                    ViewData["Error"] = msg;
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al modificar cliente, ", ex.Message);
            }
            return View(cS);
        }

        public ActionResult borrarCliente(int id)
        {
            try
            {
                string msg = cAD.borrarCliente(id);
                if (!msg.Contains("error"))
                {
                    TempData["Exito"] = msg;
                }
                else
                {
                    TempData["Error"] = msg;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al modificar cliente, ", ex.Message);
            }
            return RedirectToAction("consultarClientes");
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