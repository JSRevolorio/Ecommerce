using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Ecommerce_proyect.Models
{
    [Table("Cliente")]
    [Index(nameof(Nit), Name = "UQ__Cliente__C7D1D6DA6F38F1E7", IsUnique = true)]
    public partial class Cliente
    {
        public Cliente()
        {
            Facturas = new HashSet<Factura>();
            RecuperarContraseñas = new HashSet<RecuperarContraseña>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(40)]
        public string Apellido { get; set; }
        [Required]
        [StringLength(12)]
        public string Telefono { get; set; }
        [Required]
        [StringLength(50)]
        public string Correo { get; set; }
        [Required]
        [StringLength(50)]
        public string Direccion { get; set; }
        [Required]
        [StringLength(15)]
        public string Nit { get; set; }
        [Required]
        [StringLength(255)]
        public string Usuario { get; set; }
        [Required]
        [StringLength(255)]
        public string Contraseña { get; set; }
        public int? IdTipoCliente { get; set; }
        public int? Estado { get; set; }

        [ForeignKey(nameof(IdTipoCliente))]
        [InverseProperty(nameof(TipoCliente.Clientes))]
        public virtual TipoCliente IdTipoClienteNavigation { get; set; }
        [InverseProperty(nameof(Factura.IdClienteNavigation))]
        public virtual ICollection<Factura> Facturas { get; set; }
        [InverseProperty(nameof(RecuperarContraseña.IdClienteNavigation))]
        public virtual ICollection<RecuperarContraseña> RecuperarContraseñas { get; set; }
    }
}
