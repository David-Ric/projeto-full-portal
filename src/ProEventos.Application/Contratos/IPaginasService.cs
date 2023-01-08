using System.Collections.Generic;
using System.Threading.Tasks;
using ProEventos.Application.Dtos;

namespace ProEventos.Application.Contratos
{
    public interface IPaginasService
    {
          Task <IEnumerable<PaginaDto>> GetAlPaginasAsync();
         Task<PaginaDto> GetPaginaByIdAsync(int id);
         Task<PaginaDto> AddPaginas(PaginaDto model);
         Task<PaginaDto> UpdatePagina(int id, PaginaDto model);
         Task<bool> DeletePagina(int id); 
    }
}