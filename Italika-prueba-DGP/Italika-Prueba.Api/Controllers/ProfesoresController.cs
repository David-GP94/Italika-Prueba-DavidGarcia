using Italika_Prueba.Api.Examples;
using Italika_Prueba.Application.Dtos;
using Italika_Prueba.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace Italika_Prueba.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesoresController : ControllerBase
    {
        private readonly ProfesorService _profesorService;

        public ProfesoresController(ProfesorService profesorService)
        {
            _profesorService = profesorService;
        }

        /// <summary>
        /// Crea un nuevo profesor.
        /// </summary>
        /// <param name="dto">Datos del profesor.</param>
        /// <returns>El ID del profesor creado.</returns>
        /// <response code="200">Profesor creado exitosamente.</response>
        /// <response code="400">Datos inválidos o escuela no encontrada.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(CreateProfesorDTO), typeof(CreateProfesorDTOExample))]
        [SwaggerResponseExample(200, typeof(ProfesorCreatedResponseExample))]
        [SwaggerResponseExample(400, typeof(BadRequestResponseExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorResponseExample))]
        public async Task<IActionResult> Crear([FromBody] CreateProfesorDTO dto)
        {
            try
            {
                var id = await _profesorService.CrearAsync(dto);
                return Ok(new { Id = id });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Error interno del servidor." });
            }
        }

        /// <summary>
        /// Obtiene un profesor por su ID.
        /// </summary>
        /// <param name="id">ID del profesor.</param>
        /// <returns>Detalles del profesor.</returns>
        /// <response code="200">Profesor encontrado.</response>
        /// <response code="404">Profesor no encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponseExample(200, typeof(ProfesorDTOExample))]
        [SwaggerResponseExample(404, typeof(NotFoundResponseExample))]
        public async Task<IActionResult> ObtenerPorId(Guid id)
        {
            var profesor = await _profesorService.ObtenerPorIdAsync(id);
            if (profesor == null)
                return NotFound(new { Message = "Profesor no encontrado." });
            return Ok(profesor);
        }

        /// <summary>
        /// Obtiene un profesor con sus alumnos y escuela.
        /// </summary>
        /// <param name="id">ID del profesor.</param>
        /// <returns>Detalles del profesor, su escuela y alumnos.</returns>
        /// <response code="200">Profesor encontrado.</response>
        /// <response code="404">Profesor no encontrado.</response>
        [HttpGet("{id}/con-alumnos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponseExample(200, typeof(ProfesorConAlumnosDTOExample))]
        [SwaggerResponseExample(404, typeof(NotFoundResponseExample))]
        public async Task<IActionResult> ObtenerConAlumnosYEscuela(Guid id)
        {
            var profesor = await _profesorService.ObtenerConAlumnosYEscuelaAsync(id);
            if (profesor == null)
                return NotFound(new { Message = "Profesor no encontrado." });
            return Ok(profesor);
        }

        /// <summary>
        /// Actualiza un profesor existente.
        /// </summary>
        /// <param name="id">ID del profesor.</param>
        /// <param name="dto">Datos actualizados del profesor.</param>
        /// <returns>Confirmación de la actualización.</returns>
        /// <response code="200">Profesor actualizado exitosamente.</response>
        /// <response code="400">Datos inválidos o escuela no encontrada.</response>
        /// <response code="404">Profesor no encontrado.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(CreateProfesorDTO), typeof(CreateProfesorDTOExample))]
        [SwaggerResponseExample(400, typeof(BadRequestResponseExample))]
        [SwaggerResponseExample(404, typeof(NotFoundResponseExample))]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] CreateProfesorDTO dto)
        {
            try
            {
                await _profesorService.ActualizarAsync(id, dto);
                return Ok(new { Message = "Profesor actualizado exitosamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Error interno del servidor." });
            }
        }

        /// <summary>
        /// Elimina un profesor por su ID.
        /// </summary>
        /// <param name="id">ID del profesor.</param>
        /// <returns>Confirmación de la eliminación.</returns>
        /// <response code="200">Profesor eliminado exitosamente.</response>
        /// <response code="404">Profesor no encontrado.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(404, typeof(NotFoundResponseExample))]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            try
            {
                await _profesorService.EliminarAsync(id);
                return Ok(new { Message = "Profesor eliminado exitosamente." });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Error interno del servidor." });
            }
        }
    }
}
