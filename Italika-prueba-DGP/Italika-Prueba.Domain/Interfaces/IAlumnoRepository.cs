using Italika_Prueba.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Italika_Prueba.Domain.Interfaces
{
    public interface IAlumnoRepository
    {
        Task<Guid> CrearAsync(Alumno alumno);
        Task<Alumno?> ObtenerPorIdAsync(Guid id);
        Task<IEnumerable<Alumno>> ObtenerTodosAsync();
        Task ActualizarAsync(Alumno alumno);
        Task EliminarAsync(Guid id);
        Task AsignarProfesorAsync(Guid alumnoId, Guid profesorId);
        Task AsignarEscuelaAsync(Guid alumnoId, Guid escuelaId);
        Task<IEnumerable<Alumno>> ObtenerPorProfesorIdAsync(Guid profesorId);
    }
}
