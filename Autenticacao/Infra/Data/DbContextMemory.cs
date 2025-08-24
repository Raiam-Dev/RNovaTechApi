using Microsoft.EntityFrameworkCore;
using RNovaTech.Domain.Entidades;

namespace AplicacaoWebApi.Infra.Data
{
    public class DbContextMemory : DbContext
    {
        public DbSet<Tarefa> Tarefas { get; set; } = null!;
        public DbContextMemory(DbContextOptions<DbContextMemory> config) : base(config) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarefa>()
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