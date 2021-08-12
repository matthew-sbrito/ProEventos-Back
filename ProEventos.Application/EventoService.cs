using System;
using ProEventos.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProEventos.Application.Interfaces;
using ProEventos.Persistence.Interfaces;
using ProEventos.Application.Dto;
using AutoMapper;

namespace ProEventos.Application
{
  public class EventoService : IEventoService
  {
    private readonly IGeralPersist _geralPersist;
    private readonly IEventoPersist _eventoPersist;
    private readonly IMapper _mapper;

    public EventoService(
      IGeralPersist geralPersist,
      IEventoPersist eventoPersist,
      IMapper mapper)
    {
      _geralPersist = geralPersist;
      _eventoPersist = eventoPersist;
      _mapper = mapper;
    }
    public async Task<EventoDto> AddEventos(EventoDto model)
    {
      try
      {
        var evento = _mapper.Map<Evento>(model);
        _geralPersist.Add<Evento>(evento);
        if (await _geralPersist.SaveChangesAsync())
        {
          var eventReturn = await _eventoPersist.GetEventoByIdAsync(evento.Id, false);
          return _mapper.Map<EventoDto>(eventReturn);
        };
        return null;
      }
      catch (Exception ex)
      {

        throw new Exception(ex.Message);
      }
    }

    public async Task<EventoDto> UpdateEvento(int eventoId, EventoDto model)
    {
      try
      {
        var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);
        if (evento == null) return null;
        
        model.Id = evento.Id;
       _mapper.Map(model, evento);

        _geralPersist.Update<Evento>(evento);
        if (await _geralPersist.SaveChangesAsync())
        {
          var eventReturn = await _eventoPersist.GetEventoByIdAsync(evento.Id, false);
          return _mapper.Map<EventoDto>(eventReturn);
        }
        return null;
      }
      catch (Exception ex)
      {

        throw new Exception(ex.Message);
      }
    }

    public async Task<bool> DeleteEvento(int eventoId)
    {
      try
      {
        var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);
        if (evento == null) throw new Exception("Evento para delete não encontrado!");

        _geralPersist.Delete<Evento>(evento);

        return await _geralPersist.SaveChangesAsync();

      }
      catch (Exception ex)
      {

        throw new Exception(ex.Message);
      }
    }

    public async Task<EventoDto[]> GetAllEventosAsync(bool includesPalestrantes = false)
    {
      try
      {
        var eventos = await _eventoPersist.GetAllEventosAsync(includesPalestrantes);
        if (eventos == null) return null;

        var resultado = _mapper.Map<EventoDto[]>(eventos);

        return resultado;
      }
      catch (Exception ex)
      {

        throw new Exception(ex.Message);
      }
    }

    public async Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includesPalestrantes = false)
    {
      try
      {
        var eventos = await _eventoPersist.GetAllEventosByTemaAsync(tema, includesPalestrantes);
        if (eventos == null) return null;
        
        var resultado = _mapper.Map<EventoDto[]>(eventos);

        return resultado;
      }
      catch (Exception ex)
      {

        throw new Exception(ex.Message);
      }
    }

    public async Task<EventoDto> GetEventoByIdAsync(int eventoId, bool includesPalestrantes = false)
    {
      try
      {
        var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, includesPalestrantes);
        if (evento == null) return null;

        var resultado = _mapper.Map<EventoDto>(evento);

        return resultado;
      }
      catch (Exception ex)
      {

        throw new Exception(ex.Message);
      }
    }
  }
}