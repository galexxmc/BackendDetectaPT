namespace BackendPTDetecta.Domain.Common
{
    public abstract class EntidadAuditable
    {
        public int Id { get; set; }
        
        // AUDITORÍA
        public string UsuarioRegistro { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } // Quitamos el default UtcNow para que no confunda
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        // ELIMINACIÓN
        public string? UsuarioEliminacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public string? MotivoEliminacion { get; set; }
        
        public int EstadoRegistro { get; set; } = 1;

        public void Eliminar(string usuario, string motivo)
        {
            if (EstadoRegistro == 0) return;

            EstadoRegistro = 0;
            UsuarioEliminacion = usuario;
            MotivoEliminacion = motivo;
            
            // CORRECCIÓN: Usamos la misma lógica segura que el DbContext
            // Esto funciona en Windows, Linux y Mac sin errores.
            FechaEliminacion = DateTime.UtcNow.AddHours(-5); 
        }

        public void Recuperar(string usuario)
        {
            if (EstadoRegistro == 1) return;

            EstadoRegistro = 1;
            UsuarioModificacion = usuario;
            
            // CORRECCIÓN: Aquí también ponemos hora Perú (antes tenías UtcNow directo)
            FechaModificacion = DateTime.UtcNow.AddHours(-5); 
            
            UsuarioEliminacion = null;
            FechaEliminacion = null;
            MotivoEliminacion = null; // Limpiamos el motivo también
        }
    }
}