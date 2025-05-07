using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;

public class ScreenSoundContext : DbContext
{
    public DbSet<Artista> Artistas { get; set; }
    public DbSet<Musica> Musicas { get; set; }
    public DbSet<Genero> Generos { get; set; }

    // Construtor para injeção de dependência (ASP.NET)
    public ScreenSoundContext(DbContextOptions<ScreenSoundContext> options) : base(options) { }

    // Construtor manual para console apps
    public ScreenSoundContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

        optionsBuilder.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString)
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Musica>()
            .HasMany(m => m.Generos)
            .WithMany(g => g.Musicas)
            .UsingEntity(j => j.ToTable("MusicasGeneros"));
    }
}
