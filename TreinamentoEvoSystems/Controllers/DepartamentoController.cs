using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using TreinamentoEvoSystems.Data;
using TreinamentoEvoSystems.Models;
using TreinamentoEvoSystems.Repositorios;
using TreinamentoEvoSystems.Repositorios.Interfaces;

namespace TreinamentoEvoSystems.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {

        //Var de instância para ser inicializada apartir do construtor. Isso dará acesso aos métodos "DepartamentoRepositorios".
        private readonly IDepartamentoRepositorios _departamentoRepositorios;
        private readonly IFuncionarioRepositorios _funcionarioRepositorios;
        

        public DepartamentoController(IDepartamentoRepositorios departamentoRepositorios) { 
        _departamentoRepositorios = departamentoRepositorios;
        }


        [HttpGet("")]
        public async Task<ActionResult<List<DepartamentoModel>>> GetDepartamentos()
        {
            List<DepartamentoModel> departamentos = await _departamentoRepositorios.BuscarTodosDepartamentos();
            return Ok(departamentos);
        }

        [HttpGet("{id}")] //Personalização para exibir no API (padrão rest).
        public async Task<ActionResult<List<DepartamentoModel>>> GetDepartamento(int id)
        {
            DepartamentoModel departamento = await _departamentoRepositorios.BuscarPorId(id);
            return Ok(departamento);
        }

        [HttpPost]
        public async Task<ActionResult<DepartamentoModel>> PostDepartamento([FromBody] DepartamentoModel departamentoModel)
        {
            DepartamentoModel departamento = await _departamentoRepositorios.Cadastrar(departamentoModel);
            return Ok(departamento);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DepartamentoModel>> PutDepartamento([FromBody] DepartamentoModel departamentoModel, int id)
        {
            departamentoModel.Id = id;
            DepartamentoModel departamento = await _departamentoRepositorios.Atualizar(departamentoModel,id);
            return Ok(departamento);

        }

        [HttpDelete("{id}")]
       public async Task<ActionResult<DepartamentoModel>> DeleteDepartamento(int id)
        {
            bool deletado = await _departamentoRepositorios.Apagar(id);
            return Ok(deletado);

        }


        [HttpGet("Funcionarios")]
        public async Task<ActionResult<List<FuncionarioModel>>> GetFuncionarios(int id)
        {
            var funcionarios = await _departamentoRepositorios.ExibirTodosFuncionarios(id);
            return Ok(funcionarios);
        }


    }
}
