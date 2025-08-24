using Microsoft.AspNetCore.Mvc;
using MediatR;
using Aplicacao.Entidades;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using AplicacaoWebApi.Features.Shared;
using RNovaTech.Infra.Data;
using RNovaTech.Domain.Entidades;
using RNovaTech.Features.Shared.Atributo;
using RNovaTech.Features._Shared;

namespace Aplicacao.Controller
{
    [Route("api/[controller]")]
    //[FirebaseAutenticacao]
    public class TarefaController : ApiController
    {
        private readonly IMediator _mediator;
        //private readonly Usuario _usuario;

        public TarefaController(IMediator mediatR)
        {
            _mediator = mediatR;
            //_usuario = firebaseToken.GetCurrentUser(httpContextAccessor.HttpContext!);
        }

        [HttpGet("pendentes")]
        [SwaggerOperation(
            Summary = "Busca todas as tarefas pendentes",
            Description = "Retorna uma lista de tarefas pendentes"
        )]
        [ProducesResponseType(typeof(CustomResult),StatusCodes.Status200OK)]
        public async Task<IActionResult> Pendentes(
            [FromServices] DbContextProducao _dbContext)
        {
            var tarefasPendentes = await _dbContext.Tarefas
                .Where(t => t.Status == Status.Pendente && t.UsuarioUid == "b7253657-8542-4378-8d4e-8219b265b38b")
                .ToListAsync();

            return ResponseOk(tarefasPendentes);
        }

        [HttpGet("prioritarias")]
        [SwaggerOperation(
            Summary = "Busca todas as tarefas prioritárias",
            Description = "Retorna uma lista de tarefas com prioridade alta"
        )]
        [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Prioritarias([FromServices] DbContextProducao _dbContext)
        {
            var tarefasPrioritarias = await _dbContext.Tarefas
                .Where(t => t.Prioridade == Prioridade.Alta && t.UsuarioUid == "b7253657-8542-4378-8d4e-8219b265b38b")
                .ToListAsync();

            return ResponseOk(tarefasPrioritarias);
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista todas as tarefas",
            Description = "Retorna uma lista de todas as tarefas cadastradas"
        )]
        [ProducesResponseType(typeof(CustomResult),StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarTarefas([FromServices] DbContextProducao _dbContext)
        {
            var tarefas = await _dbContext.Tarefas.Where(t => t.UsuarioUid == "b7253657-8542-4378-8d4e-8219b265b38b").ToListAsync();

            return ResponseOk(tarefas);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Adiciona uma nova tarefa ao sistema, retornando o ID da tarefa criada",
            Description = "Caso não queira mandar data de vencimento mande ela no corpo como null"
        )]
        [ProducesResponseType(typeof(CustomResult),StatusCodes.Status201Created)]
        public async Task<IActionResult> CriarTarefa(AdicionarTarefa tarefa)
        {
            var resposta = await _mediator.Send(tarefa);

            return ResponseCreated(resposta);
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Atualiza uma tarefa existente",
            Description = "Atualiza os detalhes de uma tarefa existente, retornando sucesso ou falha"
        )]
        [ProducesResponseType(typeof(CustomResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CustomResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTarefa(AtualizarTarefa tarefa)
        {
            var resposta = await _mediator.Send(tarefa);

            if (!resposta) 
                return ResponseNotFound(erros: "Tarefa não existe");

            return NoContent();
        }

        [HttpDelete]
        [SwaggerOperation(
            Summary = "Exclui uma tarefa",
            Description = "Remove uma tarefa do sistema pelo ID, retornando sucesso ou falha"
        )]
        [ProducesResponseType(typeof(CustomResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CustomResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExcluirTarefa([FromServices] DbContextProducao _dbContext,Guid id)
        {
            var tarefa = await _dbContext.Tarefas.FindAsync(id);

            if (tarefa == null)
                return ResponseNotFound(erros: "Tarefa não encontrada");

            _dbContext.Tarefas.Remove(tarefa);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}