using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Ecommerce_proyect.Models
{
    [Table("RecuperarContraseña")]
    public partial class RecuperarContraseña
    {
        [Key]
        public int Id { get; set; }
        public int? IdCliente { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Fecha { get; set; }
        public TimeSpan? Tiempo { get; set; }

        [ForeignKey(nameof(IdCliente))]
        [InverseProperty(nameof(Cliente.RecuperarContraseñas))]
        public virtual Cliente IdClienteNavigation { get; set; }
    }
}
