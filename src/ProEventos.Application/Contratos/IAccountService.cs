using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProEventos.Application.Dtos;
using ProEventos.Persistence.Models;

namespace ProEventos.Application.Contratos
{
    public interface IAccountService
    {
        Task<bool> UserExists(string username);
         Task<UserUpdateDto> GetUserByIdAsync(int id);
        Task<UserUpdateDto> GetUserByUserNameAsync(string username);
        Task<UserUpdateDto> GetUserByNameAsync(string primeiroNome);
        Task<PageList<UserUpdateDto>> GetAllUserUserAsync(PageParams pageParams);
        Task<IEnumerable<UserUpdateDto>> GetAllUserAsync();
        Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password);
        Task<UserUpdateDto> CreateAccountAsync(UserDto userDto);
        Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto);
        Task<UserUpdateDto> UpdateUser(int id, UserUpdateDto userUpdateDto);
    }
}