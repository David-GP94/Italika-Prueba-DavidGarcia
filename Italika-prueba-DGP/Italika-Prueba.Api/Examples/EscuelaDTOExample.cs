using Italika_Prueba.Application.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace Italika_Prueba.Api.Examples
{
    public class EscuelaDTOExample : IExamplesProvider<EscuelaDTO>
    {
        public EscuelaDTO GetExamples()
        {
            return new EscuelaDTO
            {
                Id = Guid.NewGuid(),
                Nombre = "Escuela de Música Beethoven",
                Descripcion = "Clases de piano y violín."
            };
        }
    }
}
