using BackendPTDetecta.Domain.Common;

namespace BackendPTDetecta.Domain.Entities;

public class Maestro : EntidadAuditable
{
    // Define el tipo (Ej: "SEGURO", "SEXO")
    public string Grupo { get; set; } = string.Empty; 
    
    // Lo que ve el usuario (Ej: "SIS", "Masculino")
    public string Nombre { get; set; } = string.Empty;
}