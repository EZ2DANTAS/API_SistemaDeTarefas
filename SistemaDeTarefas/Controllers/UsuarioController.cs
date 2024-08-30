using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;


    public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
    {
        _usuarioRepositorio = usuarioRepositorio;
    }

    // Metodo para Buscar os usuarios
    //ActionResult para retornar uma lista de Usuarios do Model
    [HttpGet]
    public async Task<ActionResult<List<UsuarioModel>>> BuscarTodosUsuarios()
    {
        List<UsuarioModel> users =  await _usuarioRepositorio.BuscarTodosUsuarios();
        return Ok(users);
    }


    [HttpGet("/{id}")]
    public async Task<ActionResult<UsuarioModel>> BuscarPorId(int id)
    {
        UsuarioModel user = await _usuarioRepositorio.BuscarPorId(id);
        return Ok(user);

    }

    [HttpPost]
    public async Task<ActionResult<UsuarioModel>> AdicionarUsuario([FromBody] UsuarioModel user)
    {
       return Ok(await _usuarioRepositorio.Adicionar(user));
    }


    [HttpPut()]
    public async Task<ActionResult<UsuarioModel>> AtualizarUsuario([FromBody ] UsuarioModel user, int idUser)
    {
        user.Id = idUser;
        return Ok(await _usuarioRepositorio.Atualizar(user, idUser));
    }


    [HttpDelete]
    public async Task<ActionResult<bool>> DeletarUsuario(int idUser)
    {
        return Ok(await _usuarioRepositorio.Deletar(idUser));
    }

}
