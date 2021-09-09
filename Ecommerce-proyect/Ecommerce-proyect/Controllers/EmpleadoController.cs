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
    public class EmpleadoController : ControllerBase
    {
        private DbContexEcommerce context;

        public EmpleadoController(DbContexEcommerce context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetEmpleado()
        {
            try
            {
                var empleados = context.Empleados.Include(empleado => empleado.EmpleadoRols).ThenInclude(empleadoRol => empleadoRol.Rols).ToList();

                List<EmpleadoView> empleadoViews = new List<EmpleadoView>();
                
                empleados.ForEach(empleado => 
                {
                    empleado.EmpleadoRols.ToList().ForEach(empleadoRol => 
                    {
                
                        empleadoViews.Add(new EmpleadoView()
                        {
                            Id        = empleado.Id,
                            Nombre    = empleado.Nombre,
                            Apellido  = empleado.Apellido,
                            Telefono  = empleado.Telefono,
                            Correo    = empleado.Correo,
                            Direccion = empleado.Direccion,
                            Nit       = empleado.Nit,
                            IdRol     = empleadoRol.IdRol,
                            Rol       = empleadoRol.Rols.Nombre
                        });
                    });
                });


                return StatusCode((int)HttpStatusCode.OK, empleadoViews);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdEmpleado([FromRoute] int id)
        {
            try
            {
                var empleado = context.Empleados.Include(empleado => empleado.EmpleadoRols).ThenInclude(empleadoRol => empleadoRol.Rols).Where(empleado => empleado.Id == id).FirstOrDefault();

                if (empleado != null)
                {
                    EmpleadoView empleadoView = new EmpleadoView();
                    empleado.EmpleadoRols.ToList().ForEach(empleadoRol =>
                    {
                        empleadoView.Id        = empleado.Id;
                        empleadoView.Nombre    = empleado.Nombre;
                        empleadoView.Apellido  = empleado.Apellido;
                        empleadoView.Telefono  = empleado.Telefono;
                        empleadoView.Correo    = empleado.Correo;
                        empleadoView.Direccion = empleado.Direccion;
                        empleadoView.Nit       = empleado.Nit;
                        empleadoView.IdRol     = empleadoRol.IdRol;
                        empleadoView.Rol       = empleadoRol.Rols.Nombre;
                    });

                    return StatusCode((int)HttpStatusCode.OK, empleadoView);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "El empleado no fue encontrado" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CreateEmpleado([FromBody] EmpleadoView empleadoView)
        {
            try
            {
                var empleado = new Empleado()
                {
                    Nombre    = empleadoView.Nombre,
                    Apellido  = empleadoView.Apellido,
                    Telefono  = empleadoView.Telefono,
                    Correo    = empleadoView.Correo,
                    Direccion = empleadoView.Direccion,
                    Nit       = empleadoView.Nit,
                    Estado    = 1,
                };

                context.Empleados.Add(empleado);
                context.SaveChanges();


                var rol = context.Rols.Find(empleadoView.IdRol);

                var empleadoRol = new EmpleadoRol() 
                { 
                   IdEmpleado = empleado.Id,
                   IdRol      = rol.Id
                };

                context.EmpleadoRols.Add(empleadoRol);
                context.SaveChanges();

                empleadoView.Id = empleado.Id;
  

                return StatusCode((int)HttpStatusCode.Created, empleadoView);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        public IActionResult ModifyEmpleado([FromBody] EmpleadoView empleadoView)
        {
            try
            {
                var empleado = context.Empleados.Find(empleadoView.Id);

                if (empleado != null)
                {
                    empleado.Nombre    = empleadoView.Nombre;
                    empleado.Apellido  = empleadoView.Apellido;
                    empleado.Telefono  = empleadoView.Telefono;
                    empleado.Correo    = empleadoView.Correo;
                    empleado.Direccion = empleadoView.Direccion;
                    empleado.Nit       = empleadoView.Nit;

                    context.Empleados.Update(empleado);
                    context.SaveChanges();

                    var empleadoRol = context.EmpleadoRols.Find(empleadoView.IdRol);

                    empleadoRol.IdEmpleado = empleado.Id;
                    empleadoRol.IdRol      = empleadoView.IdRol;


                    context.EmpleadoRols.Update(empleadoRol);
                    context.SaveChanges();


                    return StatusCode((int)HttpStatusCode.OK, new { mensaje = "La transaccion fue realizada con exito" });

                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "El empleado no fue encontrado" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProveedor([FromRoute] int id)
        {
            try
            {
                var empleado = context.Empleados.Find(id);

                if (empleado != null)
                {
                    empleado.Estado = 0;
                    context.Empleados.Update(empleado);
                    context.SaveChanges();

                    return StatusCode((int)HttpStatusCode.OK, new { mensaje = "La transaccion fue realizada con exito" });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mensaje = "El empleado no fue encontrado" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }
    }
}
