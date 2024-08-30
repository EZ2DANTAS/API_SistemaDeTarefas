using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TarefaController : ControllerBase
{
    private readonly ITarefaRepositorio _tarefaRepositorio;


    public TarefaController(ITarefaRepositorio tarefaRepositorio)
    {
        _tarefaRepositorio = tarefaRepositorio;
    }

    // Metodo para Buscar os usuarios
    //ActionResult para retornar uma lista de Usuarios do Model
    [HttpGet]
    public async Task<ActionResult<List<TarefaModel>>> BuscarTodasTarefas()
    {
        List<TarefaModel> tarefas =  await _tarefaRepositorio.BuscarTodasTarefas();
        return Ok(tarefas);
    }


    [HttpGet("/{id}")]
    public async Task<ActionResult<TarefaModel>> BuscarTarefaPorId(int id)
    {
        TarefaModel tarefa = await _tarefaRepositorio.BuscarPorId(id);
        return Ok(tarefa);

    }

    [HttpPost]
    public async Task<ActionResult<TarefaModel>> AdicionarTarefa([FromBody] TarefaModel tarefa)
    {
       return Ok(await _tarefaRepositorio.Adicionar(tarefa));
    }


    [HttpPut()]
    public async Task<ActionResult<TarefaModel>> AtualizarUsuario([FromBody ] TarefaModel tarefa, int idTarefa)
    {
        tarefa.Id = idTarefa;
        return Ok(await _tarefaRepositorio.Atualizar(tarefa, idTarefa));
    }


    [HttpDelete]
    public async Task<ActionResult<bool>> DeletarUsuario(int idTarefa)
    {
        return Ok(await _tarefaRepositorio.Deletar(idTarefa));
    }

}
