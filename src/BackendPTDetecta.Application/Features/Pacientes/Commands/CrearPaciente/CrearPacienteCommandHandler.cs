using BackendPTDetecta.Application.Common.Interfaces;
using BackendPTDetecta.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore; // 👈 IMPORTANTE: Necesario para usar .FirstOrDefaultAsync

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
            // =================================================================================
            // 1. LOGICA DE HISTORIA CLÍNICA (HC-AAAA-NNNNN)
            // =================================================================================
            
            // a. Definimos el prefijo del año actual, ej: "HC-2025-"
            // Nota: Si quieres año Perú exacto, usa .AddHours(-5).Year
            var anioActual = DateTime.UtcNow.Year; 
            var prefijoHc = $"HC-{anioActual}-";

            // b. Buscamos el último código que empiece con ese prefijo en la BD
            var ultimoHc = await _context.Pacientes
                .Where(p => p.HistoriaClinica.StartsWith(prefijoHc))
                .OrderByDescending(p => p.HistoriaClinica)
                .Select(p => p.HistoriaClinica) // Solo traemos el string para ser más rápidos
                .FirstOrDefaultAsync(cancellationToken);

            // c. Calculamos el nuevo número
            int nuevoCorrelativo = 1;

            if (ultimoHc != null)
            {
                // Ejemplo ultimoHc: "HC-2025-00003"
                // Split separa por guiones: ["HC", "2025", "00003"]
                // Last() agarra "00003"
                var parteNumerica = ultimoHc.Split('-').Last();
                
                if (int.TryParse(parteNumerica, out int numeroAnterior))
                {
                    nuevoCorrelativo = numeroAnterior + 1;
                }
            }

            // d. Generamos el código final, ej: "HC-2025-00001"
            string codigoHistoriaGenerado = $"{prefijoHc}{nuevoCorrelativo:D5}";


            // =================================================================================
            // 2. CREACIÓN DE LA ENTIDAD
            // =================================================================================
            var entity = new Paciente
            {
                Nombres = request.Nombres,
                Apellidos = request.Apellidos,
                Dni = request.Dni,
                FechaNacimiento = request.FechaNacimiento,
                SexoId = request.SexoId,
                
                // Código temporal del paciente (se arregla abajo)
                Codigo = "GENERANDO...", 
                
                // 🔥 ASIGNAMOS LA HISTORIA CLÍNICA CALCULADA
                HistoriaClinica = codigoHistoriaGenerado,

                Direccion = request.Direccion ?? string.Empty,
                Telefono = request.Telefono ?? string.Empty,
                Email = request.Email ?? string.Empty,
                TipoSeguroId = request.IdTipoSeguro
            };

            // 3. PRIMER GUARDADO (Obtiene ID y guarda HC)
            await _context.Pacientes.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken); 

            // 4. GENERAR CÓDIGO PACIENTE (PAC-XXXXX) BASADO EN ID
            // Tu lógica original se mantiene aquí
            entity.Codigo = $"PAC-{entity.Id:D5}";

            // 5. SEGUNDO GUARDADO (Actualiza solo el PAC-XXXXX)
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}