using CadastroPostosVacinacao.Domain.Entities;
using Infra.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using Application.DTO.PostoDeVacinacao;
using Infra.Data.Repositories;
using Application.DTO.Vacina;

namespace Application.Services
{
    public class PostoDeVacinacaoService : IPostoDeVacinacaoService
    {
        private readonly IPostoDeVacinacaoRepository _postoDeVacinacaoRepository; 
        private readonly IVacinaRepository _vacinaRepository;
        private readonly IMapper _mapper;

        public PostoDeVacinacaoService(IPostoDeVacinacaoRepository postoDeVacinacaoRepository, IMapper mapper, IVacinaRepository vacinaRepository)
        {
            _vacinaRepository = vacinaRepository;
            _postoDeVacinacaoRepository = postoDeVacinacaoRepository;
            _mapper = mapper;
        }

        public async Task<List<PostoDeVacinacaoDTO>> SearchByName(string name)
        {
            var allPostos = await _postoDeVacinacaoRepository.SearchByName(name);

            if (allPostos == null)
            {
                throw new Exception("Posto não encontrado.");
            }

            foreach (var posto in allPostos)
            {
                var vacinas = await _vacinaRepository.GetByPostoDeVacinacaoId(posto.Id);
                posto.Vacinas = vacinas.ToList();
            }

            return _mapper.Map<List<PostoDeVacinacaoDTO>>(allPostos);
        }

        public async Task<IEnumerable<PostoDeVacinacaoDTO>> GetAllAsync()
        {
            var postos = await _postoDeVacinacaoRepository.GetAll(); 

            if (postos == null)
            {
                throw new Exception("Posto não encontrado.");
            }

            foreach (var posto in postos)
            {
                var vacinas = await _vacinaRepository.GetByPostoDeVacinacaoId(posto.Id);
                posto.Vacinas = vacinas.ToList();
            }

            return _mapper.Map<IEnumerable<PostoDeVacinacaoDTO>>(postos);
        }

        public async Task<PostoDeVacinacaoDTO> GetByIdAsync(long id)
        {
            var posto = await _postoDeVacinacaoRepository.Get(id);
            if (posto == null)
            {
                throw new Exception("Posto não encontrado.");
            }

            var vacinas = await _vacinaRepository.GetByPostoDeVacinacaoId(id);
            posto.Vacinas = vacinas.ToList();

            return _mapper.Map<PostoDeVacinacaoDTO>(posto);
        }

        public async Task<PostoDeVacinacaoDTO> AddAsync(PostoDeVacinacaoDTO postoDTO)
        {
            await ValidatePostoDeVacinacaoNomeAsync(postoDTO.Nome);

            var posto = _mapper.Map<PostoDeVacinacao>(postoDTO);

            var createdPosto = await _postoDeVacinacaoRepository.Create(posto);
            return _mapper.Map<PostoDeVacinacaoDTO>(createdPosto);
        }

        public async Task<PostoDeVacinacaoDTO> UpdateAsync(PostoDeVacinacaoDTO postoViewModel)
        {
            var posto = await _postoDeVacinacaoRepository.Get(postoViewModel.Id);
            if (posto == null)
            {
                throw new Exception("Posto não encontrado.");
            }

            await ValidatePostoDeVacinacaoNomeAsync(postoViewModel.Nome);

            _mapper.Map(postoViewModel, posto);

            var updatePosto = await _postoDeVacinacaoRepository.Update(posto);
            return _mapper.Map<PostoDeVacinacaoDTO>(updatePosto);
        }

        public async Task DeleteAsync(long id)
        {
            var posto = await _postoDeVacinacaoRepository.Get(id);
            if (posto == null)
            {
                throw new Exception("Posto não encontrado.");
            }

            var vacinas = await GetVacinasByPostoDeVacinacaoId(id);
            if (vacinas.Any())
            {
                throw new Exception("Postos de vacinação com vacinas associadas não podem ser excluídos.");
            }

            await _postoDeVacinacaoRepository.Delete(posto.Id);
        }

        public async Task<IEnumerable<VacinaDTO>> GetVacinasByPostoDeVacinacaoId(long postoDeVacinacaoId)
        {
            var vacinas = await _vacinaRepository.GetByPostoDeVacinacaoId(postoDeVacinacaoId);
            return _mapper.Map<IEnumerable<VacinaDTO>>(vacinas);
        }

        private async Task ValidatePostoDeVacinacaoNomeAsync(string nome)
        {
            if (await _postoDeVacinacaoRepository.ExistsAsync(nome))
            {
                throw new Exception("Já existe um posto com esse nome.");
            }
        }
    }
}
