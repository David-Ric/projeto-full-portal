using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProEventos.Api.Helpers;
using ProEventos.API.Extensions;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Models;

namespace ProEventos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;
          private readonly ProEventosContext _context;
        private readonly IUtil _util;

        private readonly string _destino = "Perfil";

        public AccountController(IAccountService accountService,
                                 ITokenService tokenService,
                                 ProEventosContext context,
                                 IUtil util)
        {
            _util = util;
            _accountService = accountService;
            _tokenService = tokenService;
            _context = context;
        }
        // [HttpGet("Filter")]
   
        //   [AllowAnonymous]
        // public async Task<IActionResult> Get()
        // {

        //     try
        //     {
        //         var usuarios = await _accountService.GetAllUserAsync();
        //         if(usuarios == null) return NoContent();
               

        //         return Ok(usuarios);
        //     }
        //     catch (Exception ex)
        //     {
                
        //         return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar os usuarios. Erro: {ex.Message}");
        //     }
        // }
         [HttpGet]
          [AllowAnonymous]
        // public async Task<IActionResult> Get([FromServices]ProEventosContext context, int skip = 0, int take = 10)
         public async Task<IActionResult> Get([FromQuery] PageParams pageParams)
        {

            try
            {
                 var usuarios = await _accountService.GetAllUserUserAsync(pageParams);
                  if(usuarios == null) return NoContent();
                  Response.AddPagination(usuarios.CurrentPage, usuarios.PageSize, usuarios.TotalCount, usuarios.TotalPages);
            //    var usuarios = await context.Users.Skip(skip)
            //     .Take(take).ToListAsync();

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar os usuarios. Erro: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var grupo = await _accountService.GetUserByIdAsync(id);
                if(grupo == null) return NoContent();

                return Ok(grupo);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar grupo. Erro: {ex.Message}");
            }

        }
         [HttpGet("GetUserName")]
         [AllowAnonymous]
         public async Task<IActionResult> GetUserName(string PrimeiroNome)
         {
             try
             {
                // var userName = User.GetUserName();
                 var user = await _accountService.GetUserByNameAsync(PrimeiroNome);
                 return Ok(user);
             }
             catch (Exception ex)
             {
                 return this.StatusCode(StatusCodes.Status500InternalServerError,
                     $"Erro ao tentar recuperar Usuário. Erro: {ex.Message}");
             }
         }
         [HttpGet("GetUser")]
         [AllowAnonymous]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var userName = User.FindFirst(ClaimTypes.Name)?.Value;
                var user = await _accountService.GetUserByUserNameAsync(userName);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar Usuário. Erro: {ex.Message}");
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            try
            {
                if (await _accountService.UserExists(userDto.UserName))
                    return BadRequest("Usuário já existe");

                var user = await _accountService.CreateAccountAsync(userDto);
                if (user != null)
                    return Ok(new
                    {
                        userName = user.UserName,
                        PrimeroNome = user.PrimeiroNome,
                        token = _tokenService.CreateToken(user).Result
                    });

                return BadRequest("Usuário não criado, tente novamente mais tarde!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar Registrar Usuário. Erro: {ex.Message}");
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            try
            {
                var user = await _accountService.GetUserByUserNameAsync(userLogin.Username);
                if (user == null) return Unauthorized("Usuário ou Senha está errado");

                var result = await _accountService.CheckUserPasswordAsync(user, userLogin.Password);
                if (!result.Succeeded) return Unauthorized();

                return Ok(new
                {
                    userName = user.UserName,
                    PrimeiroNome = user.PrimeiroNome,
                    Grupo = user.Grupo,
                    Ativo = user.Ativo,
                    token = _tokenService.CreateToken(user).Result
                });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar realizar Login. Erro: {ex.Message}");
            }
        }

        [HttpPut("UpdateUser")]
         [AllowAnonymous]
        public async Task<IActionResult> UpdateUser(UserUpdateDto userUpdateDto)
        {
            try
            {
                // if (userUpdateDto.UserName != User.GetUserName())
                //     return Unauthorized("Usuário Inválido");

                var user = await _accountService.GetUserByUserNameAsync(User.GetUserName());
                if (user == null) return Unauthorized("Usuário Inválido");

                var userReturn = await _accountService.UpdateAccount(userUpdateDto);
                if (userReturn == null) return NoContent();

                return Ok(new
                {
                    userName = userReturn.UserName,
                    PrimeroNome = userReturn.PrimeiroNome,
                    token = _tokenService.CreateToken(userReturn).Result
                });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar Atualizar Usuário. Erro: {ex.Message}");
            }
        }

       [HttpPut("{id}")]
          [AllowAnonymous]
        public async Task<IActionResult> Put(int id, UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await _accountService.UpdateUser( id, userUpdateDto);
                if (user == null) return NoContent();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar usuário. Erro: {ex.Message}");
            }
        }

        [HttpPost("upload-image")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadImage()
        {
            try
            {
                var user = await _accountService.GetUserByUserNameAsync(User.GetUserName());
                if (user == null) return NoContent();

                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    _util.DeleteImage(user.ImagemURL, _destino);
                    user.ImagemURL = await _util.SaveImage(file, _destino);
                }
                var userRetorno = await _accountService.UpdateAccount(user);

                return Ok(userRetorno);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar realizar upload de Foto do Usuário. Erro: {ex.Message}");
            }
        }
    }
}