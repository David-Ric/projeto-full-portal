using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain.Identity;
using ProEventos.Persistence.Models;

namespace ProEventos.Persistence.Contratos
{
    public interface IUserPersist : IGeralPersist
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<PageList<User>> GetAllUsersAsync(PageParams pageParams);
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUserNameAsync(string userName);
         Task<User> GetUseByPrimironomeAsync(string PrimeiroNome);
    }
}