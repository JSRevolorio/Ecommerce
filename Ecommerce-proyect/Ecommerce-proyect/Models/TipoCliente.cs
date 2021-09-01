using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Ecommerce_proyect.Models
{
    [Table("TipoCliente")]
    public partial class TipoCliente
    {
        public TipoCliente()
        {
            Clientes = new HashSet<Cliente>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Tipo { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal? Descuento { get; set; }

        [InverseProperty(nameof(Cliente.IdTipoClienteNavigation))]
        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
