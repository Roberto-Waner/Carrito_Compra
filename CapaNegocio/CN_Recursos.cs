using MailKit.Security;
//using MailKit.Net.Smtp;
using MimeKit;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class CN_Recursos
    {
        /// <summary>
        /// Genera una clave corta de 6 caracteres basada en un GUID.
        /// </summary>
        /// <returns>Cadena de 6 caracteres alfanuméricos (hexadecimales) que representa la clave generada.</returns>
        public static string GenerarClave()
        {
            // 1) Genera un GUID nuevo. Ejemplo: "d3c4a1f05e6b4a7b9c0d1e2f3a4b5c6d"
            //    Usamos ToString("N") para obtener la representación sin guiones (32 caracteres hex).

            // 2) Tomamos los primeros 6 caracteres del GUID para formar la clave.
            //    Substring(0, 6) devuelve los caracteres desde índice 0 hasta 5 (6 caracteres en total).
            string clave = Guid.NewGuid().ToString("N").Substring(0, 6);

            // 3) Devolvemos la clave generada.
            return clave;
        }

        //Encriptacion de texto en SHA256
        public static string ConvertirSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));
                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        public static bool EnviarCorreo(string correo, string asunto, string mensaje)
        {
            bool resultado = false;

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From = new MailAddress("no-reply@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential("rwaner977@gmail.com", "aomh hnyc jiei iwmv"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                };

                smtp.Send(mail);
                resultado = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar correo: " + ex.Message);
                resultado = false;
                throw;
            }

            //string userEmail = "rwaner977@gmail.com";
            //string userPassword = "aomh hnyc jiei iwmv";
            ////string host = "smtp.gmail.com";
            ////int port = 587;

            //try
            //{
            //    var mail = new MimeMessage();
            //    mail.From.Add(new MailboxAddress("Soporte Técnico", userEmail));
            //    mail.To.Add(new MailboxAddress("User Clint", correo));
            //    mail.Subject = asunto;

            //    var bodyBuilders = new BodyBuilder
            //    {
            //        HtmlBody = mensaje
            //    };
            //    mail.Body = bodyBuilders.ToMessageBody();

            //    using var smtpClient = new SmtpClient();
            //    smtpClient.ConnectAsync(
            //        "smtp.gmail.com",
            //        587,
            //        SecureSocketOptions.StartTls
            //    );
            //    smtpClient.AuthenticateAsync(userEmail, userPassword);
            //    smtpClient.SendAsync(mail);
            //    smtpClient.DisconnectAsync(true);

            //    resultado = true;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error al enviar correo: " + ex.Message);
            //    resultado = false;
            //    throw;
            //}

            return resultado;
        }
    }
}
