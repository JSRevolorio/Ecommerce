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
    public class InventarioController : ControllerBase
    {
        private DbContexEcommerce context;

        public InventarioController(DbContexEcommerce context)
        {
            this.context = context;
        }


        [HttpGet]
        public IActionResult GetInventario()
        {
            try
            {
                var inventarioViews = (from producto in context.Productos
                                 join inventario in context.Inventarios on producto.Id equals inventario.IdProducto
                                 where producto.Estado == 1
                                 group inventario by new
                                 {
                                     producto.Id,
                                     producto.Nombre,
                                     producto.Descripcion,
                                     producto.PrecioConIva,
                                     producto.Imagen
                                 }
                                 into g
                                 select new InventarioView()
                                 {

                                   IdProducto     = g.Key.Id,
                                   NombreProducto = g.Key.Nombre,					
		                           Descripcion  = g.Key.Descripcion,									
		                           Cantidad	    = g.Sum(inv => inv.Cantidad),
                                 }).ToList();

 


                return StatusCode((int)HttpStatusCode.OK, inventarioViews);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex });
            }

        }

        [HttpGet("id={id}&mes={mes}&ano={ano}")]
        public IActionResult GetKardex([FromRoute] int id, int mes, int ano)
        {
            try
            {
                List<KardexView> kardexViews = new List<KardexView>();

                var cantidadCompra = (from compra in context.Compras.Where(p => ((DateTime)p.Fecha).Year <= ano && ((DateTime)p.Fecha).Month < mes && p.Estado == 1)
                            join detalleCompra in context.DetalleCompras.Where(d => d.IdProducto == id) on compra.Id equals detalleCompra.IdCompra
                            select detalleCompra.Cantidad).Sum();

                var cantidadVenta = (from factura in context.Facturas.Where(p => ((DateTime)p.Fecha).Year <= ano && ((DateTime)p.Fecha).Month < mes && p.Estado == 1)
                                      join detalleFactura in context.DetalleFacturas.Where(d => d.IdProducto == id) on factura.Id equals detalleFactura.IdFactura
                                      select detalleFactura.Cantidad).Sum();


                var KCompras = from producto in context.Productos.Where(p => p.Id == id)
                             join detalleCompra in context.DetalleCompras on producto.Id equals detalleCompra.IdProducto
                             join compra in context.Compras.Where(p => ((DateTime)p.Fecha).Year == ano && ((DateTime)p.Fecha).Month == mes && p.Estado == 1) on detalleCompra.IdCompra equals compra.Id
                             select new KardexView() 
                             {
                                 Factura    = compra.NumeroFactura,
                                 Id         = producto.Id,
                                 Producto   = producto.Nombre,
                                 Fecha      = compra.Fecha,
                                 Compra     = detalleCompra.Cantidad,
                                 Venta      = 0,
                                 Inventario = 0,
                             };

                var KVentas = (from producto   in context.Productos.Where(p => p.Id == id)
                             join detalleFactura in context.DetalleFacturas on producto.Id equals detalleFactura.IdProducto
                             join factura in context.Facturas.Where(p => ((DateTime)p.Fecha).Year == ano && ((DateTime)p.Fecha).Month == mes && p.Estado == 1) on detalleFactura.IdFactura equals factura.Id
                             select new KardexView() 
                             {
                                 Factura    = factura.Numero,
                                 Id         = producto.Id,
                                 Producto   = producto.Nombre,
                                 Fecha      = factura.Fecha,
                                 Compra     = 0,
                                 Venta      = detalleFactura.Cantidad,
                                 Inventario = 0,
                             }).ToList();

                kardexViews.AddRange(KCompras);
                kardexViews.AddRange(KVentas);

                int cantidad = (cantidadCompra - cantidadVenta);
                kardexViews.ForEach(inv => 
                {
                    cantidad = ((cantidad + inv.Compra) - inv.Venta);

                    inv.Inventario = cantidad;
                
                });

                return StatusCode((int)HttpStatusCode.OK, kardexViews);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex });
            }

        }


        [HttpGet("{product}")]
        public IActionResult GetInventarioProductoEspecifico([FromRoute] string product)
        {
            try
            {
                var inventarios = (from producto in context.Productos
                                   join Inventario in context.Inventarios on producto.Id equals Inventario.IdProducto
                                   where producto.Nombre.Contains(product)
                                   select new
                                   {
                                       producto.Id,
                                       producto.Nombre,
                                       producto.Descripcion,
                                       Inventario.Cantidad,
                                   }).ToList();
                List<InventarioView> inventarioViews = new List<InventarioView>();


                inventarios.ForEach(inventario =>
                {

                    inventarioViews.Add(new InventarioView()
                    {
                        IdProducto = inventario.Id,
                        NombreProducto = inventario.Nombre,
                        Descripcion = inventario.Descripcion,
                        Cantidad = inventario.Cantidad

                    });
                });

                return StatusCode((int)HttpStatusCode.OK, inventarioViews);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex });
            }

        }

    }
}
