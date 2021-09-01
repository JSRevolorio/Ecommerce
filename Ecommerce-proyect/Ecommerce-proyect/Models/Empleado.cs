using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Ecommerce_proyect.Models
{
    [Table("Empleado")]
    [Index(nameof(Nit), Name = "UQ__Empleado__C7D1D6DA75B126A1", IsUnique = true)]
    public partial class Empleado
    {
        public Empleado()
        {
            Compras = new HashSet<Compra>();
            EmpleadoRols = new HashSet<EmpleadoRol>();
            Facturas = new HashSet<Factura>();
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
        public int? Estado { get; set; }

        [InverseProperty(nameof(Compra.IdEmpleadoNavigation))]
        public virtual ICollection<Compra> Compras { get; set; }
        [InverseProperty(nameof(EmpleadoRol.Empleados))]
        public virtual ICollection<EmpleadoRol> EmpleadoRols { get; set; }
        [InverseProperty(nameof(Factura.IdEmpleadoNavigation))]
        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
