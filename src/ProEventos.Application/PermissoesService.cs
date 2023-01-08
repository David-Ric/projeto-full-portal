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
    public class PermissoesService : IPermissoesService
    {
          private readonly IGeralPersist _geralPersist;
        private readonly  IPermissoesPersist _permissoesPersist;

        private readonly IMapper _mapper;

        public PermissoesService(IGeralPersist geralPersist,
                            IPermissoesPersist permissoesPersist,
                            IMapper mapper)
        {
            _geralPersist = geralPersist;
            _permissoesPersist = permissoesPersist;
            _mapper = mapper;
        }
        public async Task<PermissoesDto> AddPermissoes(PermissoesDto model)
        {
            try
            {
                var permissao = _mapper.Map<Permissoes>(model);

                _geralPersist.Add<Permissoes>(permissao);
                if (await _geralPersist.SaveChangesAsync())
                {
                    var retrono = await _permissoesPersist.GetPermissoesById(permissao.Id);
                    
                    return _mapper.Map<PermissoesDto>(retrono);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeletePermissao(int id)
        {
           try
            {
                var permissao = await _permissoesPersist.GetPermissoesById(id);
                if (permissao == null) throw new Exception("Permissão não encontrado!");
                _geralPersist.Delete<Permissoes>(permissao);
                return await _geralPersist.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<PermissoesDto>> GetAllPermissoesAsync()
        {
            try
            {
                var permissoes = await _permissoesPersist.GetAllPertmissoesAssync();
                if (permissoes == null) return null;

                 var permissoesResultado = _mapper.Map<PermissoesDto[]>(permissoes);
            
                return permissoesResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PermissoesDto> GetPermissaoByIdAsync(int id)
        {
            try
            {
                var permissao = await _permissoesPersist.GetPermissoesById(id);
                if (permissao == null) return null;

                var permissoesResultado = _mapper.Map<PermissoesDto>(permissao);

                return permissoesResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PermissoesDto> UpdatePermissao(int id, PermissoesDto model)
        {
            try
            {
                var permissao = await _permissoesPersist.GetPermissoesById(id);
                if (permissao == null) return null;

                model.Id = permissao.Id;
              
                _mapper.Map(model, permissao);

                _geralPersist.Update<Permissoes>(permissao);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var permissaoRetorno = await _permissoesPersist.GetPermissoesById(model.Id);

                    return _mapper.Map<PermissoesDto>(permissaoRetorno);
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