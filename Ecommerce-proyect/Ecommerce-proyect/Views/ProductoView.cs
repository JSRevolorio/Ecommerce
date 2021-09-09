 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_proyect.Views
{
    public class ProductoView
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public decimal PrecioConIva { get; set; }
        [Required]
        public decimal PrecioSinIva { get; set; }
        [Required]
        public int? IdCategoria { get; set; }

        public string CategoriaTipo { get; set; }
        [Required]
        public string LinkImagen { get; set; }
        [Required]
        public int Garantia { get; set; }
    }
}
