namespace BackendPTDetecta.Application.Common.Interfaces;

public interface ICurrentUserService
{
    // Solo necesitamos obtener el código (ej: "gmonje")
    string? CodigoUsuario { get; }
}