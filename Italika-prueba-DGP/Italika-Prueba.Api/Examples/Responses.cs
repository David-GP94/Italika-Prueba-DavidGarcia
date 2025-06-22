using Swashbuckle.AspNetCore.Filters;

namespace Italika_Prueba.Api.Examples
{
    public class EscuelaCreatedResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new { Id = Guid.NewGuid() };
    }

    public class ProfesorCreatedResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new { Id = Guid.NewGuid() };
    }

    public class AlumnoCreatedResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new { Id = Guid.NewGuid() };
    }

    public class BadRequestResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new { Message = "Datos inválidos." };
    }

    public class InternalServerErrorResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new { Message = "Error interno del servidor." };
    }
    public class NotFoundResponseExample : IExamplesProvider<object>
    {
        public object GetExamples()
        {
            return new { Message = "Recurso no encontrado." };
        }
    }
}
