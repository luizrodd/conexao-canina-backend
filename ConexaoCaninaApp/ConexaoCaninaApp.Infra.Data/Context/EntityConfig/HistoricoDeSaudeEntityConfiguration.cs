using ConexaoCaninaApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConexaoCaninaApp.Infra.Data.Context.EntityConfig
{
    public class HistoricoDeSaudeEntityConfiguration : IEntityTypeConfiguration<HistoricoDeSaude>
    {
        public void Configure(EntityTypeBuilder<HistoricoDeSaude> builder)
        {
            builder.HasKey(h => h.HistoricoSaudeId);

            builder.Property(x => x.HistoricoSaudeId)
                .ValueGeneratedNever();

            builder.Property(h => h.Exame)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(h => h.Vacina)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(h => h.CondicoesDeSaude)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(h => h.ConsentimentoDono)
                .IsRequired();

            builder.Property(h => h.DataExame)
                .IsRequired();
        }
    }
}
