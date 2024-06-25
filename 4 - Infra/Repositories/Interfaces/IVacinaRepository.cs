using CadastroPostosVacinacao.Domain.Entities;
using Infra.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Infra.Repositories.Interfaces
{
    public interface IVacinaRepository : IBaseRepository<Vacina>
    {
        Task<IEnumerable<Vacina>> GetByPostoDeVacinacaoId(long postoDeVacinacaoId);
        Task<List<Vacina>> SearchByName(string nome);
        Task<bool> ExistsByLoteAsync(string lote);
    }
}