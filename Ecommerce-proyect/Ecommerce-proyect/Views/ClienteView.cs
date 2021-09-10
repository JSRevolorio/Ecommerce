using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_proyect.Views
{
    public class ClienteView
    {

        public int Id { get; set; }


        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public string Correo { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required]
        public string Nit { get; set; }

        [Required]
        public string Usuario { get; set; }

        [Required]
        public string Contraseña { get; set; }

        [Required]
        public int? IdTipoCliente { get; set; }
        public string ClienteTipo { get; set; }
    
    }
}
