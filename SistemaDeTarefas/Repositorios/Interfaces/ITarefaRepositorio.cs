﻿using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Repositorios.Interfaces;

public interface ITarefaRepositorio
{
    Task<List<TarefaModel>> BuscarTodasTarefas();
    Task<TarefaModel> BuscarPorId(int id);
    Task<TarefaModel> Adicionar(TarefaModel tarefa);
    Task<TarefaModel> Atualizar(TarefaModel tarefa, int id);
    Task<bool> Deletar(int id);
}
