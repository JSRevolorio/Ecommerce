using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Ecommerce_proyect.Views
{
    public class FacturaPagoView
    {
        public int Id { get; set; }


        [Required]

        public int? IdMetodoPago { get; set; }


        [Required]
        public int? IdFactura { get; set; }

        [Required]
        public decimal Monto { get; set; }
     
        [Required]
        public string Referencia { get; set; }
    }
}
