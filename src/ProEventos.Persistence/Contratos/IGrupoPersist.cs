using System.Collections.Generic;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface IGrupoPersist
    {
          Task <IEnumerable<Grupos>> GetAllGruposAsync();
         Task<Grupos> GetGrupoByIdAsync(int id);
         Task<Grupos> GetGrupoByNomeAsync(string nome);
    }
}