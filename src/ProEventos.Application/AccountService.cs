using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Domain.Identity;
using ProEventos.Persistence.Contratos;
using ProEventos.Persistence.Models;

namespace ProEventos.Application
{
    public class AccountService : IAccountService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUserPersist _userPersist;

        public AccountService(UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              IMapper mapper,
                              IUserPersist userPersist,
                              IGeralPersist geralPersist
                              )
        {
             _geralPersist = geralPersist;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _userPersist = userPersist;
        }

        public async Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password)
        {
            try
            {
                var user = await _userManager.Users
                                             .SingleOrDefaultAsync(user => user.UserName == userUpdateDto.UserName.ToLower());

                return await _signInManager.CheckPasswordSignInAsync(user, password, false);
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar verificar password. Erro: {ex.Message}");
            }
        }
        public async Task<UserUpdateDto> GetUserByIdAsync(int id)
        {
              try
            {
                var user = await _userPersist.GetUserByIdAsync(id);
                if (user == null) return null;

                var userResultado = _mapper.Map<UserUpdateDto>(user);

                return userResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserUpdateDto> CreateAccountAsync(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (result.Succeeded)
                {
                    var userToReturn = _mapper.Map<UserUpdateDto>(user);
                    return userToReturn;
                }

                return null;
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar Criar Usuário. Erro: {ex.Message}");
            }
        }

        public async Task<PageList<UserUpdateDto>> GetAllUserUserAsync(PageParams pageParams)
        {
          try
            {
                var usuarios = await _userPersist.GetAllUsersAsync(pageParams);
                //var usuarios = await _userPersist.GetUsersAsync();
                if (usuarios == null) return null;

                 var resultado = _mapper.Map<PageList<UserUpdateDto>>(usuarios);
                 resultado.CurrentPage = usuarios.CurrentPage;
                    resultado.TotalPages = usuarios.TotalPages;
                    resultado.PageSize = usuarios.PageSize;
                    resultado.TotalCount = usuarios.TotalCount;
                     
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserUpdateDto> GetUserByUserNameAsync(string userName)
        {
            try
            {
                var user = await _userPersist.GetUserByUserNameAsync(userName);
                if (user == null) return null;

                var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
                return userUpdateDto;
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar pegar Usuário por Username. Erro: {ex.Message}");
            }
        }
         public async Task<UserUpdateDto> GetUserByNameAsync(string PrimeiroNome)
        {
           try
            {
                var user = await _userPersist.GetUseByPrimironomeAsync(PrimeiroNome);
                if (user == null) return null;

                var useDto = _mapper.Map<UserUpdateDto>(user);
                return useDto;
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar pegar Usuário por Nome. Erro: {ex.Message}");
            }
        }



        public async Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await _userPersist.GetUserByUserNameAsync(userUpdateDto.UserName);
                if (user == null) return null;

                userUpdateDto.Id = user.Id;

                _mapper.Map(userUpdateDto, user);

                // if (userUpdateDto.Password != null) {
                //     var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                //     await _userManager.ResetPasswordAsync(user, token, userUpdateDto.Password);
                // }

                _userPersist.Update<User>(user);

                if (await _userPersist.SaveChangesAsync())
                {
                    var userRetorno = await _userPersist.GetUserByUserNameAsync(user.UserName);

                    return _mapper.Map<UserUpdateDto>(userRetorno);
                }

                return null;
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar atualizar usuário. Erro: {ex.Message}");
            }
        }

        public async Task<bool> UserExists(string userName)
        {
            try
            {
                return await _userManager.Users
                                         .AnyAsync(user => user.UserName == userName.ToLower());
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao verificar se usuário existe. Erro: {ex.Message}");
            }
        }

       

        public async Task<UserUpdateDto> UpdateUser(int id, UserUpdateDto userUpdateDto)
        {
            try
        {
            var user = await _userPersist.GetUserByIdAsync(userUpdateDto.Id);
            if (user == null) return null;

            userUpdateDto.Id = user.Id;

            _mapper.Map(userUpdateDto,user);

             _userPersist.Update<User>(user);

            if (await _geralPersist.SaveChangesAsync())
            {
                 var grupoRetorno = await _userPersist.GetUserByIdAsync(user.Id);

                    return _mapper.Map<UserUpdateDto>(grupoRetorno);
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

        public async Task<IEnumerable<UserUpdateDto>> GetAllUserAsync()
        {
            try
            {
                var usuarios = await _userPersist.GetUsersAsync();
                if (usuarios == null) return null;

                 var resultado = _mapper.Map<UserUpdateDto[]>(usuarios);
                     
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}