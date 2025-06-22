using Italika_Prueba.Application.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace Italika_Prueba.Api.Examples
{
    public class CreateAlumnoDTOExample : IExamplesProvider<CreateAlumnoDTO>
    {
        public CreateAlumnoDTO GetExamples()
        {
            return new CreateAlumnoDTO
            {
                Nombre = "María",
                ApellidoPaterno = "López",
                ApellidoMaterno = "Martínez",
                FechaNacimiento = new DateTime(2005, 5, 10)
            };
        }
    }
}
