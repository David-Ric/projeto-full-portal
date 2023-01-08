using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;

namespace ProEventos.API.Controllers
{   [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    [Route("api/[controller]")]
    
    public class PermissaoController : ControllerBase
    {
        private readonly IPermissoesService _permissoesService;

        public PermissaoController(IPermissoesService permissoesService)
        {
            _permissoesService = permissoesService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            try
            {
                var permissoes = await _permissoesService.GetAllPermissoesAsync();
                if (permissoes == null) return NoContent();


                return Ok(permissoes);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar permissoes. Erro: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var permissao = await _permissoesService.GetPermissaoByIdAsync(id);
                if (permissao == null) return NoContent();

                return Ok(permissao);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar permissao. Erro: {ex.Message}");
            }

        }
        [HttpPost]
        public async Task<IActionResult> Post(PermissoesDto model)
        {
            try
            {
                var permissao = await _permissoesService.AddPermissoes(model);
                if (permissao == null) return NoContent();

                return Ok(permissao);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar salvar permissao. Erro: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PermissoesDto model)
        {
            try
            {
                var permissao = await _permissoesService.UpdatePermissao(id, model);
                if (permissao == null) return NoContent();
                return Ok(permissao);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar permissao. Erro: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var permissao = await _permissoesService.GetPermissaoByIdAsync(id);

                if (permissao == null) return NoContent();

                return await _permissoesService.DeletePermissao(id) ?

                    Ok("Deletado com sucesso!") :

                    throw new Exception("Ocorreu um erro ao tentar deletar a permissão!");

            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar permissão. Erro: {ex.Message}");
            }
        }
    }
}