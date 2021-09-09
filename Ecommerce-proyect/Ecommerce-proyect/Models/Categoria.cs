using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Ecommerce_proyect.Models
{
    [Table("Categoria")]
    public partial class Categoria
    {
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Tipo { get; set; }
        [Required]
        [StringLength(30)]
        public string Marca { get; set; }
        [Required]
        [StringLength(30)]
        public string Modelo { get; set; }

        [InverseProperty(nameof(Producto.Categorias))]
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
