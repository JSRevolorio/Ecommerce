using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Ecommerce_proyect.Models
{
    [Table("EmpleadoRol")]
    public partial class EmpleadoRol
    {
        public EmpleadoRol()
        {
            Usuarios = new HashSet<Usuario>();
        }

        [Key]
        public int Id { get; set; }
        public int? IdEmpleado { get; set; }
        public int? IdRol { get; set; }

        [ForeignKey(nameof(IdEmpleado))]
        [InverseProperty(nameof(Empleado.EmpleadoRols))]
        public virtual Empleado Empleados { get; set; }
        [ForeignKey(nameof(IdRol))]
        [InverseProperty(nameof(Rol.EmpleadoRols))]
        public virtual Rol Rols { get; set; }
        [InverseProperty(nameof(Usuario.IdEmpleadoRolNavigation))]
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
