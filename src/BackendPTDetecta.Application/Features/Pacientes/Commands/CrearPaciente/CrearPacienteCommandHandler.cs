using BackendPTDetecta.Application.Common.Interfaces;
using BackendPTDetecta.Domain.Entities;
using MediatR;

namespace BackendPTDetecta.Application.Features.Pacientes.Commands.CrearPaciente
{
    // IRequestHandler<Entrada, Salida>
    public class CrearPacienteCommandHandler : IRequestHandler<CrearPacienteCommand, int>
    {
        private readonly IApplicationDbContext _context;

        // Inyectamos nuestra interfaz de base de datos (desacoplada)
        public CrearPacienteCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CrearPacienteCommand request, CancellationToken cancellationToken)
        {
            // --- NUEVO: LÓGICA DE FECHA Y HORA PERÚ ---

            // 1. Obtenemos la hora mundial (UTC)
            var utcNow = DateTime.UtcNow;

            // 2. Buscamos la zona horaria de Perú (En Windows se llama "SA Pacific Standard Time")
            // Nota: Esto funciona para Lima, Bogotá y Quito.
            var zonaPeru = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
            var horaPeru = TimeZoneInfo.ConvertTimeFromUtc(utcNow, zonaPeru);

            // 3. Quitamos los milisegundos (Creamos una fecha nueva solo hasta los segundos)
            var fechaRegistroLimpia = new DateTime(
                horaPeru.Year,
                horaPeru.Month,
                horaPeru.Day,
                horaPeru.Hour,
                horaPeru.Minute,
                horaPeru.Second
            );
            // ------------------------------------------

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
                Email = request.Email ?? string.Empty,

                UsuarioRegistro = "SISTEMA",

                // AQUI ASIGNAMOS TU FECHA PERUANA LIMPIA
                FechaRegistro = fechaRegistroLimpia
            };

            // 1. Primer Guardado
            _context.Pacientes.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            // 2. Generar Código PAC-XXXXX
            entity.Codigo = $"PAC-{entity.Id:D5}";

            // 3. Segundo Guardado
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}