using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_proyect.Views
{
    public class CategoriaView
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Tipo { get; set; }

        [Required]
        [StringLength(30)]
        public string Marca { get; set; }

        [Required]
        [StringLength(30)]
        public string Modelo { get; set; }
    }
}
