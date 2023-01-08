using System.Collections.Generic;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface IPermissoesPersist
    {
         Task<IEnumerable<Permissoes>> GetAllPertmissoesAssync();
         Task<Permissoes> GetPermissoesById(int id);
         
    }
}