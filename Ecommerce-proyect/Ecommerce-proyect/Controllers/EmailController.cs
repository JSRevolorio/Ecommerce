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


                string body = "" +
                    "" +
                    "" +
                    "" +
                    "   v";
                

                MailMessage mailMessage = new MailMessage(emailOrigen, emailView.Correo, "Factura de Compra", "<h1> Factuta Prueba <h1>");
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
