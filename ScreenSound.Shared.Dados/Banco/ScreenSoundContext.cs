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

    // Construtor manual para console apps
    public ScreenSoundContext() { }

 

public class ScreenSoundContextFactory : IDesignTimeDbContextFactory<ScreenSoundContext>
{
    public ScreenSoundContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ScreenSoundContext>();
        var connectionString = "Server=127.0.0.1;Port=3306;Database=ScreenSound;User=root;Password=@Well32213115;";
        var serverVersion = ServerVersion.AutoDetect(connectionString);

        optionsBuilder
            .UseMySql(connectionString, serverVersion)
            .UseLazyLoadingProxies();

        return new ScreenSoundContext(optionsBuilder.Options);
    }
}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Musica>()
            .HasMany(m => m.Generos)
            .WithMany(g => g.Musicas)
            .UsingEntity(j => j.ToTable("MusicasGeneros"));
    }
}
