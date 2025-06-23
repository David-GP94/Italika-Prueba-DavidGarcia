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
    public class AlumnosController : ControllerBase
    {
        private readonly AlumnoService _alumnoService;

        public AlumnosController(AlumnoService alumnoService)
        {
            _alumnoService = alumnoService;
        }

        /// <summary>
        /// Crea un nuevo alumno.
        /// </summary>
        /// <param name="dto">Datos del alumno.</param>
        /// <returns>El ID del alumno creado.</returns>
        /// <response code="200">Alumno creado exitosamente.</response>
        /// <response code="400">Datos inválidos.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(CreateAlumnoDTO), typeof(CreateAlumnoDTOExample))]
        [SwaggerResponseExample(200, typeof(AlumnoCreatedResponseExample))]
        [SwaggerResponseExample(400, typeof(BadRequestResponseExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorResponseExample))]
        public async Task<IActionResult> Crear([FromBody] CreateAlumnoDTO dto)
        {
            try
            {
                var id = await _alumnoService.CrearAsync(dto);
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
        /// Obtiene todos los alumnos.
        /// </summary>
        /// <returns>Listado de todos los alumnos.</returns>
        /// <response code="200">Alumnos encontrados.</response>
        /// <response code="404">Ningun alumno registrado.</response>
        [HttpGet]
        [Route("ObtenerTodos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponseExample(200, typeof(AlumnoDTOExample))]
        [SwaggerResponseExample(404, typeof(NotFoundResponseExample))]
        public async Task<IActionResult> ObtenerTodos()
        {
            var alumnos = await _alumnoService.ObtenerTodosAsync();
            if (!alumnos.Any())
                return NotFound(new { Message = "Ningun alumno registrado." });
            return Ok(alumnos);
        }

        /// <summary>
        /// Obtiene un alumno por su ID.
        /// </summary>
        /// <param name="id">ID del alumno.</param>
        /// <returns>Detalles del alumno.</returns>
        /// <response code="200">Alumno encontrado.</response>
        /// <response code="404">Alumno no encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponseExample(200, typeof(AlumnoDTOExample))]
        [SwaggerResponseExample(404, typeof(NotFoundResponseExample))]
        public async Task<IActionResult> ObtenerPorId(Guid id)
        {
            var alumno = await _alumnoService.ObtenerPorIdAsync(id);
            if (alumno == null)
                return NotFound(new { Message = "Alumno no encontrado." });
            return Ok(alumno);
        }

        /// <summary>
        /// Actualiza un alumno existente.
        /// </summary>
        /// <param name="id">ID del alumno.</param>
        /// <param name="dto">Datos actualizados del alumno.</param>
        /// <returns>Confirmación de la actualización.</returns>
        /// <response code="200">Alumno actualizado exitosamente.</response>
        /// <response code="400">Datos inválidos.</response>
        /// <response code="404">Alumno no encontrado.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(CreateAlumnoDTO), typeof(CreateAlumnoDTOExample))]
        [SwaggerResponseExample(400, typeof(BadRequestResponseExample))]
        [SwaggerResponseExample(404, typeof(NotFoundResponseExample))]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] CreateAlumnoDTO dto)
        {
            try
            {
                await _alumnoService.ActualizarAsync(id, dto);
                return Ok(new { Message = "Alumno actualizado exitosamente." });
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
        /// Elimina un alumno por su ID.
        /// </summary>
        /// <param name="id">ID del alumno.</param>
        /// <returns>Confirmación de la eliminación.</returns>
        /// <response code="200">Alumno eliminado exitosamente.</response>
        /// <response code="404">Alumno no encontrado.</response>
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
                await _alumnoService.EliminarAsync(id);
                return Ok(new { Message = "Alumno eliminado exitosamente." });
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

        /// <summary>
        /// Asigna un profesor a un alumno.
        /// </summary>
        /// <param name="alumnoId">ID del alumno.</param>
        /// <param name="profesorId">ID del profesor.</param>
        /// <returns>Confirmación de la asignación.</returns>
        /// <response code="200">Asignación exitosa.</response>
        /// <response code="400">Alumno o profesor no encontrado.</response>
        [HttpPost("{alumnoId}/asignar-profesor/{profesorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponseExample(400, typeof(BadRequestResponseExample))]
        public async Task<IActionResult> AsignarProfesor(Guid alumnoId, Guid profesorId)
        {
            try
            {
                await _alumnoService.AsignarProfesorAsync(alumnoId, profesorId);
                return Ok("Profesor asignado correctamente.");
            }
            catch (InvalidOperationException ex) when (ex.Message == "El profesor ya está ligado al alumno.")
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Inscribe un alumno a una escuela.
        /// </summary>
        /// <param name="alumnoId">ID del alumno.</param>
        /// <param name="escuelaId">ID de la escuela.</param>
        /// <returns>Confirmación de la inscripción.</returns>
        /// <response code="200">Inscripción exitosa.</response>
        /// <response code="400">Alumno o escuela no encontrada.</response>
        [HttpPost("{alumnoId}/inscribir-escuela/{escuelaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponseExample(400, typeof(BadRequestResponseExample))]
        public async Task<IActionResult> AsignarEscuela(Guid alumnoId, Guid escuelaId)
        {
            try
            {
                await _alumnoService.AsignarEscuelaAsync(alumnoId, escuelaId);
                return Ok("Escuela asignada correctamente.");
            }
            catch (InvalidOperationException ex) when (ex.Message == "El alumno ya está inscrito en la escuela.")
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene los alumnos inscritos a un profesor, con la escuela asociada.
        /// </summary>
        /// <param name="profesorId">ID del profesor.</param>
        /// <returns>Lista de alumnos con su escuela.</returns>
        /// <response code="200">Alumnos encontrados.</response>
        /// <response code="400">Profesor no encontrado.</response>
        [HttpGet("por-profesor/{profesorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponseExample(200, typeof(AlumnoConEscuelaListResponseExample))]
        [SwaggerResponseExample(400, typeof(BadRequestResponseExample))]
        public async Task<IActionResult> ObtenerPorProfesorId(Guid profesorId)
        {
            try
            {
                var alumnos = await _alumnoService.ObtenerPorProfesorIdAsync(profesorId);
                return Ok(alumnos);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
