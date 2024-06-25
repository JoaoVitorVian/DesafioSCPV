using Application.DTO.PostoDeVacinacao;
using CadastroPostosVacinacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPostoDeVacinacaoService
    {
        Task<IEnumerable<PostoDeVacinacaoDTO>> GetAllAsync();
        Task<List<PostoDeVacinacaoDTO>> SearchByName(string name);
        Task<PostoDeVacinacaoDTO> GetByIdAsync(long id);
        Task<PostoDeVacinacaoDTO> AddAsync(PostoDeVacinacaoDTO postoDTO);
        Task<PostoDeVacinacaoDTO> UpdateAsync(PostoDeVacinacaoDTO posto);
        Task DeleteAsync(long id);
    }

}
