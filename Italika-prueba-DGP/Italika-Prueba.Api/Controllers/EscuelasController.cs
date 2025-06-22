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
    public class EscuelasController : ControllerBase
    {
        private readonly EscuelaService _escuelaService;

        public EscuelasController(EscuelaService escuelaService)
        {
            _escuelaService = escuelaService;
        }

        /// <summary>
        /// Crea una nueva escuela.
        /// </summary>
        /// <param name="dto">Datos de la escuela.</param>
        /// <returns>El ID de la escuela creada.</returns>
        /// <response code="200">Escuela creada exitosamente.</response>
        /// <response code="400">Datos inválidos.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(CreateEscuelaDTO), typeof(CreateEscuelaDTOExample))]
        [SwaggerResponseExample(200, typeof(EscuelaCreatedResponseExample))]
        [SwaggerResponseExample(400, typeof(BadRequestResponseExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorResponseExample))]
        public async Task<IActionResult> Crear([FromBody] CreateEscuelaDTO dto)
        {
            try
            {
                var id = await _escuelaService.CrearAsync(dto);
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
        /// Obtiene una escuela por su ID.
        /// </summary>
        /// <param name="id">ID de la escuela.</param>
        /// <returns>Detalles de la escuela.</returns>
        /// <response code="200">Escuela encontrada.</response>
        /// <response code="404">Escuela no encontrada.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponseExample(200, typeof(EscuelaDTOExample))]
        [SwaggerResponseExample(404, typeof(NotFoundResponseExample))]
        public async Task<IActionResult> ObtenerPorId(Guid id)
        {
            var escuela = await _escuelaService.ObtenerPorIdAsync(id);
            if (escuela == null)
                return NotFound(new { Message = "Escuela no encontrada." });
            return Ok(escuela);
        }

        /// <summary>
        /// Obtiene todas las escuelas.
        /// </summary>
        /// <returns>Lista de escuelas.</returns>
        /// <response code="200">Escuelas obtenidas exitosamente.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponseExample(200, typeof(EscuelaListResponseExample))]
        public async Task<IActionResult> ObtenerTodas()
        {
            var escuelas = await _escuelaService.ObtenerTodasAsync();
            return Ok(escuelas);
        }

        /// <summary>
        /// Actualiza una escuela existente.
        /// </summary>
        /// <param name="id">ID de la escuela.</param>
        /// <param name="dto">Datos actualizados de la escuela.</param>
        /// <returns>Confirmación de la actualización.</returns>
        /// <response code="200">Escuela actualizada exitosamente.</response>
        /// <response code="400">Datos inválidos.</response>
        /// <response code="404">Escuela no encontrada.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(CreateEscuelaDTO), typeof(CreateEscuelaDTOExample))]
        [SwaggerResponseExample(400, typeof(BadRequestResponseExample))]
        [SwaggerResponseExample(404, typeof(NotFoundResponseExample))]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] CreateEscuelaDTO dto)
        {
            try
            {
                await _escuelaService.ActualizarAsync(id, dto);
                return Ok(new { Message = "Escuela actualizada exitosamente." });
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
        /// Elimina una escuela por su ID.
        /// </summary>
        /// <param name="id">ID de la escuela.</param>
        /// <returns>Confirmación de la eliminación.</returns>
        /// <response code="200">Escuela eliminada exitosamente.</response>
        /// <response code="404">Escuela no encontrada.</response>
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
                await _escuelaService.EliminarAsync(id);
                return Ok(new { Message = "Escuela eliminada exitosamente." });
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
