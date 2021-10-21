using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_proyect.Views
{
    public class UsuarioView
    {
        public int Id { get; set; }
        public int? IdEmpleadoRol { get; set; }

        public string RolEmpleado { get; set; }

        [Required]
        [StringLength(255)]
        public string Usuario1 { get; set; }

        [Required]
        [StringLength(255)]
        public string Contraseña { get; set; }

    }
}
