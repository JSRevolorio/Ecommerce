﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_proyect.Views
{
    public class CompraView
    {
        public CompraView()
        {
            compraDetalles = new List<CompraDetalleView>();
        }

        public int Id { get; set; }

        [Required]
        public DateTime? Fecha { get; set; }

        [Required]
        public string NumeroFactura { get; set; }

        [Required]
        public int? IdProveedor { get; set; }

        [Required]
        public int? IdEmpleado { get; set; }

        [Required]
        public decimal TotalConIva { get; set; }

        [Required]
        public List<CompraDetalleView> compraDetalles { get; set; }
    }
}
