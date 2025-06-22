using Italika_Prueba.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Italika_Prueba.Domain.Interfaces
{
    public interface IEscuelaRepository
    {
        Task<Guid> CrearAsync(Escuela escuela);
        Task<Escuela?> ObtenerPorIdAsync(Guid id);
        Task ActualizarAsync(Escuela escuela);
        Task EliminarAsync(Guid id);
        Task<IEnumerable<Escuela>> ObtenerTodasAsync();
    }
}
