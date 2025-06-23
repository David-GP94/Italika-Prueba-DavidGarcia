using Italika_Prueba.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Italika_Prueba.Domain.Interfaces
{
    public interface IProfesorRepository
    {
        Task<Guid> CrearAsync(Profesor profesor);
        Task<Profesor?> ObtenerPorIdAsync(Guid id);
        Task<IEnumerable<Profesor?>> ObtenerTodosAsync();
        Task ActualizarAsync(Profesor profesor);
        Task EliminarAsync(Guid id);
        Task<IEnumerable<Profesor>> ObtenerPorEscuelaIdAsync(Guid escuelaId);
        Task<Profesor?> ObtenerConAlumnosYEscuelaAsync(Guid profesorId);
    }
}
