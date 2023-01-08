using System.Collections.Generic;
using System.Threading.Tasks;
using ProEventos.Application.Dtos;

namespace ProEventos.Application.Contratos
{
    public interface IPermissoesService
    {
         Task <IEnumerable<PermissoesDto>> GetAllPermissoesAsync();
         Task<PermissoesDto> GetPermissaoByIdAsync(int id);
         Task<PermissoesDto> AddPermissoes(PermissoesDto model);
         Task<PermissoesDto> UpdatePermissao(int id, PermissoesDto model);
         Task<bool> DeletePermissao(int id); 
    }
}