using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_proyect.Views
{
    public class FacturaView
    {
        public FacturaView()
        {
            facturaDetalles = new List<DetalleFacturaView>();
         
        }
        public int Id { get; set; }
       
        [Required]
        public string Serie { get; set; }
      
        [Required]
        public string Numero { get; set; }

        [Required]
        public DateTime? Fecha { get; set; }

        [Required]
        public int? IdCliente { get; set; }

        [Required]
        public int? IdTienda { get; set; }

        [Required]
        public int? IdEmpleado { get; set; }


        [Required]
        public decimal? Descuento { get; set; }


        [Required]
        public int? IdMetodoPagos { get; set; }

        [Required]
        public List<DetalleFacturaView> facturaDetalles { get; set; }
   
    }
}
