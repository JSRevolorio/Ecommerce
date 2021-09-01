using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Ecommerce_proyect.Models
{
    [Table("Proveedor")]
    public partial class Proveedor
    {
        public Proveedor()
        {
            Compras = new HashSet<Compra>();
            Inventarios = new HashSet<Inventario>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(12)]
        public string Telefono { get; set; }
        [Required]
        [StringLength(40)]
        public string Direccion { get; set; }
        [Required]
        [StringLength(50)]
        public string Correo { get; set; }
        public int? Estado { get; set; }

        [InverseProperty(nameof(Compra.IdProveedorNavigation))]
        public virtual ICollection<Compra> Compras { get; set; }
        [InverseProperty(nameof(Inventario.IdProveedorNavigation))]
        public virtual ICollection<Inventario> Inventarios { get; set; }
    }
}
