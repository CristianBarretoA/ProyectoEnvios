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

        public string nombreRemitente { get; set; }

        public int idRemitente { get; set; }

        public string nombreDestinatario { get; set; }

        public int idDestinatario { get; set; }

        public DateTime fechaRecepcion { get; set; }

        public DateTime fechaEntrega { get; set; }

        public int idOrigen { get; set; }

        public string origen { get; set; }

        public int idDestino { get; set; }

        public string destino { get; set; }

        public int idTipoProducto { get; set; }

        public string tipoProducto { get; set; }

        public double peso { get; set; }

        public int idEstado { get; set; }

        public string estado { get; set; }

        public string direccionOrigen { get; set; }

        public string direccionDestino { get; set; }
    }

    public partial class Estado
    {
        public int idEstado { get; set; }

        public string estado { get; set; }
    }

}