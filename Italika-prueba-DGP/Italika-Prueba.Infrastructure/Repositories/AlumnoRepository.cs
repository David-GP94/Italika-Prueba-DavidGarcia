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
    public class AlumnoRepository : IAlumnoRepository
    {
        private readonly ItalikaContext _context;

        public AlumnoRepository(ItalikaContext context)
        {
            _context = context;
        }

        public async Task<Guid> CrearAsync(Alumno alumno)
        {
            var parameters = new[]
            {
            new SqlParameter("@Id", alumno.Id),
            new SqlParameter("@Nombre", alumno.Nombre),
            new SqlParameter("@ApellidoPaterno", alumno.ApellidoPaterno),
            new SqlParameter("@ApellidoMaterno", (object?)alumno.ApellidoMaterno ?? DBNull.Value),
            new SqlParameter("@FechaNacimiento", alumno.FechaNacimiento)
        };
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_CrearAlumno @Id, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @FechaNacimiento", parameters);
            await _context.SaveChangesAsync();
            return alumno.Id;
        }

        public async Task<Alumno?> ObtenerPorIdAsync(Guid id)
        {
            var alumno = await _context.Alumnos
                .FromSqlRaw("EXEC sp_ObtenerAlumno @Id", new SqlParameter("@Id", id))
                .ToListAsync();
            return alumno.FirstOrDefault();
        }

        public async Task<IEnumerable<Alumno>> ObtenerTodosAsync()
        {
            var alumnos = await _context.Alumnos
               .FromSqlRaw("EXEC sp_ObtenerAlumnos")
               .ToListAsync();
            return alumnos;
        }

        public async Task ActualizarAsync(Alumno alumno)
        {
            var parameters = new[]
            {
            new SqlParameter("@Id", alumno.Id),
            new SqlParameter("@Nombre", alumno.Nombre),
            new SqlParameter("@ApellidoPaterno", alumno.ApellidoPaterno),
            new SqlParameter("@ApellidoMaterno", (object?)alumno.ApellidoMaterno ?? DBNull.Value),
            new SqlParameter("@FechaNacimiento", alumno.FechaNacimiento)
        };
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_ActualizarAlumno @Id, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @FechaNacimiento", parameters);
        }

        public async Task EliminarAsync(Guid id)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_EliminarAlumno @Id", new SqlParameter("@Id", id));
        }

        public async Task AsignarProfesorAsync(Guid alumnoId, Guid profesorId)
        {
            var alumno = await _context.Alumnos.FindAsync(alumnoId);
            var profesor = await _context.Profesores.FindAsync(profesorId);
            if (alumno == null )
            {
                throw new ArgumentException("Alumno no encontrado.");
            }
            else if (profesor == null)
            {
                throw new ArgumentException("Profesor no encontrado.");

            }


            alumno.Profesores.Add(profesor);
            await _context.SaveChangesAsync();
        }

        public async Task AsignarEscuelaAsync(Guid alumnoId, Guid escuelaId)
        {
            var alumno = await _context.Alumnos.FindAsync(alumnoId);
            var escuela = await _context.Escuelas.FindAsync(escuelaId);
            if (alumno == null || escuela == null)
                throw new ArgumentException("Alumno o Escuela no encontrada.");

            alumno.Escuelas.Add(escuela);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Alumno>> ObtenerPorProfesorIdAsync(Guid profesorId)
        {
            return await _context.Alumnos
                .Include(a => a.Profesores)
                .Include(a => a.Escuelas)
                .Where(a => a.Profesores.Any(p => p.Id == profesorId))
                .ToListAsync();
        }
    }
}
