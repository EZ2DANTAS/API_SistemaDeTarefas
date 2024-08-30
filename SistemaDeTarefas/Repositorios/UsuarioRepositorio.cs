using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Repositorios;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    private readonly SistemaDeTarefasDBContext _dbContext;
    public UsuarioRepositorio(SistemaDeTarefasDBContext sistemaDeTarefasDBContext)
    {
        _dbContext = sistemaDeTarefasDBContext;
    }
    public async Task<UsuarioModel> BuscarPorId(int id)
    {
        return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
    {
        return await _dbContext.Usuarios.ToListAsync();
    }


    public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
    {
        // adiciona o usuario e salva as informações no banco com SaveChanges
        await _dbContext.Usuarios.AddAsync(usuario);
        await _dbContext.SaveChangesAsync();
        return usuario;
    }


    // chama o medodo de busca e retorna uma Exception se não encontrado
    // caso encontre atualiza com os novos dados do parametro e confirma alterações
    public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
    {
        UsuarioModel user = await BuscarPorId(id);

        if (user == null)
        {
            throw new Exception($"Usuario para o ID: {id} Não encontrado no banco de dados");
        }
        
            user.Nome = usuario.Nome;
            user.Email = usuario.Email;

            _dbContext.Usuarios.Update(user);
            await _dbContext.SaveChangesAsync();
            return user;
    }


    public async Task<bool> Deletar(int id)
    {
        UsuarioModel user = await BuscarPorId(id);

        if (user == null)
        {
            throw new Exception($"Usuario para o ID: {id} Não encontrado no banco de dados");
        }

        _dbContext.Usuarios.Remove(user);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
