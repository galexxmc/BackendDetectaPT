namespace BackendPTDetecta.Application.Features.Pacientes.Queries.GetPacientesList;

public class PacienteDto
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty; // Ej: PAC-00001
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Dni { get; set; } = string.Empty;
    public DateOnly FechaNacimiento { get; set; } // ¡Usamos el nuevo DateOnly!
    public int Edad { get; set; } // Calculado
    public string Sexo { get; set; } = string.Empty; // Devolveremos "Masculino" en vez de "1"
    public string? Telefono { get; set; }
    public string? Email { get; set; }
}