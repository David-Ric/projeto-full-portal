using System.Threading.Tasks;
using ProEventos.Application.Dtos;

namespace ProEventos.Application.Contratos
{
    public interface IUserService
    {
        Task<UserUpdateDto> GetUserByIdAsync(int id);
        Task<UserUpdateDto> UpdateUser(int id, UserUpdateDto userUpdateDto);
    }
}