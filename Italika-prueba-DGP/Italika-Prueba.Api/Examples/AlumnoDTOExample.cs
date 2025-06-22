using Italika_Prueba.Application.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace Italika_Prueba.Api.Examples
{
    public class AlumnoDTOExample : IExamplesProvider<AlumnoDTO>
    {
        public AlumnoDTO GetExamples()
        {
            return new AlumnoDTO
            {
                Id = Guid.NewGuid(),
                Nombre = "María",
                ApellidoPaterno = "López",
                ApellidoMaterno = "Martínez",
                FechaNacimiento = new DateTime(2005, 5, 10)
            };
        }
    }
}
