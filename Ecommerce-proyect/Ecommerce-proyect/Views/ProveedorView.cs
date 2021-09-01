using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_proyect.Views
{
    public class ProveedorView
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(12)]
        public string Telefono { get; set; }

        [Required]
        [StringLength(40)]
        public string Direccion { get; set; }

        [Required]
        [StringLength(50)]
        public string Correo { get; set; }
    }
}
