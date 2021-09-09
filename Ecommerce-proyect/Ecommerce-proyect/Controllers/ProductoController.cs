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


namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private DbContexEcommerce context;

        public ProductoController(DbContexEcommerce context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetProducto()
        {
            try
            {
                var productos = context.Productos.Include(producto => producto.Categorias).Where(producto => producto.Estado == 1).ToList();


                List<ProductoView> productoViews = new List<ProductoView>();

                productos.ForEach(producto =>
                {
                    productoViews.Add(new ProductoView()
                    {
                        Id            = producto.Id,
                        Nombre        = producto.Nombre,
                        Descripcion   = producto.Descripcion,
                        PrecioConIva  = producto.PrecioConIva,
                        PrecioSinIva  = producto.PrecioSinIva,
                        IdCategoria   = producto.IdCategoria,
                        CategoriaTipo = producto.Categorias.Tipo,
                        LinkImagen    = producto.Imagen,
                        Garantia      = producto.Garantia
                    });
                });

                return StatusCode((int)HttpStatusCode.OK, productoViews);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdProducto([FromRoute] int id)
        {
            try
            {
                var producto = context.Productos.Include(producto => producto.Categorias).Where(producto => producto.Id == id).FirstOrDefault();

                if (producto != null)
                {
                    var productoView = new ProductoView()
                    {
                        Id            = producto.Id,
                        Nombre        = producto.Nombre,
                        Descripcion   = producto.Descripcion,
                        PrecioConIva  = producto.PrecioConIva,
                        PrecioSinIva  = producto.PrecioSinIva,
                        IdCategoria   = producto.IdCategoria,
                        CategoriaTipo = producto.Categorias.Tipo,
                        LinkImagen    = producto.Imagen,
                        Garantia      = producto.Garantia
                    };

                    return StatusCode((int)HttpStatusCode.OK, productoView);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "El producto no fue encontrado" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CreateProducto([FromBody] ProductoView productoView)
        {
            try
            {
                var producto = new Producto()
                {
                    Nombre              = productoView.Nombre,
                    Descripcion         = productoView.Descripcion,
                    PrecioConIva        = productoView.PrecioConIva,
                    PrecioSinIva        = productoView.PrecioSinIva,
                    IdCategoria         = productoView.IdCategoria,
                    Imagen              = productoView.LinkImagen,
                    Garantia            = productoView.Garantia,
                    PorcentajeDescuento = 0,
                    Estado              = 1
                };

                context.Productos.Add(producto);
                context.SaveChanges();


                productoView.Id = producto.Id;


                return StatusCode((int)HttpStatusCode.Created, productoView);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        public IActionResult ModifyProducto([FromBody] ProductoView productoView)
        {
            try
            {
                var producto = context.Productos.Find(productoView.Id);

                if (producto != null)
                {
                    producto.Nombre              = productoView.Nombre;
                    producto.Descripcion         = productoView.Descripcion;
                    producto.PrecioConIva        = productoView.PrecioConIva;
                    producto.PrecioSinIva        = productoView.PrecioSinIva;
                    producto.IdCategoria         = productoView.IdCategoria;
                    producto.Imagen              = productoView.LinkImagen;
                    producto.Garantia            = productoView.Garantia;

                    context.Productos.Update(producto);
                    context.SaveChanges();

                    return StatusCode((int)HttpStatusCode.OK, new { mensaje = "La transaccion fue realizada con exito" });

                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "El producto no fue encontrado" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProducto([FromRoute] int id)
        {
            try
            {
                var producto = context.Productos.Find(id);

                if (producto != null)
                {
                    producto.Estado = 0;
                    context.Productos.Update(producto);
                    context.SaveChanges();

                    return StatusCode((int)HttpStatusCode.OK, new { mensaje = "La transaccion fue realizada con exito" });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "El producto no fue encontrado" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }
    }
}
