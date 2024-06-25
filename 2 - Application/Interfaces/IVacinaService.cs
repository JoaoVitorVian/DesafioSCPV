using Application.DTO.Vacina;
using CadastroPostosVacinacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IVacinaService
    {
        Task<IEnumerable<VacinaDTO>> GetAllAsync();
        Task<VacinaDTO> GetByIdAsync(long id);
        Task<VacinaDTO> AddAsync(VacinaDTO vacinaViewModel);
        Task<VacinaDTO> UpdateAsync(VacinaDTO vacina);
        Task DeleteAsync(long id);
        Task<List<VacinaDTO>> SearchByName(string name);

    }
}
