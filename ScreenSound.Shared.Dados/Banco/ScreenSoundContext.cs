using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;

public class ScreenSoundContext : DbContext
{
    public DbSet<Artista> Artistas { get; set; }
    public DbSet<Musica> Musicas { get; set; }
    public DbSet<Genero> Generos { get; set; }

    // Construtor para injeção de dependência (ASP.NET)
    public ScreenSoundContext(DbContextOptions<ScreenSoundContext> options) : base(options) { }

    // Configuração do modelo
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Musica>()
            .HasMany(m => m.Generos)
            .WithMany(g => g.Musicas)
            .UsingEntity(j => j.ToTable("MusicasGeneros"));
    }
}

// Fábrica para criar o DbContext em tempo de design
public class ScreenSoundContextFactory : IDesignTimeDbContextFactory<ScreenSoundContext>
{
    public ScreenSoundContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ScreenSoundContext>();
        var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ScreenSound;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        optionsBuilder
            .UseSqlServer(connectionString) // ✅ Agora usando SQL Server corretamente
            .UseLazyLoadingProxies();

        return new ScreenSoundContext(optionsBuilder.Options);
    }
}
