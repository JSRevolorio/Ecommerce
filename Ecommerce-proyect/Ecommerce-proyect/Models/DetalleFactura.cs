using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Ecommerce_proyect.Models
{
    [Table("DetalleFactura")]
    public partial class DetalleFactura
    {
        [Key]
        public int Id { get; set; }
        public int? IdFactura { get; set; }
        public int? IdProducto { get; set; }
        [Column(TypeName = "decimal(7, 2)")]
        public decimal PrecioConIva { get; set; }
        public int Cantidad { get; set; }

        [ForeignKey(nameof(IdFactura))]
        [InverseProperty(nameof(Factura.DetalleFacturas))]
        public virtual Factura IdFacturaNavigation { get; set; }
        [ForeignKey(nameof(IdProducto))]
        [InverseProperty(nameof(Producto.DetalleFacturas))]
        public virtual Producto IdProductoNavigation { get; set; }
    }
}
