using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgropecuaria.Infraestructure.Servicios
{
    public class EnviarCorreos
    {
        private readonly IConfiguration configuration;

        public EnviarCorreos(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task EnviarCorreo(string emailDestino, string asunto, string cuerpo)
        {
            var nuestroEmail = configuration["Config_Email:Email"];
            var password = configuration["Config_Email:Password"];
            var host = configuration["Config_Email:Host"];
            var puerto = configuration["Config_Email:Puerto"];

            var smtpClient = new SmtpClient(host, int.Parse(puerto!));
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(nuestroEmail, password);

            var mensaje = new MailMessage
            {
                From = new MailAddress(nuestroEmail!),
                Subject = asunto,
                Body = cuerpo,
                IsBodyHtml = true 
            };
            mensaje.To.Add(emailDestino);

            await smtpClient.SendMailAsync(mensaje);

        }
    }
}
