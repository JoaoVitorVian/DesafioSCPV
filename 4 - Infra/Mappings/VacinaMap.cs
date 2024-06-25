using CadastroPostosVacinacao.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
    public class VacinaMap : IEntityTypeConfiguration<Vacina>
    {
        public void Configure(EntityTypeBuilder<Vacina> builder)
        {
            builder.ToTable("Vacinas");

            builder.HasKey(x => x.Id); 

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(255); 

            builder.Property(x => x.Fabricante)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Lote)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Quantidade)
                .IsRequired();

            builder.Property(x => x.DataValidade)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.HasOne(x => x.PostoDeVacinacao)
                .WithMany(p => p.Vacinas)
                .HasForeignKey(x => x.PostoDeVacinacaoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
