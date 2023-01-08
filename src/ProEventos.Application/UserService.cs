using System;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class UserService : IUserService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IUserPersist _userPersist;

        private readonly IMapper _mapper;

        public UserService(IGeralPersist geralPersist,
                            IUserPersist userPersist,
                            IMapper mapper)
        {
             _geralPersist = geralPersist;
            _userPersist = userPersist;
            _mapper = mapper;
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

        public Task<UserUpdateDto> UpdateUser(int id, UserUpdateDto userUpdateDto)
        {
            throw new NotImplementedException();
        }

        // public async Task<UserUpdateDto> UpdateUser(int id, UserUpdateDto userUpdateDto)
        // {
        //     try
        //     {
        //         var user = await _userPersist.GetUserByIdAsync(id);
        //         if (user == null) return null;

        //         userUpdateDto.Id = user.Id;

        //         _mapper.Map(userUpdateDto, user);

        //         _geralPersist.Update<UserUpdateDto>(user);

        //         if (await _geralPersist.SaveChangesAsync())
        //         {
        //             var userRetorno = await _userPersist.GetUserByIdAsync(userUpdateDto.Id);

        //             return _mapper.Map<UserUpdateDto>(userRetorno);
        //         }
        //         return null;
        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception(ex.Message);
        //     }
        // }


    }
}