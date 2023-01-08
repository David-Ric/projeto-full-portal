using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;

namespace ProEventos.API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    [Route("api/[controller]")]

    public class GruposController : ControllerBase
    {
      private readonly IGrupoService _grupoService;

        public GruposController(IGrupoService grupoService)
        {
            _grupoService = grupoService;

        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            try
            {
                var grupos = await _grupoService.GetAllGruposAsync();
                if(grupos == null) return NoContent();
               

                return Ok(grupos);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar grupos. Erro: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var grupo = await _grupoService.GetGrupoByIdAsync(id);
                if(grupo == null) return NoContent();

                return Ok(grupo);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar grupo. Erro: {ex.Message}");
            }

        }
        
        [HttpPost]
        public async Task<IActionResult> Post( GrupoDto model)
        {
             try
            {
                var grupo = await _grupoService.AddGrupos(model);
                if(grupo == null) return NoContent();

                return Ok(grupo);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar salvar grupo. Erro: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, GrupoDto model)
        {
             try
            {
                var grupo = await _grupoService.UpdateGrupo(id, model);
                if(grupo == null) return NoContent();
                return Ok(grupo);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar grupo. Erro: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete( int id)
        {
             try
            {
                 var grupo = await _grupoService.GetGrupoByIdAsync(id);

                if(grupo == null) return NoContent();

                return await _grupoService.DeleteGrupo(id)?

                    Ok("Deletado com sucesso!") :
                    
                    throw new Exception("Ocorreu um erro ao tentar deletar o grupo!");

            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar grupo. Erro: {ex.Message}");
            }
        }
  
    }
}