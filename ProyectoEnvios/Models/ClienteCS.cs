using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoEnvios.Models
{
    public class ClienteCS
    {
        [Display(Name = "Documento")]
        public int IdentificacionUsuario { get; set; }

        [Display(Name = "Nombre")]
        [Required]
        public string NombreUsuario { get; set; }

        [Display(Name = "Apellido")]
        [Required]
        public string ApellidoUsuario { get; set; }

        [Display(Name = "Edad")]
        [Required]
        public int EdadUsuario { get; set; }

        [Display(Name = "Tipo Documento")]
        public long TipoDocumento { get; set; }

        [Display(Name = "Usuario")]
        public string Usuario { get; set; }

        [Display(Name = "Contraseña")]
        public string Pass { get; set; }

        [Display(Name = "Tipo Documento")]
        public string NombreDocumento { get; set; }

        [Display(Name = "Rol")]
        public string rol { get; set; }


    }

    public partial class IdentificacionCS
    {

        public int IdTipoDocumento { get; set; }

        public string NombreDocumento { get; set; }

    }
}