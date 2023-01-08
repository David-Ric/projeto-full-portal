using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class PaginasPersist : GeralPersist, IPaginaPersist
    {
        private readonly ProEventosContext _context;
        public PaginasPersist(ProEventosContext context) : base(context)
        {
            _context = context;
             _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<IEnumerable<Paginas>> GetAllPaginasAssync()
        {
            return await _context.Paginas.ToListAsync();
        }

        public async Task<Paginas> GetPaginaById(int id)
        {
            return await _context.Paginas.FindAsync(id);
        }
    }
}