﻿using ProyectoEnvios.Datos;
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


        // GET: Usuario
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(ClienteCS cS)
        {
            try
            {
                ClienteCS c = new ClienteCS();
                c = cAD.login(cS.Usuario, cS.Pass);
                if (c.rol != null)
                {
                    Session["userID"] = c.NombreUsuario + " " + c.ApellidoUsuario + "," + c.rol;
                    switch (c.rol)
                    {
                        case "Cliente":
                            return RedirectToAction("Index", "Usuario");
                        case "Trabajador":
                            return RedirectToAction("Index", "Trabajador");
                        case "Mensajero":
                            return RedirectToAction("Index", "Mensajero");
                        default:
                            return RedirectToAction("Login", "Home");
                    }
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