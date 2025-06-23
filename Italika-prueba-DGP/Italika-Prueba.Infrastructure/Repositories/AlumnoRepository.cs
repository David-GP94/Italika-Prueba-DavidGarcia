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
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_AsignarProfesor @AlumnoId, @ProfesorId",
                    new SqlParameter("@AlumnoId", alumnoId),
                    new SqlParameter("@ProfesorId", profesorId));
            }
            catch (SqlException ex) when (ex.Number == 50001)
            {
                throw new InvalidOperationException("El profesor ya está ligado al alumno.");
            }
            catch (SqlException ex) when (ex.Number == 50002)
            {
                throw new ArgumentException("El alumno no existe.");
            }
            catch (SqlException ex) when (ex.Number == 50003)
            {
                throw new ArgumentException("El profesor no existe.");
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException($"Error al asignar el profesor: {ex.Message}", ex);
            }
        }

        public async Task AsignarEscuelaAsync(Guid alumnoId, Guid escuelaId)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_AsignarEscuela @AlumnoId, @EscuelaId",
                    new SqlParameter("@AlumnoId", alumnoId),
                    new SqlParameter("@EscuelaId", escuelaId));
            }
            catch (SqlException ex) when (ex.Number == 50004)
            {
                throw new InvalidOperationException("El alumno ya está inscrito en la escuela.");
            }
            catch (SqlException ex) when (ex.Number == 50002)
            {
                throw new ArgumentException("El alumno no existe.");
            }
            catch (SqlException ex) when (ex.Number == 50005)
            {
                throw new ArgumentException("La escuela no existe.");
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException($"Error al asignar la escuela: {ex.Message}", ex);
            }
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
