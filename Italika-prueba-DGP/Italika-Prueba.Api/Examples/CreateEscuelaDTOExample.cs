using Italika_Prueba.Application.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace Italika_Prueba.Api.Examples
{
    public class CreateEscuelaDTOExample : IExamplesProvider<CreateEscuelaDTO>
    {
        public CreateEscuelaDTO GetExamples()
        {
            return new CreateEscuelaDTO
            {
                Nombre = "Escuela de Música Beethoven",
                Descripcion = "Clases de piano y violín."
            };
        }
    }
}
