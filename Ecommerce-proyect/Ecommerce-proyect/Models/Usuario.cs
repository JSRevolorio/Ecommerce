using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Ecommerce_proyect.Models
{
    [Table("Usuario")]
    public partial class Usuario
    {
        [Key]
        public int Id { get; set; }
        public int? IdEmpleadoRol { get; set; }
        [Required]
        [Column("Usuario")]
        [StringLength(255)]
        public string Usuario1 { get; set; }
        [Required]
        [StringLength(255)]
        public string Contraseña { get; set; }

        [ForeignKey(nameof(IdEmpleadoRol))]
        [InverseProperty(nameof(EmpleadoRol.Usuarios))]
        public virtual EmpleadoRol IdEmpleadoRolNavigation { get; set; }
    }
}
