using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class PermissoesPersist : GeralPersist, IPermissoesPersist
    {
         private readonly ProEventosContext _context;
        public PermissoesPersist(ProEventosContext context) : base(context)
        {
             _context = context;
             _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<IEnumerable<Permissoes>> GetAllPertmissoesAssync()
        {
            return await _context.Permissoes.ToListAsync();
        }

        public async Task<Permissoes> GetPermissoesById(int id)
        {
            return await _context.Permissoes.FindAsync(id);
        }
    }
}