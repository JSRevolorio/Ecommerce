using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_proyect.Views
{
    public class EmailViews
    {

        public EmailViews() 
        {
            Productos = new List<ProductoEmail>();
        }

        [Required]
        public string Serie { get; set; }

        [Required]
        public string Numero { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public string Nit { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required]
        public string Correo { get; set; }

        [Required]
        public string MetodoPago { get; set; }

        [Required]
        public List<ProductoEmail> Productos { get; set; }

    }

    public class ProductoEmail
    {
        [Required]
        public int IdProducto { get; set; }

        [Required]
        public string NombreProducto { get; set; }

        [Required]
        public double Precio { get; set; }

        [Required]
        public int Cantidad { get; set; }
    }
}
