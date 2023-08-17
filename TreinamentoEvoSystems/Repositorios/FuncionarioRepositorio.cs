using Microsoft.EntityFrameworkCore;
using TreinamentoEvoSystems.Data;
using TreinamentoEvoSystems.Models;
using TreinamentoEvoSystems.Repositorios.Interfaces;

namespace TreinamentoEvoSystems.Repositorios
{
    public class FuncionarioRepositorio : IFuncionarioRepositorios
    {
//O propósito da var de instância abaixo, é iniciar o dbCOntext neste repositório por meio do construtor.
        private readonly SistemaDBContext _dbContext; //Variável de instância que não pode ser alterada graças ao readonly.

        public FuncionarioRepositorio(SistemaDBContext sistemaDBContext) 
        { 
            _dbContext = sistemaDBContext;
        }    
        
        public async Task<FuncionarioModel> BuscarPorId(int id)
        {
            //O retorno abaixo pega todos os elementos e utiliza uma função lambda pra pegar o elemento igual o id inserido.
            return await _dbContext.Funcionarios
                .Include(x => x.Departamento)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<FuncionarioModel>> BuscarTodosFuncionarios()
        {
            //Nesse return não tem um lamdda, pq já é retornado tudo com o ToListAsync.
            return await _dbContext.Funcionarios
                .Include(x => x.Departamento)
                .ToListAsync();
        }

        public async Task<FuncionarioModel> Cadastrar(FuncionarioModel funcionario)
        {
            await _dbContext.Funcionarios.AddAsync(funcionario); //Adiciona o departamento.
            await _dbContext.SaveChangesAsync(); //Salva o departamento na base de dados.
            
            return funcionario;
        }

        public async Task<FuncionarioModel> Atualizar(FuncionarioModel funcionario, int id)
        {

            FuncionarioModel funcionarioPorId = await BuscarPorId(id);

            if (funcionarioPorId == null)
            {
                throw new Exception($"Funcionário do ID: {id} não encontrado no banco de dados.");
            }
            funcionarioPorId.Nome = funcionario.Nome;
            funcionarioPorId.Foto = funcionario.Foto;
            funcionarioPorId.Rg = funcionario.Rg;

            _dbContext.Funcionarios.Update(funcionarioPorId);
            await _dbContext.SaveChangesAsync();

            return funcionarioPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            FuncionarioModel funcionarioPorId = await BuscarPorId(id);

            if (funcionarioPorId == null)
            {
                throw new Exception($"Funcionário do ID: {id} não encontrado no banco de dados.");
            }

            _dbContext.Funcionarios.Remove(funcionarioPorId);
            await _dbContext.SaveChangesAsync();
            return true;    
        }
    }
}
