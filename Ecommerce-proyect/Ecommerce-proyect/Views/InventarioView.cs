using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Ecommerce_proyect.Views
{
    public class InventarioView
    {
        public int? IdProducto { get; set; }

        [Required]
        public string NombreProducto { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public int Cantidad { get; set; }
    }
}
