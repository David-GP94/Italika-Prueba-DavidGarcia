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
                Nombre = "Maria",
                ApellidoPaterno = "Lopez",
                ApellidoMaterno = "Martinez",
                FechaNacimiento = new DateTime(2005, 5, 10)
            };
        }
    }
}
