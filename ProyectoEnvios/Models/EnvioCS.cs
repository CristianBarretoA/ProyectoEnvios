using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoEnvios.Models
{
    public class EnvioCS
    {
        [Display(Name = "Numero de guia")]
        public int idEnvio { get; set; }

        [Display(Name = "Nombre del remitente")]
        public string nombreRemitente { get; set; }

        [Display(Name = "Remitente")]
        public int idRemitente { get; set; }

        [Display(Name = "Nombre del destinatario")]
        public string nombreDestinatario { get; set; }

        [Display(Name = "Destinatario")]
        public int idDestinatario { get; set; }

        [Display(Name = "Fecha de recepcion")]
        public DateTime fechaRecepcion { get; set; }

        [Display(Name = "Fecha de entrega")]
        public DateTime fechaEntrega { get; set; }

        public int idOrigen { get; set; }

        [Display(Name = "Ciudad origen")]
        public string origen { get; set; }

        public int idDestino { get; set; }

        [Display(Name = "Ciudad destino")]
        public string destino { get; set; }

        public int idTipoProducto { get; set; }

        [Display(Name = "Producto")]
        public string tipoProducto { get; set; }

        [Display(Name = "Peso (Kg)")]
        public double peso { get; set; }

        public int idEstado { get; set; }

        [Display(Name = "Estado del envio")]
        public string estado { get; set; }

        [Display(Name = "Direccion Origen")]
        public string direccionOrigen { get; set; }

        [Display(Name = "Direccion Destino")]
        public string direccionDestino { get; set; }
    }

    public partial class Estado
    {
        public int idEstado { get; set; }

        public string estado { get; set; }
    }

    public partial class Ciudades
    {
        public int idCiudades { get; set; }

        public string nombreCiudades { get; set; }
    }

    public partial class TipoProducto
    {
        public int idTipoProducto { get; set; }

        public string nombreTipoProducto { get; set; }
    }
}