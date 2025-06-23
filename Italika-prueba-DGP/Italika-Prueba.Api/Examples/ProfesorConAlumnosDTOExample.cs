using Italika_Prueba.Application.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace Italika_Prueba.Api.Examples
{
    public class ProfesorConAlumnosDTOExample : IExamplesProvider<ProfesorConAlumnosDTO>
    {
        public ProfesorConAlumnosDTO GetExamples()
        {
            return new ProfesorConAlumnosDTO
            {
                Id = Guid.NewGuid(),
                Nombre = "Juan",
                ApellidoPaterno = "Perez",
                ApellidoMaterno = "Gomez",
                Escuela = new EscuelaDTO
                {
                    Id = Guid.NewGuid(),
                    Nombre = "Escuela de Musica Beethoven",
                    Descripcion = "Clases de piano y Violin."
                },
                Alumnos = new List<AlumnoDTO>
            {
                new AlumnoDTO
                {
                    Id = Guid.NewGuid(),
                    Nombre = "Maria",
                    ApellidoPaterno = "Lopez",
                    ApellidoMaterno = "Martinez",
                    FechaNacimiento = new DateTime(2005, 5, 10)
                },
                new AlumnoDTO
                {
                    Id = Guid.NewGuid(),
                    Nombre = "Carlos",
                    ApellidoPaterno = "Ramírez",
                    ApellidoMaterno = null,
                    FechaNacimiento = new DateTime(2006, 8, 15)
                }
            }
            };
        }
    }
}
