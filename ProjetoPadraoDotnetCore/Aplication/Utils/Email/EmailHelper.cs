using System.Net.Mail;

namespace Aplication.Utils.Email;

public class EmailHelper : IEmailHelper
{
    private readonly IConfiguration _configuration;
    public EmailHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public bool EnviarEmail(List<string> email,string titulo,string corpo)
    {
        try
        {
            var mail = new MailMessage();
            var smtpServer = new SmtpClient(_configuration.GetSection("EmailBase:Host").Value,587);

            mail.From = new MailAddress(_configuration.GetSection("EmailBase:EmailPadraoFrom").Value);

            foreach (var item in email)
            {
                mail.To.Add(item);
            }

            //Configurações
            mail.Subject = titulo;
            mail.IsBodyHtml = true;
            mail.Body = corpo;
            
            
            smtpServer.Credentials = new System.Net.NetworkCredential(
                _configuration.GetSection("EmailBase:EmailPadraoFrom").Value, 
                _configuration.GetSection("EmailBase:Password").Value);
            smtpServer.EnableSsl = true;

            smtpServer.Send(mail);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}