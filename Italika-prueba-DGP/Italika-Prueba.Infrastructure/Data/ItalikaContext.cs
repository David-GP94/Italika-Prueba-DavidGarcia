using Italika_Prueba.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Italika.Infrastructure.Data;

public class ItalikaContext : DbContext
{
    public ItalikaContext(DbContextOptions<ItalikaContext> options) : base(options) { }

    public DbSet<Escuela> Escuelas { get; set; }
    public DbSet<Profesor> Profesores { get; set; }
    public DbSet<Alumno> Alumnos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurar relaciones
        modelBuilder.Entity<Profesor>()
            .HasOne(p => p.Escuela)
            .WithMany(e => e.Profesores)
            .HasForeignKey(p => p.EscuelaId);

        // Relación N:N entre Alumnos y Profesores
        modelBuilder.Entity<Alumno>()
            .HasMany(a => a.Profesores)
            .WithMany(p => p.Alumnos)
            .UsingEntity<Dictionary<string, object>>(
                "AlumnoProfesor",
                j => j.HasOne<Profesor>().WithMany().HasForeignKey("ProfesorId"),
                j => j.HasOne<Alumno>().WithMany().HasForeignKey("AlumnoId"),
                j =>
                {
                    j.HasKey("AlumnoId", "ProfesorId");
                    j.Property("AlumnoId").HasColumnName("AlumnoId");
                    j.Property("ProfesorId").HasColumnName("ProfesorId");
                });

        // Relación N:N entre Alumnos y Escuelas
        modelBuilder.Entity<Alumno>()
            .HasMany(a => a.Escuelas)
            .WithMany(e => e.Alumnos)
            .UsingEntity<Dictionary<string, object>>(
                "AlumnoEscuela",
                j => j.HasOne<Escuela>().WithMany().HasForeignKey("EscuelaId"),
                j => j.HasOne<Alumno>().WithMany().HasForeignKey("AlumnoId"),
                j =>
                {
                    j.HasKey("AlumnoId", "EscuelaId");
                    j.Property("AlumnoId").HasColumnName("AlumnoId");
                    j.Property("EscuelaId").HasColumnName("EscuelaId");
                });

        // Configurar propiedades requeridas
        modelBuilder.Entity<Alumno>()
            .Property(a => a.Nombre).IsRequired();
        modelBuilder.Entity<Alumno>()
            .Property(a => a.ApellidoPaterno).IsRequired();
        modelBuilder.Entity<Alumno>()
            .Property(a => a.ApellidoMaterno).IsRequired(false);
        modelBuilder.Entity<Alumno>()
            .Property(a => a.FechaNacimiento).IsRequired();

        modelBuilder.Entity<Profesor>()
            .Property(p => p.Nombre).IsRequired();
        modelBuilder.Entity<Profesor>()
            .Property(p => p.ApellidoPaterno).IsRequired();
        modelBuilder.Entity<Profesor>()
            .Property(p => p.ApellidoMaterno).IsRequired(false);
    }

    public async Task InicializarBaseDatosAsync()
    {
        await Database.EnsureCreatedAsync();

        var sqlFilePath = Path.Combine(AppContext.BaseDirectory, "Scripts", "InitDatabase.sql");
        if (File.Exists(sqlFilePath))
        {
            var sql = await File.ReadAllTextAsync(sqlFilePath);
            await Database.ExecuteSqlRawAsync(sql);
        }
        else
        {
            throw new FileNotFoundException($"No se encontró el archivo SQL en la ruta: {sqlFilePath}");
        }
    }
}