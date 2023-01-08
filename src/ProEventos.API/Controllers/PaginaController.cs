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

    public class Permissoes_das_PaginasController : ControllerBase
    {
       // <sumamary>
       // Permissões das páginas do portal.
       // </sumamary>
        private readonly IPaginasService _paginasService;
        public Permissoes_das_PaginasController(IPaginasService paginasService)
        {
            _paginasService = paginasService;

        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            try
            {
                var paginas = await _paginasService.GetAlPaginasAsync();
                if (paginas == null) return NoContent();


                return Ok(paginas);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar pagina. Erro: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var pagina = await _paginasService.GetPaginaByIdAsync(id);
                if (pagina == null) return NoContent();

                return Ok(pagina);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar pagina. Erro: {ex.Message}");
            }

        }
        [HttpPost]
        public async Task<IActionResult> Post(PaginaDto model)
        {
            try
            {
                var pagina = await _paginasService.AddPaginas(model);
                if (pagina == null) return NoContent();

                return Ok(pagina);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar salvar pagina. Erro: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PaginaDto model)
        {
            try
            {
                var pagina = await _paginasService.UpdatePagina(id, model);
                if (pagina == null) return NoContent();
                return Ok(pagina);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar pagina. Erro: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var pagina = await _paginasService.GetPaginaByIdAsync(id);

                if (pagina == null) return NoContent();

                return await _paginasService.DeletePagina(id) ?

                    Ok("Deletado com sucesso!") :

                    throw new Exception("Ocorreu um erro ao tentar deletar a pagina!");

            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar pagina. Erro: {ex.Message}");
            }
        }
    }
}