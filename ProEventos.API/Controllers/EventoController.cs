using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Dto;
using ProEventos.Application.Interfaces;

namespace ProEventos.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class EventoController : ControllerBase
  {
    public readonly IEventoService _eventoService;
    public EventoController(IEventoService eventoService)
    {
      _eventoService = eventoService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {
        var eventos = await _eventoService.GetAllEventosAsync(true);
        if (eventos == null) return NoContent();
        //"Evento não encontrado!"
        return Ok(eventos);
      }
      catch (Exception ex)
      {
        return this.StatusCode(500, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
      }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      try
      {
        var evento = await _eventoService.GetEventoByIdAsync(id);
        if (evento == null) return NoContent();
          //"Evento por id não encontrado!"
        return Ok(evento);
      }
      catch (Exception ex)
      {
        return this.StatusCode(500, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
      }
    }
    [HttpGet("tema/{tema}")]
    public async Task<IActionResult> GetByTema(string tema)
    {
      try
      {
        var evento = await _eventoService.GetAllEventosByTemaAsync(tema);
        if (evento == null) return NoContent();
        //"Eventos por tema não encontrados!"
        return Ok(evento);
      }
      catch (Exception ex)
      {
        return this.StatusCode(500, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post(EventoDto model)
    {
      try
      {
        var evento = await _eventoService.AddEventos(model);
        if (evento == null) return BadRequest("Erro ao cadastrar evento!");

        return Ok(evento);
      }
      catch (Exception ex)
      {
        return this.StatusCode(500, $"Erro ao tentar cadastrar eventos. Erro: {ex.Message}");
      }
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, EventoDto model)
    {
      try
      {
        //   var model = await _eventoService.GetEventoByIdAsync(id);
        var evento = await _eventoService.UpdateEvento(id, model);
        if (evento == null) return BadRequest("Erro ao atualizar evento!");

        return Ok(evento);
      }
      catch (Exception ex)
      {
        return this.StatusCode(500, $"Erro ao tentar atualizar evento. Erro: {ex.Message}");
      }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        return await _eventoService.DeleteEvento(id) ?
              Ok("Deletado") :
              BadRequest("Evento não deletado");

      }
      catch (Exception ex)
      {
        return this.StatusCode(500, $"Erro ao tentar atualizar evento. Erro: {ex.Message}");
      }
    }
  }
}
