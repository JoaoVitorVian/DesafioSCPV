
using CadastroPostosVacinacao.Domain.Entities;
using Infra.Data.Context;
using Infra.Repositories;
using Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class VacinaRepository : BaseRepository<Vacina>, IVacinaRepository
    {
        private readonly ApplicationDbContext _context;

        public VacinaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByLoteAsync(string lote)
        {
            return await _context.Vacinas.AnyAsync(v => v.Lote == lote);
        }

        public async Task<IEnumerable<Vacina>> GetByPostoDeVacinacaoId(long postoDeVacinacaoId)
        {
            return await _context.Vacinas
                .Where(v => v.PostoDeVacinacaoId == postoDeVacinacaoId)
                .ToListAsync();
        }

        public async Task<List<Vacina>> SearchByName(string nome)
        {
            var allUsers = await _context.Vacinas
                                        .Where(x => x.Nome.ToLower().Contains(nome.ToLower()))
                                        .AsNoTracking()
                                        .ToListAsync();

            return allUsers;
        }
    }
}
