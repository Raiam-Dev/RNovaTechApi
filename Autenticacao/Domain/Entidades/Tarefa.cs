using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RNovaTech.Domain.Entidades
{
    public class Tarefa
    {
        [Key]
        public Guid Id { get; set; }

        [JsonIgnore]
        public Usuario Usuario { get; set; } = null!;

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        public string UsuarioUid { get; set; } = null!;

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(100)]
        public string Titulo { get; set; } = null!;

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(100)]
        public string Descricao { get; set; } = null!;

        public DateTime DataCriacao { get; set; }

        public DateTime? Vencimento { get; set; }

        public Status Status { get; set; }

        public Prioridade Prioridade { get; set; }
    }

    public enum Status
    {
        Pendente,
        EmProgresso,
        Concluido
    }
    public enum Prioridade
    {
        Baixa,
        Media,
        Alta
    }
}