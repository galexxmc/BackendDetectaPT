using BackendPTDetecta.Domain.Common;

namespace BackendPTDetecta.Domain.Entities
{
    public class Paciente : EntidadAuditable
    {
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string HistoriaClinica { get; set; } = string.Empty;
        public DateOnly FechaNacimiento { get; set; }
        public int SexoId { get; set; }
        public Maestro? Sexo { get; set; } // Propiedad de navegación
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Edad
        {
            get
            {
                var hoy = DateOnly.FromDateTime(DateTime.UtcNow);
                var edad = hoy.Year - FechaNacimiento.Year;
                if (hoy < FechaNacimiento.AddYears(edad))
                {
                    edad--;
                }
                return edad;
            }
        }
        public int TipoSeguroId { get; set; }
        public Maestro? TipoSeguro { get; set; } // Propiedad de navegación
    }
}