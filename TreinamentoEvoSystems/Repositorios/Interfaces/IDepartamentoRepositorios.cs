using TreinamentoEvoSystems.Models;

namespace TreinamentoEvoSystems.Repositorios.Interfaces
{
    public interface IDepartamentoRepositorios
    {
        Task<List<DepartamentoModel>> BuscarTodosDepartamentos(); //Retorna todos os departamentos.
        Task<DepartamentoModel> BuscarPorId(int id); //Retorna o departamento igual ao id inserido.
        Task<DepartamentoModel> Cadastrar(DepartamentoModel departamento); //Adiciona um departamento.
        Task<DepartamentoModel> Atualizar(DepartamentoModel departamento, int id); //Atualiza o derpatamento por meio de um id.
        Task<bool> Apagar(int id); //Apaga o departamento igual ao id inserido.

        Task<List<FuncionarioModel>> ExibirTodosFuncionarios(int id); //Retorna todos os funcionários.
    }
}
