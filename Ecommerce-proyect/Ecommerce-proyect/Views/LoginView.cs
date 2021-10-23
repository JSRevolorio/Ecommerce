using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Ecommerce_proyect.Views
{
    public class LoginView
    {


        public int Id { get; set; }

        [Required]
        public string Usuario { get; set; }

        [Required]
        public string Contraseña { get; set; }

    }
}
