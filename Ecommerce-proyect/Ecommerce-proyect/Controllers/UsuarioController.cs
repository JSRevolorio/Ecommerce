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
    public class UsuarioController : ControllerBase
    {
        private DbContexEcommerce context;

        public UsuarioController(DbContexEcommerce context)
        {
            this.context = context;
        }

        [HttpGet]

        public IActionResult GetUsuario()
        {
            try
            {
                var usuarios = context.Usuarios.Include(user => user.IdEmpleadoRolNavigation.Rols).ToList();

                List<UsuarioView> usuarioViews = new List<UsuarioView>();

                usuarios.ForEach(user =>
                {
                    usuarioViews.Add(new UsuarioView()
                    {
                        Id = user.Id,
                        IdEmpleadoRol = user.IdEmpleadoRol,
                        RolEmpleado = user.IdEmpleadoRolNavigation.Rols.Nombre,
                        Usuario1 = user.Usuario1,
                        Contraseña = user.Contraseña

                    });
                });

                return StatusCode((int)HttpStatusCode.OK, usuarioViews);

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });

            }
        }

        [HttpGet("{id}")]

        public IActionResult GetByIdUsuario([FromRoute] int id)
        {
            try
            {
                var usuario = context.Usuarios.Include(user => user.IdEmpleadoRolNavigation.Rols).Where(user => user.Id == id).FirstOrDefault();

                if (usuario != null)
                {
                    var usuarioView = new UsuarioView()
                    {
                        Id = usuario.Id,
                        IdEmpleadoRol = usuario.IdEmpleadoRol,
                        RolEmpleado = usuario.IdEmpleadoRolNavigation.Rols.Nombre,
                        Usuario1 = usuario.Usuario1,
                        Contraseña = usuario.Contraseña
                    };

                    return StatusCode((int)HttpStatusCode.OK, usuarioView);

                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "El usuario no fue encontrado" });

                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });

            }
        }

        [HttpPost]
        public IActionResult CreateUsuario([FromBody] UsuarioView usuarioView)
        {
            try
            {
                var usuario = new Usuario()
                {
                    Id = usuarioView.Id,
                    IdEmpleadoRol = usuarioView.IdEmpleadoRol,
                    Usuario1 = usuarioView.Usuario1,
                    Contraseña = usuarioView.Contraseña
                };

                context.Usuarios.Add(usuario);
                context.SaveChanges();

                usuarioView.Id = usuario.Id;

                return StatusCode((int)HttpStatusCode.Created, usuarioView);

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });

            }
        }


        [HttpPut]
        public IActionResult ModifyUsuario([FromBody] UsuarioView usuarioView)
        {
            try
            {
                var usuario = context.Usuarios.Find(usuarioView.Id);

                if (usuario != null)
                {
                    usuario.IdEmpleadoRol = usuarioView.IdEmpleadoRol;
                    usuario.Usuario1 = usuarioView.Usuario1;
                    usuario.Contraseña = usuarioView.Contraseña;


                    context.Usuarios.Update(usuario);
                    context.SaveChanges();

                    return StatusCode((int)HttpStatusCode.OK, new { mensaje = "La modificación fue realizada con éxito" });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "El usuario no fue encontrado" });

                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });

            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario([FromRoute] int id)
        {
            try
            {
                var usuario = context.Usuarios.Find(id);


                if (usuario != null)
                {
                    context.Usuarios.Remove(usuario);
                    context.SaveChanges();

                    return StatusCode((int)HttpStatusCode.OK, new { mensaje = "La transaccion fue realizada con exito" });

                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "El usuario no fue encontrado" });

                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }


        [HttpPost]
        [Route("login")]

        public IActionResult LoginUsuario([FromBody] LoginView loginView)
        {
            try
            {
                var login = new LoginView()
                {

                    Usuario = loginView.Usuario,
                    Contraseña = loginView.Contraseña,

                };


                var usuarioLogin = context.Usuarios.Where(user => user.Usuario1 == login.Usuario && user.Contraseña == login.Contraseña).FirstOrDefault();

                if (usuarioLogin != null)
                {
                    var usuarioView = new UsuarioView()
                    {
                        Id = usuarioLogin.Id,
                        IdEmpleadoRol = usuarioLogin.IdEmpleadoRol,
                        Usuario1 = usuarioLogin.Usuario1,
                        Contraseña = usuarioLogin.Contraseña
                    };

                    return StatusCode((int)HttpStatusCode.OK, usuarioView);

                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "usuario no registrado" });

                }



            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new { mensaje = ex.Message });

            }
        }


    }
}
