using System.Collections.Generic;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface IPaginaPersist
    {
          Task<IEnumerable<Paginas>> GetAllPaginasAssync();
         Task<Paginas> GetPaginaById(int id);
    }
}