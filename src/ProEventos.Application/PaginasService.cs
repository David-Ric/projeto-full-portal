using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class PaginasService : IPaginasService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly  IPaginaPersist _paginaPersist;

        private readonly IMapper _mapper;

        public PaginasService(IGeralPersist geralPersist,
                            IPaginaPersist paginaPersist,
                            IMapper mapper)
        {
            _geralPersist = geralPersist;
            _paginaPersist = paginaPersist;
            _mapper = mapper;
        }

        public async Task<PaginaDto> AddPaginas(PaginaDto model)
        {
           try
            {
                var pagina = _mapper.Map<Paginas>(model);

                _geralPersist.Add<Paginas>(pagina);
                if (await _geralPersist.SaveChangesAsync())
                {
                    var retrono = await _paginaPersist.GetPaginaById(pagina.Id);
                    
                    return _mapper.Map<PaginaDto>(retrono);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeletePagina(int id)
        {
            try
            {
                var pagina = await _paginaPersist.GetPaginaById(id);
                if (pagina == null) throw new Exception("Página não encontrado!");
                _geralPersist.Delete<Paginas>(pagina);
                return await _geralPersist.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<PaginaDto>> GetAlPaginasAsync()
        {
            try
            {
                var paginas = await _paginaPersist.GetAllPaginasAssync();
                if (paginas == null) return null;

                 var paginaResultado = _mapper.Map<PaginaDto[]>(paginas);
            
                return paginaResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PaginaDto> GetPaginaByIdAsync(int id)
        {
            try
            {
                var pagina = await _paginaPersist.GetPaginaById(id);
                if (pagina == null) return null;

                var paginaResultado = _mapper.Map<PaginaDto>(pagina);

                return paginaResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PaginaDto> UpdatePagina(int id, PaginaDto model)
        {
            try
            {
                var pagina = await _paginaPersist.GetPaginaById(id);
                if (pagina == null) return null;

                model.Id = pagina.Id;
              
                _mapper.Map(model, pagina);

                _geralPersist.Update<Paginas>(pagina);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var paginaRetorno = await _paginaPersist.GetPaginaById(model.Id);

                    return _mapper.Map<PaginaDto>(paginaRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}