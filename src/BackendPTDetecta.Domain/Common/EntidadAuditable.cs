namespace BackendPTDetecta.Domain.Common
{
    public abstract class EntidadAuditable
    {
        public int Id { get; set; }
        public string UsuarioRegistro { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioEliminacion { get; private set; }
        public DateTime? FechaEliminacion { get; private set; }
        public int EstadoRegistro { get; private set; } = 1;
        public void Eliminar(string usuario)
        {
            if (EstadoRegistro == 0) return;
            EstadoRegistro = 0;
            UsuarioEliminacion = usuario;
            FechaEliminacion = DateTime.UtcNow;
        }
        public void Recuperar(string usuario)
        {
            if (EstadoRegistro == 1) return;
            EstadoRegistro = 1;
            UsuarioModificacion = usuario;
            FechaModificacion = DateTime.UtcNow;
            UsuarioEliminacion = null;
            FechaEliminacion = null;
        }
    }
}