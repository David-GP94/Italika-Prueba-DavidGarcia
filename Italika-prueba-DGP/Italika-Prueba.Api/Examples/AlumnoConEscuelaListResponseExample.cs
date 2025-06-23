using Italika_Prueba.Application.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace Italika_Prueba.Api.Examples
{
    public class AlumnoConEscuelaListResponseExample : IExamplesProvider<IEnumerable<AlumnoConEscuelaDTO>>
    {
        public IEnumerable<AlumnoConEscuelaDTO> GetExamples()
        {
            return new List<AlumnoConEscuelaDTO>
        {
            new AlumnoConEscuelaDTO
            {
                Id = Guid.NewGuid(),
                Nombre = "Maria",
                ApellidoPaterno = "Lopez",
                ApellidoMaterno = "Martinez",
                FechaNacimiento = new DateTime(2005, 5, 10),
                Escuela = new EscuelaDTO
                {
                    Id = Guid.NewGuid(),
                    Nombre = "Escuela de Musica Beethoven",
                    Descripcion = "Clases de piano y violin."
                }
            },
            new AlumnoConEscuelaDTO
            {
                Id = Guid.NewGuid(),
                Nombre = "Carlos",
                ApellidoPaterno = "Ramirez",
                ApellidoMaterno = null, // Opcional
                FechaNacimiento = new DateTime(2006, 8, 15),
                Escuela = new EscuelaDTO
                {
                    Id = Guid.NewGuid(),
                    Nombre = "Escuela de Arte Mozart",
                    Descripcion = "Clases de guitarra y canto."
                }
            }
        };
        }
    }
}
