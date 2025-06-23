using Italika_Prueba.Application.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace Italika_Prueba.Api.Examples
{
    public class EscuelaListResponseExample : IExamplesProvider<IEnumerable<EscuelaDTO>>
    {
        public IEnumerable<EscuelaDTO> GetExamples()
        {
            return new List<EscuelaDTO>
        {
            new EscuelaDTO
            {
                Id = Guid.NewGuid(),
                Nombre = "Escuela de Musica Beethoven",
                Descripcion = "Clases de piano y Violin."
            },
            new EscuelaDTO
            {
                Id = Guid.NewGuid(),
                Nombre = "Escuela de Arte Mozart",
                Descripcion = "Clases de guitarra y canto."
            }
        };
        }
    }
}
