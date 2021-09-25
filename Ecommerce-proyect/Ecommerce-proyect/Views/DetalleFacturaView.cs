using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Ecommerce_proyect.Views
{
    public class DetalleFacturaView
    {
        public int Id { get; set; }

        [Required]
        public int? IdProducto { get; set; }

      
        public decimal PrecioConIva { get; set; }

        [Required]
        public int Cantidad { get; set; }

    }
}
