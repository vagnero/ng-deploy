using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreinamentoEvoSystems.Models;

namespace TreinamentoEvoSystems.Data.Map
{
//Herda o IEntity<DepartamentoModel> abaixo para poder configurar o mapeamento do DepartamentoModel.
    public class DepartamentoMap : IEntityTypeConfiguration<DepartamentoModel>
    {
        public void Configure(EntityTypeBuilder<DepartamentoModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(128);
            builder.Property(x => x.Sigla).IsRequired().HasMaxLength(20);

            builder.HasMany(d => d.Funcionarios)
               .WithOne(f => f.Departamento)
               .HasForeignKey(f => f.DepartamentoId)
               .OnDelete(DeleteBehavior.Cascade);

            // Define o comportamento de exclusão adequado

        }
    }
}
