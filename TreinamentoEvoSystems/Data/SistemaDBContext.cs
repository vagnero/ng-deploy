using Microsoft.EntityFrameworkCore;
using TreinamentoEvoSystems.Data.Map;
using TreinamentoEvoSystems.Models;

namespace TreinamentoEvoSystems.Data
{
    //SistemaDBContext herda a classe DbContext.
    public class SistemaDBContext : DbContext

    {
        //O construtor pega tem como parâmetro o DbContextOptions (ocorre um casting) e com isso é possível configurar o contexto.
        public SistemaDBContext(DbContextOptions<SistemaDBContext> options)
        : base(options)
        { }


      
        //Propriedades que geram tabelas no banco de dados.
        public DbSet<DepartamentoModel> Departamentos { get; set; }
        public DbSet<FuncionarioModel> Funcionarios { get; set; }


        //Método que permite a configuração do mapeamento das entidades (tabelas).
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DepartamentoMap());
            modelBuilder.ApplyConfiguration(new FuncionarioMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
