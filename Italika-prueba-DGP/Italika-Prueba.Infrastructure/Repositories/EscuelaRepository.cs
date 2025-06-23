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
    public class EscuelaRepository : IEscuelaRepository
    {
        private readonly ItalikaContext _context;

        public EscuelaRepository(ItalikaContext context)
        {
            _context = context;
        }

        public async Task<Guid> CrearAsync(Escuela escuela)
        {
            var parameters = new[]
            {
            new SqlParameter("@Id", escuela.Id),
            new SqlParameter("@Nombre", escuela.Nombre),
            new SqlParameter("@Descripcion", escuela.Descripcion)
        };
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_CrearEscuela @Id, @Nombre, @Descripcion", parameters);
            await _context.SaveChangesAsync();
            return escuela.Id;
        }

        public async Task<Escuela?> ObtenerPorIdAsync(Guid id)
        {
            var escuela = await _context.Escuelas
                .FromSqlRaw("EXEC sp_ObtenerEscuela @Id", new SqlParameter("@Id", id))
                .ToListAsync();
            return escuela.FirstOrDefault();
        }

        public async Task ActualizarAsync(Escuela escuela)
        {
            var parameters = new[]
            {
            new SqlParameter("@Id", escuela.Id),
            new SqlParameter("@Nombre", escuela.Nombre),
            new SqlParameter("@Descripcion", escuela.Descripcion)
        };
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_ActualizarEscuela @Id, @Nombre, @Descripcion", parameters);
        }

        public async Task EliminarAsync(Guid id)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_EliminarEscuela @Id", new SqlParameter("@Id", id));
        }

        public async Task<IEnumerable<Escuela>> ObtenerTodasAsync()
        {
            return await _context.Escuelas.ToListAsync();
        }
    }
}
