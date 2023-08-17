using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using TreinamentoEvoSystems.Models;
using TreinamentoEvoSystems.Repositorios.Interfaces;

namespace TreinamentoEvoSystems.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {

        //Var de instância para ser inicializada apartir do construtor. Isso dará acesso aos métodos "DepartamentoRepositorios".
        private readonly IFuncionarioRepositorios _funcionarioRepositorios;
        public FuncionarioController(IFuncionarioRepositorios funcionarioRepositorios) {
            _funcionarioRepositorios = funcionarioRepositorios;
        } 
       
        
        [HttpGet]
        public async Task<ActionResult<List<FuncionarioModel>>> GetFuncionarios()
        {
            List<FuncionarioModel> funcionarios = await _funcionarioRepositorios.BuscarTodosFuncionarios();
            return Ok(funcionarios);
        }

        [HttpGet("{id}")] //(padrão rest).
        public async Task<ActionResult<List<FuncionarioModel>>> GetFuncionario(int id)
        {
            FuncionarioModel funcionario = await _funcionarioRepositorios.BuscarPorId(id);
            return Ok(funcionario);
        }

        [HttpPost]
        [Route("Funcionarios")]
        public async Task<ActionResult<FuncionarioModel>> PostFuncionario([FromBody] FuncionarioModel funcionarioModel)
        {
            if (funcionarioModel == null)
            {
                return BadRequest("Dados inválidos para o novo funcionário.");
            }

            FuncionarioModel funcionario = await _funcionarioRepositorios.Cadastrar(funcionarioModel);
            //return CreatedAtAction("ObterFuncionarioPorId", new { id = funcionarioModel.Id }, funcionarioModel.Id);
            //return Ok(funcionario);
            return Ok(funcionario);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FuncionarioModel>> PutFuncionario([FromBody] FuncionarioModel funcionarioModel, int id)
        {
            funcionarioModel.Id = id;
            FuncionarioModel funcionario = await _funcionarioRepositorios.Atualizar(funcionarioModel, id);

            return Ok(funcionario);
        }

        [HttpDelete("{id}")]
       public async Task<ActionResult<FuncionarioModel>> DeleteFuncionario(int id)
        {
            bool deletado = await   _funcionarioRepositorios.Apagar(id);
            return Ok(deletado);

        }


    }
}
