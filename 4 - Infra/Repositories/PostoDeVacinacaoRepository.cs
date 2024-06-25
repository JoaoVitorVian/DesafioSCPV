using CadastroPostosVacinacao.Domain.Entities;
using Infra.Data.Context;
using Infra.Repositories;
using Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class PostoDeVacinacaoRepository : BaseRepository<PostoDeVacinacao>, IPostoDeVacinacaoRepository
    {
        private readonly ApplicationDbContext _context;

        public PostoDeVacinacaoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<PostoDeVacinacao>> SearchByName(string nome)
        {
            var allUsers = await _context.PostosDeVacinacao
                                        .Where(x => x.Nome.ToLower().Contains(nome.ToLower()))
                                        .AsNoTracking()
                                        .ToListAsync();

            return allUsers;
        }

        public async Task<bool> ExistsAsync(string nome)
        {
            return await _context.PostosDeVacinacao.AnyAsync(p => p.Nome == nome);
        }
    }
}
