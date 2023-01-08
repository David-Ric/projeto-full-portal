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
    public class GrupoService : IGrupoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IGrupoPersist _grupoPersist;

        private readonly IMapper _mapper;

        public GrupoService(IGeralPersist geralPersist,
                            IGrupoPersist grupoPersist,
                            IMapper mapper)
        {
             _geralPersist = geralPersist;
            _grupoPersist = grupoPersist;
            _mapper = mapper;
        }
        public async Task<GrupoDto> AddGrupos(GrupoDto model)
        {
            try
            {
                var grupo = _mapper.Map<Grupos>(model);

                _geralPersist.Add<Grupos>(grupo);
                if (await _geralPersist.SaveChangesAsync())
                {
                    var retrono = await _grupoPersist.GetGrupoByIdAsync(grupo.Id);
                    
                    return _mapper.Map<GrupoDto>(retrono);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteGrupo(int id)
        {
            try
            {
                var grupo = await _grupoPersist.GetGrupoByIdAsync(id);
                if (grupo == null) throw new Exception("Grupo não encontrado!");
                _geralPersist.Delete<Grupos>(grupo);
                return await _geralPersist.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GrupoDto>> GetAllGruposAsync()
        {
            try
            {
                var grupos = await _grupoPersist.GetAllGruposAsync();
                if (grupos == null) return null;

                 var grupoResultado = _mapper.Map<GrupoDto[]>(grupos);
            
                return grupoResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GrupoDto> GetGrupoByIdAsync(int id)
        {
              try
            {
                var grupo = await _grupoPersist.GetGrupoByIdAsync(id);
                if (grupo == null) return null;

                var grupoResultado = _mapper.Map<GrupoDto>(grupo);

                return grupoResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GrupoDto> GetGrupoByNomeAsync(string nome)
        {
            try
            {
                var grupo = await _grupoPersist.GetGrupoByNomeAsync(nome);
                if (grupo == null) return null;
                var grupoResultado = _mapper.Map<GrupoDto>(grupo);
                
                return grupoResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GrupoDto> UpdateGrupo(int id, GrupoDto model)
        {
            try
            {
                var grupo = await _grupoPersist.GetGrupoByIdAsync(id);
                if (grupo == null) return null;

                model.Id = grupo.Id;
              
                _mapper.Map(model, grupo);

                _geralPersist.Update<Grupos>(grupo);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var grupoRetorno = await _grupoPersist.GetGrupoByIdAsync(model.Id);

                    return _mapper.Map<GrupoDto>(grupoRetorno);
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