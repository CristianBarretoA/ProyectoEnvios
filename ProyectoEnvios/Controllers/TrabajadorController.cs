using ProyectoEnvios.Datos;
using ProyectoEnvios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoEnvios.Controllers
{
    public class TrabajadorController : Controller
    {
        // GET: Trabajador
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }

        ClienteAD cAD = new ClienteAD();

        public ActionResult consultarClientes()
        {
            try
            {
                if (TempData["Exito"] != null)
                {
                    ViewData["Exito"] = TempData["Exito"];
                    return View(cAD.consultarClientes());
                }
                else
                {
                    return View(cAD.consultarClientes());
                }
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
                    TempData["Exito"] = msg;
                    return RedirectToAction("consultarClientes");
                }
                else
                {
                    ViewData["Error"] = msg;
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al crear el cliente, ", ex.Message);
                return View();
            }
        }

        public ActionResult ListarDocumentos(int id)
        {
            ViewData["id"] = id.ToString();
            return PartialView(cAD.listaDocumentos());

        }

        public ActionResult ListarRoles(int id)
        {
            ViewData["id"] = id.ToString();
            return PartialView(cAD.listaRoles());

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
                    TempData["Exito"] = msg;
                    return RedirectToAction("consultarClientes");
                }
                else
                {
                    ViewData["Error"] = msg;
                    return View(cS);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al modificar cliente, ", ex.Message);
                return View(cS);
            }
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
    }
}