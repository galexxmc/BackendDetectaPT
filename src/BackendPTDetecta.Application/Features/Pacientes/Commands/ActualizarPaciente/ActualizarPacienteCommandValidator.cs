using FluentValidation;

namespace BackendPTDetecta.Application.Features.Pacientes.Commands.ActualizarPaciente;

public class ActualizarPacienteCommandValidator : AbstractValidator<ActualizarPacienteCommand>
{
    public ActualizarPacienteCommandValidator()
    {
        RuleFor(v => v.Id)
            .GreaterThan(0).WithMessage("El ID del paciente no es válido.");

        RuleFor(v => v.Nombres)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(100);

        RuleFor(v => v.Apellidos)
            .NotEmpty().WithMessage("El apellido es obligatorio.");

        RuleFor(v => v.Dni)
            .NotEmpty().Length(8).WithMessage("El DNI debe tener 8 dígitos.");
            
        // Usamos la lógica moderna de DateOnly
        RuleFor(v => v.FechaNacimiento)
             .LessThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("La fecha de nacimiento debe ser pasada.");
    }
}