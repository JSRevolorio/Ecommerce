using Ecommerce_proyect.Models;
using Ecommerce_proyect.Views;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Ecommerce_proyect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoClienteController : ControllerBase
    {
        private DbContexEcommerce context;

        public TipoClienteController(DbContexEcommerce context)
        {
            this.context = context;
        }

        [HttpGet]

        public IActionResult GetTipoCliente()
        {
            try
            {
                var tipoClientes = context.TipoClientes.ToList();


                List<TipoClienteView> tipoClienteViews = new List<TipoClienteView>();

                tipoClientes.ForEach(tipoCliente =>
                    {
                        tipoClienteViews.Add(new TipoClienteView()
                        {
                            Id = tipoCliente.Id,
                            Tipo = tipoCliente.Tipo,
                            Descuento = tipoCliente.Descuento
                        });
                    });

                return StatusCode((int)HttpStatusCode.OK, tipoClienteViews);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdTipoCliente([FromRoute] int id)
        {
            try
            {
                var tipoCliente = context.TipoClientes.Find(id);

                if (tipoCliente != null)
                {
                    var tipoClienteView = new TipoClienteView()
                    {
                        Id = tipoCliente.Id,
                        Tipo = tipoCliente.Tipo,
                        Descuento = tipoCliente.Descuento
                    };

                    return StatusCode((int)HttpStatusCode.OK, tipoClienteView);

                } else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "El tipo de cliente no fue encontrado" });

                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });

            }
        }

        [HttpPost]
        public IActionResult CreateTipoCliente([FromBody] TipoClienteView tipoClienteView)
        {
            try
            {
                var tipoCliente = new TipoCliente()
                {
                    Id = tipoClienteView.Id,
                    Tipo = tipoClienteView.Tipo,
                    Descuento = tipoClienteView.Descuento
                };

                context.TipoClientes.Add(tipoCliente);
                context.SaveChanges();

                tipoClienteView.Id = tipoCliente.Id;

                return StatusCode((int)HttpStatusCode.Created, tipoClienteView);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]

        public IActionResult ModifyTipoCliente([FromBody] TipoClienteView tipoClienteView)
        {
            try
            {
                var tipoCliente = context.TipoClientes.Find(tipoClienteView.Id);

                if (tipoCliente != null)
                {
                    tipoCliente.Tipo = tipoClienteView.Tipo;
                    tipoCliente.Descuento = tipoClienteView.Descuento;

                    context.TipoClientes.Update(tipoCliente);
                    context.SaveChanges();

                    return StatusCode((int)HttpStatusCode.OK, new { mensaje = "El Tipo de cliente se modificó con éxito" });

                } else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "El tipo de cliente no fue encontrado" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }
        
        [HttpDelete("{id}")]

        public IActionResult DeleteTipoCliente([FromRoute] int id)
        {
            try
            {
                var tipocliente = context.TipoClientes.Find(id);

                if( tipocliente != null)
                {
                    context.TipoClientes.Remove(tipocliente);
                    context.SaveChanges();

                    return StatusCode((int)HttpStatusCode.OK, new { mensaje = "La transaccion fue realizada con éxito" });

                } else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "El tipo de cliente no fue encontrado" });

                }
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });

            }
        }


    }

}
