using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Ecommerce_proyect.Models
{
    [Table("Impuesto")]
    public partial class Impuesto
    {
        public Impuesto()
        {
            Facturas = new HashSet<Factura>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        public int Porcentaje { get; set; }
        public int? Estado { get; set; }

        [InverseProperty(nameof(Factura.IdImpuestoNavigation))]
        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
