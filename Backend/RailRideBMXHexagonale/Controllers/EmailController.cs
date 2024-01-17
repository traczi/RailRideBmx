using Application;
using Application.IServices;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using RailRideBMXHexagonale.Helpers;

namespace RailRideBMXHexagonale.Controllers;

public class EmailController : ApiController
{
    private readonly IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }
    
    [HttpPost]
    [Route("SendMail")]
    public async Task<IActionResult> SendEmail(EmailRequest emailRequest)
    {
        await _emailService.SendEmailAsync(emailRequest.To, emailRequest.Subject, emailRequest.Body);
            return SuccessResponseHelper.SuccessResponse("E-mail envoyé avec succès !");
    }
}

public class EmailRequest
{
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}