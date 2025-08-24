using Microsoft.EntityFrameworkCore;
using RNovaTech.Domain.Entidades;

namespace RNovaTech.Infra.Data
{
    public class DbContextProducao : DbContext
    {
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbContextProducao(DbContextOptions<DbContextProducao> config) : base(config) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.Uid);

            modelBuilder.Entity<Usuario>()
                .ToTable(name: "Usuarios", schema: "Autenticacao")
                .HasMany(u => u.Tarefas)
                .WithOne(t => t.Usuario)
                .HasForeignKey(t => t.UsuarioUid);

            modelBuilder.Entity<Tarefa>()
                .ToTable(name: "Tarefas", schema: "AplicacaoDeTarefas")
                .Property(t => t.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Tarefa>()
                .Property(t => t.Prioridade)
                .HasConversion<string>();

            modelBuilder.Entity<Tarefa>()
                .Property(d => d.DataCriacao)
                .HasDefaultValueSql("NOW()");
        }
    }
}
