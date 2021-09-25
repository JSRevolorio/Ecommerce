using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Ecommerce_proyect.Views
{
    public class FacturaPresentacionView
    {
        public int Id { get; set; }

        [Required]
        public string Serie { get; set; }

        [Required]
        public string NumeroFactura { get; set; }

        [Required]
        public DateTime? Fecha { get; set; }

        [Required]
        public string NombreCliente { get; set; }

        [Required]
        public string Nit { get; set; }

        [Required]
        public string NombreTienda { get; set; }

        public string DireccionTienda { get; set; }

        [Required]
        public string NombreEmpleado { get; set; }

        [Required]
        public List<DetalleFacturaView> Productos { get; set; }

        [Required]
        public decimal Monto { get; set; }
    }
}
