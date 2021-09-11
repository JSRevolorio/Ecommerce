using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_proyect.Models;
using Ecommerce_proyect.Views;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_proyect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoProductosController : ControllerBase
    {
        private DbContexEcommerce context;

        public CatalogoProductosController(DbContexEcommerce context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetCatalogoDeProductos()
        {
            try
            {

                var catalogoP = context.Productos.Where(producto => producto.Estado == 1).Take(30).ToList();


                List<CatalogoProductosView> catalogoProductosViews = new List<CatalogoProductosView>();

                catalogoP.ForEach(catalogo =>
                {
                    catalogoProductosViews.Add(new CatalogoProductosView()
                    {
                        Id            = catalogo.Id,
                        Nombre        = catalogo.Nombre,
                        Descripcion   = catalogo.Descripcion,
                        PrecioConIva  = catalogo.PrecioConIva,
                        LinkImagen    = catalogo.Imagen
                    });
                });

                return StatusCode((int)HttpStatusCode.OK, catalogoProductosViews);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex });
            }
        }



        [HttpGet("{search}")]
        public IActionResult SearchProductos([FromRoute] string search)
        {
            try
            {
                var catalogoBusqueda = context.Productos.Include(producto => producto.Categorias).Where(producto => producto.Categorias.Marca == search && producto.Estado == 1 || producto.Categorias.Tipo == search && producto.Estado == 1 || producto.Categorias.Modelo == search && producto.Estado == 1).ToList();

                List<CatalogoProductosView> catalogoProductosViews = new List<CatalogoProductosView>();

               if( catalogoBusqueda.Count != 0)
                {
                    catalogoBusqueda.ForEach(productoBusqueda =>
                    {
                        catalogoProductosViews.Add(new CatalogoProductosView()
                        {
                            Id              = productoBusqueda.Id,
                            Nombre          = productoBusqueda.Nombre,
                            Descripcion     = productoBusqueda.Descripcion,
                            PrecioConIva    = productoBusqueda.PrecioConIva,
                            LinkImagen      = productoBusqueda.Imagen
                        });

                    });

                    return StatusCode((int)HttpStatusCode.OK, catalogoProductosViews);
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
