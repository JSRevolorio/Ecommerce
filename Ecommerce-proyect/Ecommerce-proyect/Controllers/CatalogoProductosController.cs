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
                        Id = catalogo.Id,
                        Nombre = catalogo.Nombre,
                        Descripcion = catalogo.Descripcion,
                        PrecioConIva = catalogo.PrecioConIva,
                        LinkImagen = catalogo.Imagen
                    });
                });

                return StatusCode((int)HttpStatusCode.OK, catalogoProductosViews);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex });
            }
        }
    }
}
