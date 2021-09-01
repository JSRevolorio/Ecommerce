using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Ecommerce_proyect.Models
{
    [Table("FacturaPago")]
    public partial class FacturaPago
    {
        [Key]
        public int Id { get; set; }
        public int? IdMetodoPago { get; set; }
        public int? IdFactura { get; set; }
        [Column(TypeName = "decimal(7, 2)")]
        public decimal Monto { get; set; }
        [Required]
        [StringLength(50)]
        public string Referencia { get; set; }

        [ForeignKey(nameof(IdFactura))]
        [InverseProperty(nameof(Factura.FacturaPagos))]
        public virtual Factura IdFacturaNavigation { get; set; }
        [ForeignKey(nameof(IdMetodoPago))]
        [InverseProperty(nameof(MetodoPago.FacturaPagos))]
        public virtual MetodoPago IdMetodoPagoNavigation { get; set; }
    }
}
