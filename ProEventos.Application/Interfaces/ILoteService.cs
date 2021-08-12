using System.Threading.Tasks;
using ProEventos.Application.Dto;

namespace ProEventos.Application.Interfaces
{
  public interface ILoteService
  {
    Task<LoteDto[]> SaveLote(int eventoId, LoteDto[] models);
    Task<bool> DeleteLote(int eventoId, int loteId);
    Task<LoteDto[]> GetLotesByEventoAsync(int eventoId);
    Task<LoteDto> GetLotesByIdAsync(int eventoId, int loteId);
    
  }
}