using Ecommerce_proyect.Models;
using Ecommerce_proyect.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Ecommerce_proyect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private DbContexEcommerce context;

        public EmailController(DbContexEcommerce context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult EnviarEmail([FromBody] EmailViews emailView)
        {
            try
            {
                string emailOrigen = "compushopumg@gmail.com";
                string password    = "unversidad2020";

                double montoTotal = 0;

                string body = "<!DOCTYPE html>" +
                    "<html lang=\"en\">" +
                    "<head>" +
                    "<meta charset=\"UTF - 8\">" +
                    "<meta http-equiv=\"X - UA - Compatible\" content=\"IE = edge\">" +
                    "<meta name=\"viewport\" content=\"width = device - width, initial - scale = 1.0\">" +
                    "<title>Document</title>" +
                    "</head>" +
                    "<body>" +
                    "<div style=\"max-width: 900px; margin: 0 auto; background-color: #F2F4F4; height: 800px; \">" +
                    "<br/>" +
                    "<h3 style=\"margin: 0 auto; width: 30px;\">CompuShop</h3>" +
                    "<br>" +
                    "<Label> <strong>Empresa: </strong></Label> <Label>Cmpushop S.A</Label>" +
                    "<br>" +
                    "<Label><strong>Nit: </strong></Label> <Label>41622630-5</Label>" +
                    "<br>" +
                    "<br/>" +
                    "<div style=\"display: flex; max-width: 900px; \">" +
                    "<div style=\"padding: 10px; border: black solid 1px; width: 450px; \">" +
                   $"<Label> <strong>Nombre: </strong></Label> <Label>{emailView.Nombre}</Label> " +
                    "<br/>" +
                   $"<Label> <strong>Nit: </strong></Label> <Label>{emailView.Nit}</Label>" +
                    "<br/>" +
                   $"<Label> <strong>Direccion: </strong></Label>{emailView.Direccion}<Label></Label> " +
                    "</div>" +
                    "<div style=\"padding: 10px; border: black solid 1px; width: 450px; \">" +
                   $"<Label> <strong>Fecha: </strong></Label> <Label>${emailView.Fecha}</Label> " +
                    "<br/>" +
                   $"<Label> <strong>Serie: </strong></Label>{emailView.Serie}<Label></Label> " +
                    "<br/>" +
                   $"<Label> <strong>No. Factura: </strong></Label> <Label>{emailView.Numero}</Label>" +
                    "</div>" +
                    "</div>" +
                    "<br/>" +
                    "<table border=\"1\" style=\"width: 750px; margin: 0 auto; \">" +
                    "<tr>" +
                    "<th>ID</th>" +
                    "<th>Nombre</th>" +
                    "<th>Precio</th>" +
                    "<th>Cantidad</th>" +
                    "<th>Subtotal</th>" +
                    "</tr>";

                emailView.Productos.ForEach(producto => 
                {
                    body +=  "<tr>" +
                            $"<td>{producto.IdProducto}</td>" +
                            $"<td>{producto.NombreProducto}</td>" +
                            $"<td>{producto.Precio}</td>" +
                            $"<td>{producto.Cantidad}</td>" +
                            $"<td>{producto.Precio * producto.Cantidad}</td>" +
                             "</tr>";

                    montoTotal += producto.Precio * producto.Cantidad;
                });



                body += "</table>" +
                        "<br/>"    +
                       $"<Label style=\"margin-left: 730px; \"><strong>Total: Q</strong></Label> <label>{montoTotal}</label>" +
                        "</div>"  +
                        "</body>" +
                        "</html>";
                

                MailMessage mailMessage = new MailMessage(emailOrigen, emailView.Correo, "Factura de Compra", body);
                mailMessage.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.EnableSsl             = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Host                  = "smtp.gmail.com";
                smtpClient.Port                  = 587;
                smtpClient.Credentials           = new NetworkCredential(emailOrigen, password);

                smtpClient.Send(mailMessage);

                smtpClient.Dispose();

                return StatusCode((int)HttpStatusCode.Created, emailView);
        
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });
            }
        }
    }
}
