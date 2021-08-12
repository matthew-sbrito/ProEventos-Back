using System;
using ProEventos.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProEventos.Application.Interfaces;
using ProEventos.Persistence.Interfaces;
using ProEventos.Application.Dto;
using AutoMapper;
using System.Linq;

namespace ProEventos.Application
{
  public class LoteService : ILoteService
  {
    private readonly IGeralPersist _geralPersist;
    private readonly ILotePersist _lotePersist;
    private readonly IMapper _mapper;

    public LoteService(
      IGeralPersist geralPersist,
      ILotePersist lotePersist,
      IMapper mapper)
    {
      _geralPersist = geralPersist;
      _lotePersist = lotePersist;
      _mapper = mapper;
    }

    public async Task AddLote(int eventoId, LoteDto model)
    {
      try
      {
        var lote = _mapper.Map<Lote>(model);
        lote.EventoId = eventoId;

        _geralPersist.Add<Lote>(lote);

        await _geralPersist.SaveChangesAsync();
      }
      catch (Exception ex)
      {

        throw new Exception(ex.Message);
      }
    }
    public async Task<LoteDto[]> SaveLote(int eventoId, LoteDto[] models)
    {
      try
      {
        var lotes = await _lotePersist.GetLotesByEventoAsync(eventoId);
        if (lotes == null) return null;

        foreach (var model in models)
        {
          if (model.Id == 0)
          {
            await AddLote(eventoId, model);
          }
          else
          {
            var lote = lotes.FirstOrDefault(lote => lote.Id == model.Id);
            model.EventoId = eventoId;

            _mapper.Map(model, lote);

            _geralPersist.Update<Lote>(lote);

            await _geralPersist.SaveChangesAsync();
          }

        }

        var lotesReturn = await _lotePersist.GetLotesByEventoAsync(eventoId);
        return _mapper.Map<LoteDto[]>(lotesReturn);

      }
      catch (Exception ex)
      {

        throw new Exception(ex.Message);
      }
    }
    public async Task<bool> DeleteLote(int eventoId, int loteId)
    {
      try
      {
        var lote = await _lotePersist.GetLoteByIdAsync(eventoId, loteId);
        if (lote == null) throw new Exception("Lote não encontrado!");

        _geralPersist.Delete<Lote>(lote);

        return await _geralPersist.SaveChangesAsync();

      }
      catch (Exception ex)
      {

        throw new Exception(ex.Message);
      }
    }

    public async Task<LoteDto[]> GetLotesByEventoAsync(int eventoId)
    {
      try
      {
        var lotes = await _lotePersist.GetLotesByEventoAsync(eventoId);
        if (lotes == null) return null;

        var resultado = _mapper.Map<LoteDto[]>(lotes);

        return resultado;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    public async Task<LoteDto> GetLotesByIdAsync(int eventoId, int loteId)
    {
      try
      {
        var lote = await _lotePersist.GetLoteByIdAsync(eventoId, loteId);
        if (lote == null) return null;

        var resultado = _mapper.Map<LoteDto>(lote);

        return resultado;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

  }
}