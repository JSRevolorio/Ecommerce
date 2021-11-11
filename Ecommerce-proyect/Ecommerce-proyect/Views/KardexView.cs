using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_proyect.Views
{
    public class KardexView
    {
        public string Factura { get; set; }

        public int Id { get; set; }

        public string Producto { get; set; }

        public DateTime? Fecha { get; set; }

        public int Compra { get; set; }

        public int Venta { get; set; }

        public int Inventario { get; set; }
    }
}
