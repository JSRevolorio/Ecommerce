using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Ecommerce_proyect.Models
{
    [Table("Tienda")]
    public partial class Tienda
    {
        public Tienda()
        {
            Facturas = new HashSet<Factura>();
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
        [StringLength(60)]
        public string Direccion { get; set; }
        public int? Estado { get; set; }

        [InverseProperty(nameof(Factura.IdTiendaNavigation))]
        public virtual ICollection<Factura> Facturas { get; set; }
        [InverseProperty(nameof(Inventario.IdTiendaNavigation))]
        public virtual ICollection<Inventario> Inventarios { get; set; }
    }
}
