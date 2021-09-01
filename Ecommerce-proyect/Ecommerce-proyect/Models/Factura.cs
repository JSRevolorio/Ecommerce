using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Ecommerce_proyect.Models
{
    [Table("Factura")]
    public partial class Factura
    {
        public Factura()
        {
            DetalleFacturas = new HashSet<DetalleFactura>();
            FacturaPagos = new HashSet<FacturaPago>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string Serie { get; set; }
        [Required]
        [StringLength(255)]
        public string Numero { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Fecha { get; set; }
        public int? IdCliente { get; set; }
        public int? IdTienda { get; set; }
        public int? IdEmpleado { get; set; }
        public int? IdImpuesto { get; set; }
        [Column(TypeName = "decimal(7, 2)")]
        public decimal? Descuento { get; set; }
        public int? Estado { get; set; }

        [ForeignKey(nameof(IdCliente))]
        [InverseProperty(nameof(Cliente.Facturas))]
        public virtual Cliente IdClienteNavigation { get; set; }
        [ForeignKey(nameof(IdEmpleado))]
        [InverseProperty(nameof(Empleado.Facturas))]
        public virtual Empleado IdEmpleadoNavigation { get; set; }
        [ForeignKey(nameof(IdImpuesto))]
        [InverseProperty(nameof(Impuesto.Facturas))]
        public virtual Impuesto IdImpuestoNavigation { get; set; }
        [ForeignKey(nameof(IdTienda))]
        [InverseProperty(nameof(Tienda.Facturas))]
        public virtual Tienda IdTiendaNavigation { get; set; }
        [InverseProperty(nameof(DetalleFactura.IdFacturaNavigation))]
        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; }
        [InverseProperty(nameof(FacturaPago.IdFacturaNavigation))]
        public virtual ICollection<FacturaPago> FacturaPagos { get; set; }
    }
}
