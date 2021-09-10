using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Ecommerce_proyect.Views
{
    public class CatalogoProductosView
    {

        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public decimal PrecioConIva { get; set; }
        [Required]
        public string LinkImagen { get; set; }
    }
}
