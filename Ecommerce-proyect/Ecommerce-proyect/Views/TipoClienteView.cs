using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Ecommerce_proyect.Views
{
    public class TipoClienteView
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Tipo { get; set; }

        [Required]
        public decimal? Descuento { get; set; }

        //agregar decimal ?
    }

}
