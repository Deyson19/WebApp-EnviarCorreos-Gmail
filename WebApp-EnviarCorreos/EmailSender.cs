using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace WebApp_EnviarCorreos
{
    public interface IEmailSender
    {
        void SendEmail(EmailDto email);
    }
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendEmail(EmailDto emailDto)
        {
            var email = new MimeMessage();
            string usuario = _configuration.GetSection("Email:UserName").Value;
            string puerto = _configuration.GetSection("Email:Port").Value;
            string passsword = _configuration.GetSection("Email:PassWord").Value;
            string host = _configuration.GetSection("Email:Host").Value;

            //*Enviar el correo
            email.From.Add(MailboxAddress.Parse(usuario));
            email.To.Add(MailboxAddress.Parse(emailDto.Para));
            email.To.Add(MailboxAddress.Parse("deysondev@yopmail.com"));
            email.Subject = emailDto.Asunto.ToString();
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = emailDto.Contenido.ToString()
            };

            using var smtpGmail = new SmtpClient();
            smtpGmail.Connect(host,Convert.ToInt32(puerto),SecureSocketOptions.StartTls);
            smtpGmail.Authenticate(usuario,passsword);
            smtpGmail.Send(email);
            smtpGmail.Disconnect(true);
        }
    }
}
