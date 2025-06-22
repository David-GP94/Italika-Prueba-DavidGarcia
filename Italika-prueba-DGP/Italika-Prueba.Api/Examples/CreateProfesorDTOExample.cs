using Italika_Prueba.Application.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace Italika_Prueba.Api.Examples
{
    public class CreateProfesorDTOExample : IExamplesProvider<CreateProfesorDTO>
    {
        public CreateProfesorDTO GetExamples()
        {
            return new CreateProfesorDTO
            {
                Nombre = "Juan",
                ApellidoPaterno = "Pérez",
                ApellidoMaterno = "Gómez",
                EscuelaId = Guid.NewGuid()
            };
        }
    }
}
