using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Ecommerce_proyect.Models
{
    [Table("Inventario")]
    public partial class Inventario
    {
        [Key]
        public int Id { get; set; }
        public int? IdProducto { get; set; }
        public int? IdTienda { get; set; }
        public int? IdProveedor { get; set; }
        public int Cantidad { get; set; }

        [ForeignKey(nameof(IdProducto))]
        [InverseProperty(nameof(Producto.Inventarios))]
        public virtual Producto IdProductoNavigation { get; set; }
        [ForeignKey(nameof(IdProveedor))]
        [InverseProperty(nameof(Proveedor.Inventarios))]
        public virtual Proveedor IdProveedorNavigation { get; set; }
        [ForeignKey(nameof(IdTienda))]
        [InverseProperty(nameof(Tienda.Inventarios))]
        public virtual Tienda IdTiendaNavigation { get; set; }
    }
}
