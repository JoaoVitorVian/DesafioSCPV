using CadastroPostosVacinacao.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
    public class PostoDeVacinacaoMap : IEntityTypeConfiguration<PostoDeVacinacao>
    {
        public void Configure(EntityTypeBuilder<PostoDeVacinacao> builder)
        {
            builder.ToTable("PostosDeVacinacao");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Endereco)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.Cidade)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Estado)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(x => x.Vacinas)
                .WithOne(v => v.PostoDeVacinacao)
                .HasForeignKey(v => v.PostoDeVacinacaoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
