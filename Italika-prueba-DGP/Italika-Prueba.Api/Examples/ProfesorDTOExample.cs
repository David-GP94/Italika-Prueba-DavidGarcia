using Italika_Prueba.Application.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace Italika_Prueba.Api.Examples
{
    public class ProfesorDTOExample : IExamplesProvider<ProfesorDTO>
    {
        public ProfesorDTO GetExamples()
        {
            return new ProfesorDTO
            {
                Id = Guid.NewGuid(),
                Nombre = "Juan",
                ApellidoPaterno = "Perez",
                ApellidoMaterno = "Gomez",
                EscuelaId = Guid.NewGuid()
            };
        }
    }
}
