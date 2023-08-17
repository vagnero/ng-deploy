
using System.Text.Json.Serialization;

namespace TreinamentoEvoSystems.Models
{
    public class DepartamentoModel
    {

        //Atributos do departamento.
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Sigla { get; set; }

        //Atributo onde será exibido uma lista de Funcionarios. Onde há a relação "Departamento tem muitos Funcionarios".
        [JsonIgnore] //No arquivo Json, há uma referência circular, por conta que o Departamento possui a collection Funcionarios, causando repetições. Com o JsonIgnore, foi possível remover tais repetições!
        public ICollection<FuncionarioModel>? Funcionarios { get; set; }
    }
}
