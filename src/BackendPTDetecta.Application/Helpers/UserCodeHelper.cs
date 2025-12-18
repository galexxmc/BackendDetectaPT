namespace BackendPTDetecta.Application.Common.Helpers;

public static class UserCodeHelper
{
    public static string GenerarCodigo(string nombres, string apellidos)
    {
        if (string.IsNullOrWhiteSpace(nombres) || string.IsNullOrWhiteSpace(apellidos))
            return "anonimo";

        // 1. Obtener la primera letra del nombre (Ej: "Gherson" -> "G")
        char primeraLetraNombre = nombres.Trim()[0];

        // 2. Obtener el primer apellido (Ej: "Monje Castro" -> "Monje")
        string primerApellido = apellidos.Trim().Split(' ')[0];

        // 3. Juntar y poner en minúsculas (Ej: "gmonje")
        string codigo = $"{primeraLetraNombre}{primerApellido}".ToLower();

        return codigo;
    }
}