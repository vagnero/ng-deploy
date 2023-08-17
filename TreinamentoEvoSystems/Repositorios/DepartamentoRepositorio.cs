using Microsoft.EntityFrameworkCore;
using TreinamentoEvoSystems.Data;
using TreinamentoEvoSystems.Models;
using TreinamentoEvoSystems.Repositorios.Interfaces;

namespace TreinamentoEvoSystems.Repositorios
{
    public class DepartamentoRepositorio : IDepartamentoRepositorios
    {
        //O propósito da var de instância abaixo, é iniciar o dbCOntext neste repositório por meio do construtor.
        private readonly SistemaDBContext _dbContext; //Variável de instância que não pode ser alterada graças ao readonly.

        public DepartamentoRepositorio(SistemaDBContext sistemaDBContext)
        {
            _dbContext = sistemaDBContext;
        }

        public async Task<DepartamentoModel> BuscarPorId(int id)
        {
            //O retorno abaixo pega todos os elementos e utiliza uma função lambda pra pegar o elemento igual o id inserido.
            return await _dbContext.Departamentos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<DepartamentoModel>> BuscarTodosDepartamentos()
        {
            //Nesse return não tem um lamdda, pq já é retornado tudo com o ToListAsync.
            return await _dbContext.Departamentos.ToListAsync();
        }

        public async Task<DepartamentoModel> Cadastrar(DepartamentoModel departamento)
        {
            await _dbContext.Departamentos.AddAsync(departamento); //Adiciona o departamento.
            await _dbContext.SaveChangesAsync(); //Salva o departamento na base de dados.

            return departamento;
        }

        public async Task<DepartamentoModel> Atualizar(DepartamentoModel departamento, int id)
        {

            DepartamentoModel departamentoPorId = await BuscarPorId(id);

            if (departamentoPorId == null)
            {
                throw new Exception($"Departamento do ID: {id} não encontrado no banco de dados.");
            }
            departamentoPorId.Nome = departamento.Nome;
            departamentoPorId.Sigla = departamento.Sigla;


            _dbContext.Departamentos.Update(departamentoPorId);
            await _dbContext.SaveChangesAsync();

            return departamentoPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            DepartamentoModel departamentoPorId = await BuscarPorId(id);

            if (departamentoPorId == null)
            {
                throw new Exception($"Departamento do ID: {id} não encontrado no banco de dados.");
            }

            _dbContext.Departamentos.Remove(departamentoPorId);
            await _dbContext.SaveChangesAsync();
            return true;
        }


        public async Task<List<FuncionarioModel>> ExibirTodosFuncionarios(int id)
        {
            var departamento = _dbContext.Departamentos
        .Include(d => d.Funcionarios)
        .FirstOrDefault(d => d.Id == id);
            
            return departamento.Funcionarios.ToList(); // Verifique se 'departamento' é nulo aqui

        }
    }
}
