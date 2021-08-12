using System;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Controllers.Input;

namespace ProEventos.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class EmailController : ControllerBase
  {
    [HttpPost("/api/email/send/{eventoId}")]
    public ActionResult SendEmail(int eventoId, SendEmail model)
    { 
      var content  = contentEmail(model.Email, eventoId);
      var response = Execute(model.Email, content);
      return Ok(response);
    }
    public bool Execute(string email, string content)
    {
      try
      {
        MailMessage _mailMessage = new MailMessage();

        _mailMessage.From = new MailAddress("matheusbr032@gmail.com");

        _mailMessage.CC.Add(email);
        _mailMessage.Subject = "Teste";
        _mailMessage.IsBodyHtml = true;
        _mailMessage.Body = content;

        SmtpClient _smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32("587"));

        _smtpClient.UseDefaultCredentials = false;

        _smtpClient.Credentials = new NetworkCredential("matheusbr032@gmail.com", "mt0072610");

        _smtpClient.EnableSsl = true;

        _smtpClient.Send(_mailMessage);

        return true;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

    }
    public string contentEmail(string email, int eventoId){
      return $"<b>Olá confirme seu email!{email} </b><br><button>Aceitar</button><span>Evento de Id: {eventoId}</span>";
    }
  }
}
