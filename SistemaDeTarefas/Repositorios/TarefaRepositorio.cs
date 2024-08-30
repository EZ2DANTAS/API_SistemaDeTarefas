using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Repositorios;

public class TarefaRepositorio : ITarefaRepositorio
{
    private readonly SistemaDeTarefasDBContext _dbContext;
    public TarefaRepositorio(SistemaDeTarefasDBContext sistemaDeTarefasDBContext)
    {
        _dbContext = sistemaDeTarefasDBContext;
    }
    public async Task<TarefaModel> BuscarPorId(int id)
    {
        // include para trazer as informações do usuarios na tarefa
        return await _dbContext.Tarefas.Include(x=> x.Usuario)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<TarefaModel>> BuscarTodasTarefas()
    {
        // include para trazer as informações do usuarios na tarefa
        return await _dbContext.Tarefas.Include(x => x.Usuario).
            ToListAsync();
    }


    public async Task<TarefaModel> Adicionar(TarefaModel tarefa)
    {
        // adiciona o usuario e salva as informações no banco com SaveChanges
        await _dbContext.Tarefas.AddAsync(tarefa);
        await _dbContext.SaveChangesAsync();
        return tarefa;
    }


    // chama o medodo de busca e retorna uma Exception se não encontrado
    // caso encontre atualiza com os novos dados do parametro e confirma alterações
    public async Task<TarefaModel> Atualizar(TarefaModel tarefa, int id)
    {
        TarefaModel task = await BuscarPorId(id);

        if (task == null)
        {
            throw new Exception($"Tarefa para o ID: {id} Não encontrado no banco de dados");
        }

        task.Nome = tarefa.Nome;
        task.Descricao = tarefa.Descricao;
        task.Status = tarefa.Status;
        task.UsuarioId = tarefa.UsuarioId;

            _dbContext.Tarefas.Update(task);
            await _dbContext.SaveChangesAsync();
            return task;
    }


    public async Task<bool> Deletar(int id)
    {
        TarefaModel task = await BuscarPorId(id);

        if (task == null)
        {
            throw new Exception($"Tarefa para o ID: {id} Não encontrado no banco de dados");
        }

        _dbContext.Tarefas.Remove(task);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
