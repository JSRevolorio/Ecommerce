using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Ecommerce_proyect.Models
{
    [Table("DetalleCompra")]
    public partial class DetalleCompra
    {
        [Key]
        public int Id { get; set; }
        public int? IdCompra { get; set; }
        public int? IdProducto { get; set; }
        [Column(TypeName = "decimal(7, 2)")]
        public decimal PrecioUnidadSinIva { get; set; }
        [Column(TypeName = "decimal(7, 2)")]
        public decimal PrecioUnidadConIva { get; set; }
        public int Cantidad { get; set; }

        [ForeignKey(nameof(IdCompra))]
        [InverseProperty(nameof(Compra.DetalleCompras))]
        public virtual Compra IdCompraNavigation { get; set; }
        [ForeignKey(nameof(IdProducto))]
        [InverseProperty(nameof(Producto.DetalleCompras))]
        public virtual Producto IdProductoNavigation { get; set; }
    }
}
