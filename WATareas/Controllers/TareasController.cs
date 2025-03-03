using Datos.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Interfaces;

namespace WATareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly ITareaService _tareaService;

        public TareasController(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearTarea([FromBody] Tarea tarea)
        {
            try
            {
                var nuevaTarea = await _tareaService.CrearTareaAsync(tarea);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodasTareas([FromQuery] Estado? estado)
        {
            try
            {
                var tareas = await _tareaService.ObtenerTodasTareasAsync(estado);
                return Ok(tareas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarTarea(int id, [FromBody] Tarea tareaActualizada)
        {
            try
            {
                var tarea = await _tareaService.ActualizarTareaAsync(id, tareaActualizada);
                return Ok(tarea);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarTarea(int id)
        {
            try
            {
                await _tareaService.EliminarTareaAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}
