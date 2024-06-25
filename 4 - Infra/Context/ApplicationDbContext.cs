using CadastroPostosVacinacao.Domain.Common;
using CadastroPostosVacinacao.Domain.Entities;
using Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<PostoDeVacinacao> PostosDeVacinacao { get; set; }
        public DbSet<Vacina> Vacinas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VacinaMap());
            modelBuilder.ApplyConfiguration(new PostoDeVacinacaoMap());
        }
    }
}