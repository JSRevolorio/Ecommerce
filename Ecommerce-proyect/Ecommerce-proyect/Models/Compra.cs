using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Ecommerce_proyect.Models
{
    [Table("Compra")]
    public partial class Compra
    {
        public Compra()
        {
            DetalleCompras = new HashSet<DetalleCompra>();
        }

        [Key]
        public int Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Fecha { get; set; }
        [Required]
        [StringLength(255)]
        public string NumeroFactura { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdEmpleado { get; set; }
        [Column(TypeName = "decimal(7, 2)")]
        public decimal TotalConIva { get; set; }
        public int? Estado { get; set; }

        [ForeignKey(nameof(IdEmpleado))]
        [InverseProperty(nameof(Empleado.Compras))]
        public virtual Empleado IdEmpleadoNavigation { get; set; }
        [ForeignKey(nameof(IdProveedor))]
        [InverseProperty(nameof(Proveedor.Compras))]
        public virtual Proveedor IdProveedorNavigation { get; set; }
        [InverseProperty(nameof(DetalleCompra.IdCompraNavigation))]
        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; }
    }
}
