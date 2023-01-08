using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class GrupoPersist : GeralPersist, IGrupoPersist
    {
        private readonly ProEventosContext _context;

        public GrupoPersist(ProEventosContext context):base(context)
        {
            _context = context;
             _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<IEnumerable<Grupos>> GetAllGruposAsync()
        {
            return await _context.Grupos.ToListAsync();
        }

        public async Task<Grupos> GetGrupoByIdAsync(int id)
        {
            return await _context.Grupos.FindAsync(id);
        }

        public async Task<Grupos> GetGrupoByNomeAsync(string nome)
        {
            return await _context.Grupos.SingleOrDefaultAsync(grupo => grupo.Nome == nome.ToLower());
        }
    }
}