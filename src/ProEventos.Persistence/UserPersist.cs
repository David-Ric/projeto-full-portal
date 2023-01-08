using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Identity;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;
using ProEventos.Persistence.Models;

namespace ProEventos.Persistence
{
    public class UserPersist : GeralPersist, IUserPersist
    {
        private readonly ProEventosContext _context;

        public UserPersist(ProEventosContext context) : base(context)
        {
            _context = context;
        }
         public async Task<PageList<User>> GetAllUsersAsync(PageParams pageParams )
        {
          
            IQueryable<User> query = _context.Users;
                


            query = query.AsNoTracking()
                         .Where(e => (e.PrimeiroNome.ToLower().Contains(pageParams.Term.ToLower()) ||
                                      e.Ativo.ToLower().Contains(pageParams.Term.ToLower())) || e.UltimoNome.ToLower().Contains(pageParams.Term.ToLower())) 
                         .OrderBy(e => e.Id);

            return await PageList<User>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
           
            return await _context.Users.ToListAsync();
        }


        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users
                                 .SingleOrDefaultAsync(user => user.UserName == userName.ToLower());
        }

        public async Task<User> GetUseByPrimironomeAsync(string primeiroNome)
        {
           return await _context.Users
                                 .SingleOrDefaultAsync(user => user.PrimeiroNome == primeiroNome.ToLower());
        }

       
    }
}