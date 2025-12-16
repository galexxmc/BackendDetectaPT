using BackendPTDetecta.Domain.Common;
using BackendPTDetecta.Domain.Enums;

namespace BackendPTDetecta.Domain.Entities
{
    public class Paciente : EntidadAuditable
    {
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public Sexo Sexo { get; set; }
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Edad
        {
            get
            {
                var hoy = DateTime.UtcNow;
                var edad = hoy.Year - FechaNacimiento.Year;
                if (hoy.Date < FechaNacimiento.Date.AddYears(edad)) edad--;
                return edad;
            }
        }
    }
}