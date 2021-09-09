using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_proyect.Views
{
    public class CompraPresentacionView
    {
        public int Id { get; set; }


        [Required]
        public DateTime? Fecha { get; set; }

        [Required]
        public string NumeroFactura { get; set; }

        [Required]
        public string NombreProveedor { get; set; }

        [Required]
        public string NombreEmpleado { get; set; }

        [Required]
        public decimal Total { get; set; }
    }
}
