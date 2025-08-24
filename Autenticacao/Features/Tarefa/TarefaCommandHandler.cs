using Aplicacao.Entidades;
using AplicacaoWebApi.Infra.Data;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RNovaTech.Domain.Entidades;
using RNovaTech.Infra.Data;

namespace Aplicacao.Entities.TarefaCommandHandler
{
    public class TarefaCommandHandler :
        IRequestHandler<AdicionarTarefa, Guid>,
        IRequestHandler<AtualizarTarefa, bool>
    {
        private readonly DbContextProducao _dbContext;

        public TarefaCommandHandler(DbContextProducao dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(AdicionarTarefa request,CancellationToken cancellationToken)
        {
            var tarefa = new Tarefa
            {
                Id = Guid.NewGuid(),
                UsuarioUid = request.UsuarioUid,
                Titulo = request.Titulo,
                Descricao = request.Descricao,
                DataCriacao = DateTime.UtcNow,
                Vencimento = request.Vencimento,
                Status = request.Status,
                Prioridade = request.Prioridade

            };

            await _dbContext.AddAsync(tarefa);
            await _dbContext.SaveChangesAsync();
            return tarefa.Id;
        }
        public async Task<bool> Handle(AtualizarTarefa request,CancellationToken cancellationToken)
        {
            if (!await VerificarExiste(request.Id)) return false;
            var tarefa = new Tarefa
            {
                Id = request.Id,
                UsuarioUid = request.UsuarioUid,
                Titulo = request.Titulo,
                Descricao = request.Descricao,
                DataCriacao = request.DataCriacao,
                Vencimento = request.Vencimento,
                Status = request.Status,
                Prioridade = request.Prioridade
            };

            _dbContext.Update(tarefa);

            await _dbContext.SaveChangesAsync();

            return true;
        }
        private async Task<bool> VerificarExiste (Guid id)
        {
            return await _dbContext.Tarefas.AnyAsync(t => t.Id == id);
        }
    }
}