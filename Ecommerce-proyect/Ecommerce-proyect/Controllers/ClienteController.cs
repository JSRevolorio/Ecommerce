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
    public class ClienteController : ControllerBase
    {
        private DbContexEcommerce context;

        public ClienteController(DbContexEcommerce context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult GetCliente()
        {
            try
            {
                var clientes = context.Clientes.Include(cliente => cliente.IdTipoClienteNavigation).Where(cliente => cliente.Estado == 1).ToList();

                List<ClienteView> clienteViews = new List<ClienteView>();

                clientes.ForEach(cliente =>
                {
                    clienteViews.Add(new ClienteView()
                    {
                        Id            = cliente.Id,
                        Nombre        = cliente.Nombre,
                        Apellido      = cliente.Apellido,
                        Telefono      = cliente.Telefono,
                        Correo        = cliente.Correo,
                        Direccion     = cliente.Direccion,
                        Nit           = cliente.Nit,
                        Usuario       = cliente.Usuario,
                        Contraseña    = cliente.Contraseña,
                        IdTipoCliente = cliente.IdTipoCliente,
                        ClienteTipo   = cliente.IdTipoClienteNavigation.Tipo

                    });
                });

                return StatusCode((int)HttpStatusCode.OK, clienteViews);

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });

            }
        }
        [HttpGet("{id}")]

        public IActionResult GetByIdCliente([FromRoute] int id)
        {
            try
            {
                var cliente = context.Clientes.Include(cliente => cliente.IdTipoClienteNavigation).Where(cliente => cliente.Id == id).FirstOrDefault();

                if (cliente != null)
                {
                    var clienteView = new ClienteView()
                    {
                        Id = cliente.Id,
                        Nombre = cliente.Nombre,
                        Apellido = cliente.Apellido,
                        Telefono = cliente.Telefono,
                        Correo = cliente.Correo,
                        Direccion = cliente.Direccion,
                        Nit = cliente.Nit,
                        Usuario = cliente.Usuario,
                        Contraseña = cliente.Contraseña,
                        IdTipoCliente = cliente.IdTipoCliente,
                        ClienteTipo = cliente.IdTipoClienteNavigation.Tipo
                    };

                    return StatusCode((int)HttpStatusCode.OK, clienteView);

                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "El cliente no fue encontrado" });

                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });

            }
        }

        [HttpPost]
        public IActionResult CreateCliente([FromBody] ClienteView clienteView)
        {
            try
            {
                var cliente = new Cliente()
                {
                    Id = clienteView.Id,
                    Nombre = clienteView.Nombre,
                    Apellido = clienteView.Apellido,
                    Telefono = clienteView.Telefono,
                    Correo = clienteView.Correo,
                    Direccion = clienteView.Direccion,
                    Nit = clienteView.Nit,
                    Usuario = clienteView.Usuario,
                    Contraseña = clienteView.Contraseña,
                    IdTipoCliente = clienteView.IdTipoCliente,
                    Estado = 1
                };

                context.Clientes.Add(cliente);
                context.SaveChanges();

                clienteView.Id = cliente.Id;

                return StatusCode((int)HttpStatusCode.Created, clienteView);

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });

            }
        }

        [HttpPut]
        public IActionResult ModifyCliente([FromBody] ClienteView clienteView)
        {
            try
            {
                var cliente = context.Clientes.Find(clienteView.Id);

                if (cliente != null)
                {
                    cliente.Nombre          = clienteView.Nombre;
                    cliente.Apellido        = clienteView.Apellido;
                    cliente.Telefono        = clienteView.Telefono;
                    cliente.Correo          = clienteView.Correo;
                    cliente.Direccion       = clienteView.Direccion;
                    cliente.Nit             = clienteView.Nit;
                    cliente.Usuario         = clienteView.Usuario;
                    cliente.Contraseña      = clienteView.Contraseña;
                    cliente.IdTipoCliente   = clienteView.IdTipoCliente;

                    context.Clientes.Update(cliente);
                    context.SaveChanges();

                    return StatusCode((int)HttpStatusCode.OK, new { mensaje = "La modificación fue realizada con éxito" });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "El cliente no fue encontrado" });

                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });

            }

        }

        [HttpDelete("{id}")]

        public IActionResult DeleteCliente([FromRoute] int id)
        {
            try
            {
                var cliente = context.Clientes.Find(id);

                if(cliente != null)
                {
                    cliente.Estado = 0;
                    context.Clientes.Update(cliente);
                    context.SaveChanges();

                    return StatusCode((int)HttpStatusCode.OK, new { mensaje = "La transaccion fue realizada con exito" });

                } 
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "El cliente no fue encontrado" });

                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }



    }
}
