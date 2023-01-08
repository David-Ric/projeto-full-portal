using System.Collections.Generic;
using System.Threading.Tasks;
using ProEventos.Application.Dtos;

namespace ProEventos.Application.Contratos
{
    public interface IGrupoService
    {
         Task <IEnumerable<GrupoDto>> GetAllGruposAsync();
         Task<GrupoDto> GetGrupoByIdAsync(int id);
         Task<GrupoDto> GetGrupoByNomeAsync(string nome);
         Task<GrupoDto> AddGrupos(GrupoDto model);
         Task<GrupoDto> UpdateGrupo(int id, GrupoDto model);
         Task<bool> DeleteGrupo(int id);
    }
}