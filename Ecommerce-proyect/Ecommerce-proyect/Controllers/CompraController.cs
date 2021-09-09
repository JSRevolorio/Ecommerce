using Ecommerce_proyect.Models;
using Ecommerce_proyect.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ecommerce_proyect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private DbContexEcommerce context;

        public CompraController(DbContexEcommerce context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetCompra()
        {
            try
            {
                var compras = context.Compras.Include(compra => compra.DetalleCompras).ToList();

                List<CompraPresentacionView> compraPresentacionViews = new List<CompraPresentacionView>();

                compras.ForEach(compra => 
                {
                    var proveedor = context.Proveedors.Find(compra.IdProveedor);
                    var empleado  = context.Empleados.Find(compra.IdEmpleado);

                    compraPresentacionViews.Add(new CompraPresentacionView() 
                    { 
                        Id              = compra.Id,
                        Fecha           = compra.Fecha,
                        NumeroFactura   = compra.NumeroFactura,
                        NombreProveedor = proveedor.Nombre,
                        NombreEmpleado  = empleado.Nombre,
                        Total           = compra.TotalConIva
                    });

                });

                return StatusCode((int)HttpStatusCode.OK, compraPresentacionViews);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CreateCompra([FromBody] CompraView compraView)
        {
            try
            {

                if (compraView.compraDetalles.Count > 0)
                {
                    Compra compra = new Compra()
                    {
                        Fecha         = compraView.Fecha,
                        NumeroFactura = compraView.NumeroFactura,
                        IdProveedor   = compraView.IdProveedor,
                        IdEmpleado    = compraView.IdEmpleado,
                        TotalConIva   = compraView.TotalConIva,
                        Estado        = 1
                    };

                    context.Compras.Add(compra);
                    context.SaveChanges();

                    compraView.Id = compra.Id;

                    compraView.compraDetalles.ForEach(compraDetalleView => 
                    {
                        DetalleCompra detalleCompra = new DetalleCompra()
                        {
                            IdCompra           = compra.Id,
                            IdProducto         = compraDetalleView.IdProducto,
                            PrecioUnidadSinIva = compraDetalleView.PrecioUnidadSinIva,
                            PrecioUnidadConIva = compraDetalleView.PrecioUnidadConIva,
                            Cantidad           = compraDetalleView.Cantidad
                        };

                        context.DetalleCompras.Add(detalleCompra);
                        context.SaveChanges();

                        compraDetalleView.Id = detalleCompra.Id;

                        decimal iva = 0.12M;
                        var producto = context.Productos.Find(compraDetalleView.IdProducto);
                        var cantidadProductoInventario = context.Inventarios.Where(inventario => inventario.IdProducto == producto.Id).Sum(p => p.Cantidad);

                        var precioTotalIva = ((detalleCompra.PrecioUnidadConIva * detalleCompra.Cantidad) + (producto.PrecioConIva * cantidadProductoInventario))/(cantidadProductoInventario + detalleCompra.Cantidad);
                        var precioTotalSinIva =  (precioTotalIva) - (precioTotalIva * iva);


                        Inventario inventario = new Inventario()
                        {
                            IdProducto  = compraDetalleView.IdProducto,
                            IdTienda    =  1,
                            IdProveedor = compra.IdProveedor,
                            Cantidad    = compraDetalleView.Cantidad
                        };

                        context.Inventarios.Add(inventario);
                        context.SaveChanges();


                        producto.PrecioConIva = precioTotalIva;
                        producto.PrecioSinIva = precioTotalSinIva;

                        context.Productos.Update(producto);
                        context.SaveChanges();

                    });
                }
                else 
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "El detalle de la compra no puede estar vacio" });
                }

                return StatusCode((int)HttpStatusCode.Created, compraView);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }
    }
}
