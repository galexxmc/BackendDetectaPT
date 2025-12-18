using BackendPTDetecta.Application.Common.Models;
using BackendPTDetecta.Application.Features.Pacientes.Commands.ActualizarPaciente;
using BackendPTDetecta.Application.Features.Pacientes.Commands.CrearPaciente;
using BackendPTDetecta.Application.Features.Pacientes.Commands.EliminarPaciente;
using BackendPTDetecta.Application.Features.Pacientes.Queries.GetPacienteById;
using BackendPTDetecta.Application.Features.Pacientes.Queries.GetPacientesList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendPTDetecta.API.Controllers
{
    // Heredamos de ApiControllerBase.
    // Nota: Ya no es necesario poner [Route] ni [ApiController] porque ApiControllerBase ya los tiene.
    [Authorize]
    public class PacientesController : ApiControllerBase
    {
        // ❌ BORRADO: Constructor y variable _mediator.
        // ✅ AHORA USAMOS: La propiedad "Mediator" que heredamos del padre.

        // POST: api/Pacientes
        [HttpPost]
        public async Task<ActionResult<int>> Crear(CrearPacienteCommand command)
        {
            var id = await Mediator.Send(command); // Mediator con "M" mayúscula
            return Ok(id);
        }

        // GET: api/Pacientes
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedList<PacienteDto>>> Listar(
            [FromQuery] string? searchTerm,
            [FromQuery] int pageNumber = 1, 
            [FromQuery] int pageSize = 10)
        {
            var query = new GetPacientesListQuery 
            { 
                SearchTerm = searchTerm,
                PageNumber = pageNumber, 
                PageSize = pageSize 
            };

            var resultado = await Mediator.Send(query);
            return Ok(resultado);
        }

        // GET: api/Pacientes/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PacienteDetalleDto>> ObtenerPorId(int id)
        {
            var query = new GetPacienteByIdQuery(id);
            var paciente = await Mediator.Send(query);

            if (paciente == null) return NotFound($"No se encontró el paciente con ID {id}");

            return Ok(paciente);
        }

        // PUT: api/Pacientes/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Actualizar(int id, ActualizarPacienteCommand command)
        {
            if (id != command.Id)
                return BadRequest("El ID de la URL no coincide con el ID del cuerpo.");

            try
            {
                await Mediator.Send(command);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Pacientes/5?motivo=Error de digitacion
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Eliminar(int id, [FromQuery] string motivo) // <--- Nuevo parámetro en la URL
        {
            // Creamos el comando pasando ID y MOTIVO
            var command = new EliminarPacienteCommand(id, motivo);

            try
            {
                await Mediator.Send(command);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}