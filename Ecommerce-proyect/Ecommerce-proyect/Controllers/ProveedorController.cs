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
    public class ProveedorController : ControllerBase
    {
        private DbContexEcommerce context;

        public ProveedorController(DbContexEcommerce context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetProveedores()
        {
            try
            {
                var proveedores = context.Proveedors.Where(proveedor => proveedor.Estado == 1).ToList();


                List<ProveedorView> proveedorViews = new List<ProveedorView>();
                proveedores.ForEach(proveedor =>
                {
                    proveedorViews.Add(new ProveedorView()
                    {
                        Id = proveedor.Id,
                        Nombre = proveedor.Nombre,
                        Telefono = proveedor.Telefono,
                        Direccion = proveedor.Direccion,
                        Correo = proveedor.Correo
                    });
                });

                return StatusCode((int)HttpStatusCode.OK, proveedorViews);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdProveedor([FromRoute] int id)
        {
            try
            {
                var proveedor = context.Proveedors.Find(id);

                if (proveedor != null)
                {
                    var proveedorView = new ProveedorView()
                    {
                        Id = proveedor.Id,
                        Nombre = proveedor.Nombre,
                        Telefono = proveedor.Telefono,
                        Direccion = proveedor.Direccion,
                        Correo = proveedor.Correo
                    };

                    return StatusCode((int)HttpStatusCode.OK, proveedorView);
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

        [HttpPost]
        public IActionResult CreateProveedor([FromBody] ProveedorView proveedorView)
        {
            try
            {
                var proveedor = new Proveedor()
                {
                    Nombre = proveedorView.Nombre,
                    Telefono = proveedorView.Telefono,
                    Direccion = proveedorView.Direccion,
                    Correo = proveedorView.Correo,
                    Estado = 1
                };

                context.Proveedors.Add(proveedor);
                context.SaveChanges();


                proveedorView.Id = proveedor.Id;


                return StatusCode((int)HttpStatusCode.Created, proveedorView);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        public IActionResult ModifyProveedor([FromBody] ProveedorView proveedorView) 
        {
            try
            {
                var proveedor = context.Proveedors.Find(proveedorView.Id);

                if (proveedor != null)
                {
                    proveedor.Nombre    = proveedorView.Nombre;
                    proveedor.Telefono  = proveedorView.Telefono;
                    proveedor.Direccion = proveedorView.Direccion;
                    proveedor.Correo    = proveedorView.Correo;

                    context.Proveedors.Update(proveedor);
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

        [HttpDelete("{id}")]
        public IActionResult DeleteProveedor([FromRoute] int id) 
        {
            try
            {
                var proveedor = context.Proveedors.Find(id);

                if (proveedor != null)
                {
                    proveedor.Estado = 0;
                    context.Proveedors.Update(proveedor);
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
