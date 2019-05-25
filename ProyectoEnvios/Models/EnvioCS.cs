using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoEnvios.Models
{
    public class EnvioCS
    {
        [Display(Name = "Numero de Guia")]
        public int idEnvio { get; set; }

        public DateTime fechaEnvio { get; set; }

        public int idDestino { get; set; }

        public string destino { get; set; }

        public double peso { get; set; }

        public int idEstado { get; set; }

        public string estado { get; set; }

        public string direccion { get; set; }
    }

    public partial class Estado
    {
        public int idEstado { get; set; }

        public string estado { get; set; }
    }

}