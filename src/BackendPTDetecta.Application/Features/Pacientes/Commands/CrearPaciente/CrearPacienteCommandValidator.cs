using FluentValidation;

namespace BackendPTDetecta.Application.Features.Pacientes.Commands.CrearPaciente
{
    public class CrearPacienteCommandValidator : AbstractValidator<CrearPacienteCommand>
    {
        public CrearPacienteCommandValidator()
        {
            RuleFor(v => v.Nombres)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres.");

            RuleFor(v => v.Apellidos)
                .NotEmpty().WithMessage("El apellido es obligatorio.");

            RuleFor(v => v.Dni)
                .NotEmpty().WithMessage("El DNI es obligatorio.")
                .Length(8).WithMessage("El DNI debe tener exactamente 8 caracteres.")
                .Matches("^[0-9]*$").WithMessage("El DNI solo debe contener números.");

            RuleFor(v => v.FechaNacimiento)
                .NotEmpty().WithMessage("La fecha de nacimiento es obligatoria.")
                .LessThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("La fecha de nacimiento debe ser en el pasado.");

            RuleFor(v => v.Email)
                .EmailAddress().WithMessage("El formato del correo no es válido.")
                .When(v => !string.IsNullOrEmpty(v.Email)); // Solo valida si enviaron email
        }
    }
}