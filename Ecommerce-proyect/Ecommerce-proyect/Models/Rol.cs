using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Ecommerce_proyect.Models
{
    [Table("Rol")]
    public partial class Rol
    {
        public Rol()
        {
            EmpleadoRols = new HashSet<EmpleadoRol>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [Column("Rol")]
        [StringLength(40)]
        public string Nombre { get; set; }
        public int Acciones { get; set; }

        [InverseProperty(nameof(EmpleadoRol.Rols))]
        public virtual ICollection<EmpleadoRol> EmpleadoRols { get; set; }
    }
}
