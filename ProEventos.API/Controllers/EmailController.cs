using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Controllers.Input;
using ProEventos.Application.Interfaces;

namespace ProEventos.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class EmailController : ControllerBase
  {
    public IEventoService _eventoService;
    public EmailController(IEventoService eventoService)
    {
      _eventoService = eventoService;   
    }
    [HttpPost("/api/email/send/{eventoId}")]
    public async Task<ActionResult> SendEmail(int eventoId, SendEmail model)
    {
      var content = await contentEmail(model.Email, eventoId);
      var response = Execute(model.Email, content);
      return Ok(response);
    }
    public bool Execute(string email, string content)
    {
      try
      {
        MailMessage _mailMessage = new MailMessage();

        _mailMessage.From = new MailAddress("email");

        _mailMessage.CC.Add(email);
        _mailMessage.Subject = "Pro Eventos";
        _mailMessage.IsBodyHtml = true;
        _mailMessage.Body = content;

        SmtpClient _smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32("587"));

        _smtpClient.UseDefaultCredentials = false;

        _smtpClient.Credentials = new NetworkCredential("email", "senha");

        _smtpClient.EnableSsl = true;

        _smtpClient.Send(_mailMessage);

        return true;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

    }
    public async Task<string> contentEmail(string email, int eventoId)
    {

      var evento = await _eventoService.GetEventoByIdAsync(eventoId);
      
      WebClient wc = new WebClient();
      wc.Encoding = System.Text.Encoding.UTF8;

      string sTemplate = wc.DownloadString(
          "./Controllers/Input/email.html");

      string id = $"{eventoId}";
      string tema = evento.Tema;
      string data = $"{evento.DataEvento}";

      sTemplate = sTemplate.Replace("##eventoId##", id);
      sTemplate = sTemplate.Replace("##Tema##", evento.Tema);
      sTemplate = sTemplate.Replace("##Local##", evento.Local);
      sTemplate = sTemplate.Replace("##Data##", data);
      sTemplate = sTemplate.Replace("##link##", $"https://localhost:4200/evento/{id}");

      return sTemplate;
    }
  }
}
