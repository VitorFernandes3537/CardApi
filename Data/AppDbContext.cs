using CardApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace CardApi.Data;


public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Cartao> Cartoes => Set<Cartao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cliente>(cliente =>
        {
            cliente.Property(c => c.Nome).HasMaxLength(150);
            cliente.Property(c => c.Cpf).HasMaxLength(11).IsFixedLength().IsUnicode(false);
            cliente.HasIndex(c => c.Cpf).IsUnique();

        });

        modelBuilder.Entity<Cartao>(cartao =>
        {

            cartao.Property(c => c.Final).HasMaxLength(4).IsFixedLength().IsUnicode(false);
            cartao.Property(c => c.Limite).HasPrecision(18, 2);
            cartao.Property(c => c.LimiteDisponivel).HasPrecision(18, 2);
            cartao.Property(c => c.Bandeira).HasConversion<string>().HasMaxLength(20);

        });
    }
}