using BackendPTDetecta.Application.Common.Interfaces;
using BackendPTDetecta.Domain.Entities;
using MediatR;

namespace BackendPTDetecta.Application.Features.Pacientes.Commands.CrearPaciente
{
    public class CrearPacienteCommandHandler : IRequestHandler<CrearPacienteCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CrearPacienteCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CrearPacienteCommand request, CancellationToken cancellationToken)
        {
            // 1. Preparamos la entidad (Sin preocuparnos por fechas, el DbContext lo hará)
            var entity = new Paciente
            {
                Nombres = request.Nombres,
                Apellidos = request.Apellidos,
                Dni = request.Dni,
                FechaNacimiento = request.FechaNacimiento,
                Sexo = (BackendPTDetecta.Domain.Enums.Sexo)request.SexoId,
                
                Codigo = "GENERANDO...", // Temporal
                
                Direccion = request.Direccion ?? string.Empty,
                Telefono = request.Telefono ?? string.Empty,
                Email = request.Email ?? string.Empty
            };

            // 2. PRIMER GUARDADO: Aquí obtiene su ID y se llenan FechaRegistro/UsuarioRegistro
            await _context.Pacientes.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken); 

            // 3. GENERAR CÓDIGO: Usamos el ID recién creado
            entity.Codigo = $"PAC-{entity.Id:D5}";

            // 4. SEGUNDO GUARDADO: Aquí actualizamos solo el código.
            // Normalmente esto activaría "FechaModificacion", pero la trampa lo evitará.
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}