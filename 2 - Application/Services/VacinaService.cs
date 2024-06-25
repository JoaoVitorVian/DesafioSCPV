using Application.DTO.PostoDeVacinacao;
using Application.DTO.Vacina;
using Application.Interfaces;
using AutoMapper;
using CadastroPostosVacinacao.Domain.Entities;
using Infra.Data.Repositories;
using Infra.Repositories.Interfaces;

namespace Application.Services
{
    public class VacinaService : IVacinaService
    {
        private readonly IVacinaRepository _vacinaRepository;
        private readonly IMapper _mapper;

        public VacinaService(IVacinaRepository vacinaRepository, IMapper mapper)
        {
            _vacinaRepository = vacinaRepository;
            _mapper = mapper;
        }

        public async Task<List<VacinaDTO>> SearchByName(string name)
        {
            var allVacinas= await _vacinaRepository.SearchByName(name);
            if (allVacinas == null)
            {
                throw new Exception("Nenhuma vacina encontrada!");
            }
            return _mapper.Map<List<VacinaDTO>>(allVacinas);
        }

        public async Task<IEnumerable<VacinaDTO>> GetAllAsync()
        {
            var vacinas = await _vacinaRepository.GetAll(); 

            if (vacinas == null)
            {
                throw new Exception("Nenhuma vacina encontrada!");
            }
            return _mapper.Map<IEnumerable<VacinaDTO>>(vacinas);
        }

        public async Task<VacinaDTO> GetByIdAsync(long id)
        {
            var vacina = await _vacinaRepository.Get(id);

            if(vacina == null)
            {
                throw new Exception("Nenhuma vacina encontrada!");
            }

            return _mapper.Map<VacinaDTO>(vacina);
        }

        public async Task<VacinaDTO> AddAsync(VacinaDTO vacinaDTO)
        {
            if (await _vacinaRepository.ExistsByLoteAsync(vacinaDTO.Lote))
            {
                throw new Exception("Já existe uma vacina com esse lote.");
            }

            if (vacinaDTO.DataValidade <= DateTime.Now)
            {
                throw new Exception("A data de validade da vacina deve ser uma data futura.");
            }

            var vacina = _mapper.Map<Vacina>(vacinaDTO);
            vacina.PostoDeVacinacaoId = vacinaDTO.PostoDeVacinacaoId == 0 ? (long?)null : vacinaDTO.PostoDeVacinacaoId;

            var createdVacina = await _vacinaRepository.Create(vacina);
            return _mapper.Map<VacinaDTO>(createdVacina);
        }

        public async Task<VacinaDTO> UpdateAsync(VacinaDTO vacinaDTO)
        {
            var vacina = await _vacinaRepository.Get(vacinaDTO.Id);

            if (vacina == null)
            {
                throw new Exception("Vacina não encontrada.");
            }

            if (await _vacinaRepository.ExistsByLoteAsync(vacinaDTO.Lote))
            {
                throw new Exception("Já existe uma vacina com esse lote.");
            }

            if (vacinaDTO.DataValidade <= DateTime.Now)
            {
                throw new Exception("A data de validade da vacina deve ser uma data futura.");
            }

            _mapper.Map(vacinaDTO, vacina);
            vacina.PostoDeVacinacaoId = vacinaDTO.PostoDeVacinacaoId == 0 ? (long?)null : vacinaDTO.PostoDeVacinacaoId;

            var updateVacina = await _vacinaRepository.Update(vacina);
            return _mapper.Map<VacinaDTO>(updateVacina);
        }

        public async Task DeleteAsync(long id)
        {
            var vacina = await _vacinaRepository.Get(id);
            if (vacina == null)
            {
                throw new Exception("Vacina não encontrada.");
            }

            await _vacinaRepository.Delete(vacina.Id);
        }
    }
}
