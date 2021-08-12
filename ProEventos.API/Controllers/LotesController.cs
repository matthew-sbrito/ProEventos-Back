using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Dto;
using ProEventos.Application.Interfaces;

namespace ProEventos.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class LotesController : ControllerBase
  {
    public readonly ILoteService _loteService;
    public LotesController(ILoteService LoteService)
    {
      _loteService = LoteService;
    }

    [HttpGet("{eventoId}")]
    public async Task<IActionResult> Get(int eventoId)
    {
      try
      {
        var lotes = await _loteService.GetLotesByEventoAsync(eventoId);
        if (lotes == null) return NoContent();
        
        return Ok(lotes);
      }
      catch (Exception ex)
      {
        return this.StatusCode(500, $"Erro ao tentar recuperar Lotes. Erro: {ex.Message}");
      }
    }
    
    [HttpPut("{eventoId}")]
    public async Task<IActionResult> Put(int eventoId, LoteDto[] models)
    {
      try
      {
        //   var model = await _loteService.GetEventoByIdAsync(id);
        var lotes = await _loteService.SaveLote(eventoId, models);
        if (lotes == null) return BadRequest("Erro ao atualizar Lote!");

        return Ok(lotes);
      }
      catch (Exception ex)
      {
        return this.StatusCode(500, $"Erro ao tentar salvar Lotes. Erro: {ex.Message}");
      }
    }
    [HttpDelete("{eventoId}/{loteId}")]
    public async Task<IActionResult> Delete(int eventoId, int loteId)
    {
      try
      {
        var lote = await _loteService.GetLotesByIdAsync(eventoId, loteId);
        if(lote == null) return NoContent();

        return await _loteService.DeleteLote(lote.EventoId, lote.Id) ?
              Ok(new { message = "Lote deletado"}) :
              BadRequest("Lote não deletado");

      }
      catch (Exception ex)
      {
        return this.StatusCode(500, $"Erro ao tentar atualizar Lote. Erro: {ex.Message}");
      }
    }
  }
}
