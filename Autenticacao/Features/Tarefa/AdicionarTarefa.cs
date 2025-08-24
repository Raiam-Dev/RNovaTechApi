using System.ComponentModel.DataAnnotations;
using MediatR;
using RNovaTech.Domain.Entidades;

namespace Aplicacao.Entidades
{
    public class AdicionarTarefa : IRequest<Guid>
    {
        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        public string UsuarioUid { get; set; } = null!;

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(100)]

        public string Titulo { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(100)]
        public string Descricao { get; set; }

        public DateTime? Vencimento { get; set; }

        public Status Status { get; set; }

        public Prioridade Prioridade { get; set; }
    }
}