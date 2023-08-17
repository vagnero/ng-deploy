using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreinamentoEvoSystems.Models;

namespace TreinamentoEvoSystems.Data.Map
{
    //Herda o IEntity<FuncionarioModel> abaixo para poder configurar o mapeamento do FuncionarioModel.
    public class FuncionarioMap : IEntityTypeConfiguration<FuncionarioModel>
    {
        public void Configure(EntityTypeBuilder<FuncionarioModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(128);
            builder.Property(x => x.Foto).HasMaxLength(1000);
            builder.Property(x => x.Rg).IsRequired().HasMaxLength(15);
            builder.Property(x => x.DepartamentoId);
           
            builder.HasOne(x => x.Departamento);
        }
    }
}
