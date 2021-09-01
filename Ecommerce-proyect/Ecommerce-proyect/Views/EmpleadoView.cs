using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_proyect.Views
{
    public class EmpleadoView
    {

        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(40)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(12)]
        public string Telefono { get; set; }

        [Required]
        [StringLength(50)]
        public string Correo { get; set; }

        [Required]
        [StringLength(50)]
        public string Direccion { get; set; }

        [Required]
        [StringLength(15)]
        public string Nit { get; set; }

        [Required]
        public int? IdRol { get; set; }

        [Required]
        [StringLength(40)]
        public string Rol { get; set; }
    }
}
