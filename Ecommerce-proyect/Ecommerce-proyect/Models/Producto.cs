using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Ecommerce_proyect.Models
{
    [Table("Producto")]
    public partial class Producto
    {
        public Producto()
        {
            DetalleCompras = new HashSet<DetalleCompra>();
            DetalleFacturas = new HashSet<DetalleFactura>();
            Inventarios = new HashSet<Inventario>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(100)]
        public string Descripcion { get; set; }
        [Column("PrecioConIVA", TypeName = "decimal(7, 2)")]
        public decimal PrecioConIva { get; set; }
        [Column("PrecioSinIVA", TypeName = "decimal(7, 2)")]
        public decimal PrecioSinIva { get; set; }
        public int? IdCategoria { get; set; }
        [Required]
        [StringLength(30)]
        public string Imagen { get; set; }
        public int Garantia { get; set; }
        public int? PorcentajeDescuento { get; set; }
        public int? Estado { get; set; }

        [ForeignKey(nameof(IdCategoria))]
        [InverseProperty(nameof(Categoria.Productos))]
        public virtual Categoria Categorias { get; set; }
        [InverseProperty(nameof(DetalleCompra.IdProductoNavigation))]
        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; }
        [InverseProperty(nameof(DetalleFactura.IdProductoNavigation))]
        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; }
        [InverseProperty(nameof(Inventario.IdProductoNavigation))]
        public virtual ICollection<Inventario> Inventarios { get; set; }
    }
}
