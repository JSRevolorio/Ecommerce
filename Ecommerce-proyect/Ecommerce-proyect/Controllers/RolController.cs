using Ecommerce_proyect.Models;
using Ecommerce_proyect.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private DbContexEcommerce context;

        public RolController(DbContexEcommerce context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetRols()
        {
            try
            {
                var rols = context.Rols.ToList();

                List<RolView> rolViews = new List<RolView>();

                rols.ForEach(rol =>
                {
                    rolViews.Add(new RolView()
                    {
                        Id       = rol.Id,
                        Nombre   = rol.Nombre,
                        Acciones = rol.Acciones
                    });
                });

                return StatusCode((int)HttpStatusCode.OK, rolViews);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdRol([FromRoute] int id)
        {
            try
            {
                var rol = context.Rols.Find(id);

                if (rol != null)
                {
                    var rolView = new RolView()
                    {
                        Id       = rol.Id,
                        Nombre   = rol.Nombre,
                        Acciones = rol.Acciones
                    };

                    return StatusCode((int)HttpStatusCode.OK, rolView);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "El rol no fue encontrado" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CreateCategoria([FromBody] RolView rolView)
        {
            try
            {
                var rol = new Rol()
                {
                    Nombre   = rolView.Nombre,
                    Acciones = rolView.Acciones
                };

                context.Rols.Add(rol);
                context.SaveChanges();

                rolView.Id = rol.Id;

                return StatusCode((int)HttpStatusCode.Created, rolView);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        public IActionResult ModifyCategoria([FromBody] RolView rolView)
        {
            try
            {
                var rol = context.Rols.Find(rolView.Id);

                if (rol != null)
                {
                    rol.Nombre   = rolView.Nombre;
                    rol.Acciones = rolView.Acciones;

                    context.Rols.Update(rol);
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
                var rol = context.Rols.Find(id);

                if (rol != null)
                {
                    context.Rols.Remove(rol);
                    context.SaveChanges();

                    return StatusCode((int)HttpStatusCode.OK, new { mensaje = "La transaccion fue realizada con exito" });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "El rol no fue encontrado" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }
    }
}
