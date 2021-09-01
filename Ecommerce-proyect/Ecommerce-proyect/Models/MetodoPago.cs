using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Ecommerce_proyect.Models
{
    [Table("MetodoPago")]
    public partial class MetodoPago
    {
        public MetodoPago()
        {
            FacturaPagos = new HashSet<FacturaPago>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Tipo { get; set; }

        [InverseProperty(nameof(FacturaPago.IdMetodoPagoNavigation))]
        public virtual ICollection<FacturaPago> FacturaPagos { get; set; }
    }
}
