using Italika.Infrastructure.Data;
using Italika_Prueba.Domain.Entities;
using Italika_Prueba.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Italika_Prueba.Infrastructure.Repositories
{
    public class ProfesorRepository : IProfesorRepository
    {
        private readonly ItalikaContext _context;

        public ProfesorRepository(ItalikaContext context)
        {
            _context = context;
        }

        public async Task<Guid> CrearAsync(Profesor profesor)
        {
            var parameters = new[]
            {
            new SqlParameter("@Id", profesor.Id),
            new SqlParameter("@Nombre", profesor.Nombre),
            new SqlParameter("@ApellidoPaterno", profesor.ApellidoPaterno),
            new SqlParameter("@ApellidoMaterno", (object?)profesor.ApellidoMaterno ?? DBNull.Value),
            new SqlParameter("@EscuelaId", profesor.EscuelaId)
        };
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_CrearProfesor @Id, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @EscuelaId", parameters);
            await _context.SaveChangesAsync();
            return profesor.Id;
        }

        public async Task<IEnumerable<Profesor>> ObtenerTodosAsync()
        {
            var profesores = await _context.Profesores
               .FromSqlRaw("EXEC sp_ObtenerProfesores")
               .ToListAsync();
            return profesores;
        }

        public async Task<Profesor?> ObtenerPorIdAsync(Guid id)
        {
            var profesor = await _context.Profesores
                .FromSqlRaw("EXEC sp_ObtenerProfesor @Id", new SqlParameter("@Id", id))
                .ToListAsync();
            return profesor.FirstOrDefault();
        }

        public async Task ActualizarAsync(Profesor profesor)
        {
            var parameters = new[]
            {
            new SqlParameter("@Id", profesor.Id),
            new SqlParameter("@Nombre", profesor.Nombre),
            new SqlParameter("@ApellidoPaterno", profesor.ApellidoPaterno),
            new SqlParameter("@ApellidoMaterno", (object?)profesor.ApellidoMaterno ?? DBNull.Value),
            new SqlParameter("@EscuelaId", profesor.EscuelaId)
        };
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_ActualizarProfesor @Id, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @EscuelaId", parameters);
        }

        public async Task EliminarAsync(Guid id)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_EliminarProfesor @Id", new SqlParameter("@Id", id));
        }

        public async Task<IEnumerable<Profesor>> ObtenerPorEscuelaIdAsync(Guid escuelaId)
        {
            return await _context.Profesores
                .Where(p => p.EscuelaId == escuelaId)
                .ToListAsync();
        }

        public async Task<Profesor?> ObtenerConAlumnosYEscuelaAsync(Guid profesorId)
        {
            return await _context.Profesores
                .Include(p => p.Escuela)
                .Include(p => p.Alumnos)
                .FirstOrDefaultAsync(p => p.Id == profesorId);
        }
    }
}
