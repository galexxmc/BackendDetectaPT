using BackendPTDetecta.Application.Common.Models;
using BackendPTDetecta.Application.Features.Pacientes.Commands.ActualizarPaciente;
using BackendPTDetecta.Application.Features.Pacientes.Commands.CrearPaciente;
using BackendPTDetecta.Application.Features.Pacientes.Commands.EliminarPaciente;
using BackendPTDetecta.Application.Features.Pacientes.Queries.GetPacienteById;
using BackendPTDetecta.Application.Features.Pacientes.Queries.GetPacientesList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BackendPTDetecta.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // La URL será: api/pacientes
    public class PacientesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PacientesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/pacientes
        [HttpPost]
        public async Task<ActionResult<int>> Crear(CrearPacienteCommand command)
        {
            // Enviamos el comando al "Cerebro" (Handler)
            // Si la validación falla, MediatR/FluentValidation lanzarán una excepción (luego lo manejaremos globalmente)
            // Si todo sale bien, nos devuelve el ID del paciente creado.

            var id = await _mediator.Send(command);

            return Ok(id);
        }

        // GET: api/Pacientes?pageNumber=1&pageSize=10
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

            var resultado = await _mediator.Send(query);
            
            return Ok(resultado);
        }

        // GET: api/Pacientes/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PacienteDetalleDto>> ObtenerPorId(int id)
        {
            var query = new GetPacienteByIdQuery(id);
            var paciente = await _mediator.Send(query);

            if (paciente == null)
            {
                return NotFound($"No se encontró el paciente con ID {id}");
            }

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
            {
                return BadRequest("El ID de la URL no coincide con el ID del cuerpo de la petición.");
            }

            // Si el ID no existe, el Handler lanzará KeyNotFoundException.
            // Lo ideal es tener un Middleware global de errores, pero por ahora ASP.NET lo manejará.
            try
            {
                await _mediator.Send(command);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent(); // 204 No Content es el estándar cuando se actualiza con éxito y no devuelves nada
        }

        // DELETE: api/Pacientes/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Eliminar(int id)
        {
            var command = new EliminarPacienteCommand(id);

            try
            {
                await _mediator.Send(command);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent(); // 204: Éxito sin contenido
        }
    }
}