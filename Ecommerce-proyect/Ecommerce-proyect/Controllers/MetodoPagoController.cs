using Microsoft.AspNetCore.Http;
using Ecommerce_proyect.Models;
using Ecommerce_proyect.Views;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace Ecommerce_proyect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetodoPagoController : ControllerBase
    {
        private DbContexEcommerce context;

        public MetodoPagoController(DbContexEcommerce context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetMetodosPago()
        {
            try
            {
                var metodopago = context.MetodoPagos.ToList();

                List<MetodoPagoView> metodoPagoViews = new List<MetodoPagoView>();

                metodopago.ForEach(metodo =>
                {
                    metodoPagoViews.Add(new MetodoPagoView()
                    {
                        Id = metodo.Id,
                        Tipo = metodo.Tipo
                    });
                });

                return StatusCode((int)HttpStatusCode.OK, metodoPagoViews);


            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });

            }

        }


        [HttpGet("{id}")]
        public IActionResult GetByIdMetodoPago([FromRoute] int id)
        {
            try
            {
                var metodopago = context.MetodoPagos.Find(id);

                if (metodopago != null)
                {
                    var metodoPagoView = new MetodoPagoView()
                    {
                        Id = metodopago.Id,
                        Tipo = metodopago.Tipo
                    };

                    return StatusCode((int)HttpStatusCode.OK, metodoPagoView);
                }else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "El rol no fue encontrado" });

                }

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });

            }
        }


        [HttpPut]
        public IActionResult ModifyMetodoPago([FromBody] MetodoPagoView metodoPagoView)
        {
            try
            {
                var metodopago = context.MetodoPagos.Find(metodoPagoView.Id); 

                if (metodopago != null)
                {
                    metodopago.Tipo = metodoPagoView.Tipo;

                    context.MetodoPagos.Update(metodopago);
                    context.SaveChanges();

                    return StatusCode((int)HttpStatusCode.OK, new { mensaje = "La transaccion fue realizada con exito" });

                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "La Categoria no fue encontrado" });

                }

            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });

            }
        }

        [HttpPost]
        public IActionResult CreateMetodoPago([FromBody] MetodoPagoView metodoPagoView)
        {
            try
            {
                var metodopago = new MetodoPago()
                {
                    Tipo = metodoPagoView.Tipo
                };

                context.MetodoPagos.Add(metodopago);
                context.SaveChanges();

                metodoPagoView.Id = metodopago.Id;

                return StatusCode((int)HttpStatusCode.Created, metodoPagoView);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });

            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMetodoPago([FromRoute] int id)
        {
            try
            {
                var metodopago = context.MetodoPagos.Find(id);

                if (metodopago != null)
                {
                    context.MetodoPagos.Remove(metodopago);
                    context.SaveChanges();

                    return StatusCode((int)HttpStatusCode.OK, new { mensaje = "La transaccion fue realizada con exito" });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "El metodo de pago no fue encontrado" });

                }

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }
    }
}
