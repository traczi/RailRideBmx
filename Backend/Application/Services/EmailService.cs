using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;

namespace Application.Services;

public class EmailService : IEmailService
{
    private readonly string _from;
    private readonly string _smtpServer;
    private readonly int _port;
    private readonly string _userName;
    private readonly string _password;

    public EmailService(IConfiguration configuration)
    {
        _from = configuration["EmailConfiguration:From"];
    _smtpServer = configuration["EmailConfiguration:SmtpServer"];
    _port = Convert.ToInt32(configuration["EmailConfiguration:Port"]);
    _userName = configuration["EmailConfiguration:UserName"];
    _password = configuration["EmailConfiguration:Password"];
    }
    
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        using (var message = new MailMessage())
        {
            message.To.Add(new MailAddress(to));
            message.From = new MailAddress(_from);
            Console.WriteLine(_from);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient(_smtpServer,_port))
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(_userName, _password);
                smtp.EnableSsl = true;

                try
                {
                    await smtp.SendMailAsync(message);
                }
                catch (Exception e)
                {
                    throw new ApplicationException($"Erreur lors de l'envoi de l'e-mail : {e.Message}");
                }
            }
        }
    }
}