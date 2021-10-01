using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_proyect.Views
{
    public class MetodoPagoView
    {

       
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Tipo { get; set; }




    }
}
