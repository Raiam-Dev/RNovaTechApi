using System.ComponentModel.DataAnnotations;

namespace RNovaTech.Domain.Entidades
{
    public class Usuario
    {
        [Key]
        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        public string Uid { get; set; } = null!;

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        public string Email { get; set; } = null!;
        public ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
    }
}
