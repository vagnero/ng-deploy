using System.ComponentModel.DataAnnotations;

namespace TreinamentoEvoSystems.Models
{
    public class FuncionarioModel
    {
        //Atributos do departamento.
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Foto { get; set; }
        public string? Rg { get; set; }
        
        public int DepartamentoId { get; set; }

        //Atributo departamento, onde há a relação que o "Funcionario tem um atributo".
        public virtual DepartamentoModel? Departamento { get; set; }
    }
}
