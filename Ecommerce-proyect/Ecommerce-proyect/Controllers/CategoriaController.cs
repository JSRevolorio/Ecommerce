using Ecommerce_proyect.Models;
using Ecommerce_proyect.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ecommerce_proyect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private DbContexEcommerce context;

        public CategoriaController(DbContexEcommerce context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetCategoria()
        {
            try
            {
                var categorias = context.Categorias.ToList();

                List<CategoriaView> categoriaViews = new List<CategoriaView>();
                
                categorias.ForEach(categoria => 
                {
                    categoriaViews.Add(new CategoriaView() 
                    {
                        Id     = categoria.Id,
                        Tipo   = categoria.Tipo, 
                        Marca  = categoria.Marca,
                        Modelo = categoria.Modelo
                    });
                });

                return StatusCode((int)HttpStatusCode.OK, categoriaViews);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdCategoria([FromRoute] int id)
        {
            try
            {
                var categoria = context.Categorias.Find(id);

                if (categoria != null)
                {
                    var categoriaView = new CategoriaView()
                    {
                        Id     = categoria.Id,
                        Tipo   = categoria.Tipo,
                        Marca  = categoria.Marca,
                        Modelo = categoria.Modelo
                    };

                    return StatusCode((int)HttpStatusCode.OK, categoriaView);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "La categoria no fue encontrado" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CreateCategoria([FromBody] CategoriaView CategoriaView)
        {
            try
            {
                var categoria = new Categoria()
                {
                    Tipo   = CategoriaView.Tipo,
                    Marca  = CategoriaView.Marca,
                    Modelo = CategoriaView.Modelo
                };

                context.Categorias.Add(categoria);
                context.SaveChanges();

                CategoriaView.Id = categoria.Id;

                return StatusCode((int)HttpStatusCode.Created, CategoriaView);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        public IActionResult ModifyCategoria([FromBody] CategoriaView CategoriaView)
        {
            try
            {
                var categoria = context.Categorias.Find(CategoriaView.Id);

                if (categoria != null)
                {
                    categoria.Tipo   = CategoriaView.Tipo;
                    categoria.Marca  = CategoriaView.Marca;
                    categoria.Modelo = CategoriaView.Modelo;

                    context.Categorias.Update(categoria);
                    context.SaveChanges();

                    return StatusCode((int)HttpStatusCode.OK, new { mensaje = "La transaccion fue realizada con exito" });

                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "La Categoria no fue encontrado" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategoria([FromRoute] int id)
        {
            try
            {
                var categoria = context.Categorias.Find(id);

                if (categoria != null)
                {
                    context.Categorias.Remove(categoria);
                    context.SaveChanges();

                    return StatusCode((int)HttpStatusCode.OK, new { mensaje = "La transaccion fue realizada con exito" });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "El proveedor no fue encontrado" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }
    }
}
