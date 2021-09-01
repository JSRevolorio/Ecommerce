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
    public class TiendaController : ControllerBase
    {
        private DbContexEcommerce context;

        public TiendaController(DbContexEcommerce context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetTienda()
        {
            try
            {
                var tiendas = context.Tiendas.Where(tienda => tienda.Estado == 1).ToList();

                List<TiendaView> tiendaViews = new List<TiendaView>();

                tiendas.ForEach(tienda => 
                {
                    tiendaViews.Add(new TiendaView()
                    { 
                        Id        = tienda.Id,
                        Nombre    = tienda.Nombre,
                        Telefono  = tienda.Telefono,
                        Direccion = tienda.Direccion
                    });


                });

                return StatusCode((int)HttpStatusCode.OK, tiendaViews);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdTienda([FromRoute] int id)
        {
            try
            {
                var tienda = context.Tiendas.Find(id);

                if (tienda != null)
                {
                    var tiendaView = new TiendaView()
                    {
                        Id        = tienda.Id,
                        Nombre    = tienda.Nombre,
                        Telefono  = tienda.Telefono,
                        Direccion = tienda.Direccion,
                    };

                    return StatusCode((int)HttpStatusCode.OK, tiendaView);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "La tienda no fue encontrado" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CreateTienda([FromBody] TiendaView tiendaView)
        {
            try
            {
                var tienda = new Tienda()
                {
                    Nombre    = tiendaView.Nombre,
                    Telefono  = tiendaView.Telefono,
                    Direccion = tiendaView.Direccion,
                    Estado = 1
                };

                context.Tiendas.Add(tienda);
                context.SaveChanges();

                tiendaView.Id = tienda.Id;

                return StatusCode((int)HttpStatusCode.Created, tiendaView);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        public IActionResult ModifyTienda([FromBody] TiendaView tiendaView)
        {
            try
            {
                var tienda = context.Tiendas.Find(tiendaView.Id);

                if (tienda != null)
                {
                    tienda.Nombre    = tiendaView.Nombre;
                    tienda.Telefono  = tiendaView.Telefono;
                    tienda.Direccion = tiendaView.Direccion;

                    context.Tiendas.Update(tienda);
                    context.SaveChanges();

                    return StatusCode((int)HttpStatusCode.OK, new { mensaje = "La transaccion fue realizada con exito" });

                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "La tienda no fue encontrado" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTienda([FromRoute] int id)
        {
            try
            {
                var tienda = context.Tiendas.Find(id);

                if (tienda != null)
                {
                    tienda.Estado = 0;
                    context.Tiendas.Update(tienda);
                    context.SaveChanges();

                    return StatusCode((int)HttpStatusCode.OK, new { mensaje = "La transaccion fue realizada con exito" });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "La tienda no fue encontrado" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }
    }
}
