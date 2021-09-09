using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_proyect.Views
{
    public class CompraDetalleView
    {

        public int Id { get; set; }

        [Required]
        public int? IdProducto { get; set; }

        [Required]
        public decimal PrecioUnidadSinIva { get; set; }
        
        [Required]
        public decimal PrecioUnidadConIva { get; set; }
        
        [Required]
        public int Cantidad { get; set; }

    }
}
