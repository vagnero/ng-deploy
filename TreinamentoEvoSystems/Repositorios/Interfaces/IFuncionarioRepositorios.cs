using TreinamentoEvoSystems.Models;

namespace TreinamentoEvoSystems.Repositorios.Interfaces
{
    public interface IFuncionarioRepositorios
    {
        Task<List<FuncionarioModel>> BuscarTodosFuncionarios(); //Retorna todos os departamentos.
        Task<FuncionarioModel> BuscarPorId(int id); //Retorna o departamento igual ao id inserido.
        Task<FuncionarioModel> Cadastrar(FuncionarioModel funcionario); //Adiciona um departamento.
        Task<FuncionarioModel> Atualizar(FuncionarioModel funcionario, int id); //Atualiza o derpatamento por meio de um id.
        Task<bool> Apagar(int id); //Apaga o departamento igual ao id inserido.
        
    }
}
