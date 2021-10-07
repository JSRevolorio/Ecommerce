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
                var inventarios = (from inventario in context.Inventarios
                                   join producto in context.Productos on inventario.IdProducto equals producto.Id

                                   select new
                                   {
                                       producto.Id,
                                       producto.Nombre,
                                       producto.Descripcion,
                                       inventario.Cantidad,
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
