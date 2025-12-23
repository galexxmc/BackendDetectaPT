namespace BackendPTDetecta.Application.Features.Pacientes.Queries.GetPacienteById;

public class PacienteDetalleDto
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Dni { get; set; } = string.Empty;
    public DateOnly FechaNacimiento { get; set; }
    public int Edad { get; set; }
    public string Sexo { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public string? Direccion { get; set; }
    public string NombreSeguro { get; set; } = string.Empty;
}