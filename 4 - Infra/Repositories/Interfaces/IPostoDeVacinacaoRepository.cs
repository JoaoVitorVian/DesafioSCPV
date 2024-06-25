using CadastroPostosVacinacao.Domain.Entities;
using Infra.Interfaces;

namespace Infra.Repositories.Interfaces
{
    public interface IPostoDeVacinacaoRepository : IBaseRepository<PostoDeVacinacao>
    {
        Task<List<PostoDeVacinacao>> SearchByName(string nome);
        Task<bool> ExistsAsync(string nome);
    }
}