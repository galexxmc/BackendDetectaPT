using Microsoft.AspNetCore.Mvc;
using BackendPTDetecta.Application.Features.Usuarios.Commands.RegistrarUsuario;
using BackendPTDetecta.Application.Features.Usuarios.Commands.LoginUsuario;
using BackendPTDetecta.Application.Features.Usuarios.Commands.OlvideContrasena;
using BackendPTDetecta.Application.Features.Usuarios.Commands.ResetearContrasena;
using Microsoft.AspNetCore.Authorization;

namespace BackendPTDetecta.API.Controllers;

public class UsuariosController : ApiControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<string>> Registrar(RegistrarUsuarioCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginResponseDto>> Login(LoginUsuarioCommand command)
    {
        try 
        {
            return await Mediator.Send(command);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Correo o contraseña incorrectos.");
        }
    }

    [HttpPost("forgot-password")]
    [AllowAnonymous]
    public async Task<ActionResult<string>> OlvideContrasena(OlvideContrasenaCommand command)
    {
        try
        {
            var token = await Mediator.Send(command);
            // Devolvemos el token para que puedas copiarlo y usarlo en el siguiente paso
            return Ok(new { Message = "Copia este token para resetear tu contraseña", Token = token });
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Correo no encontrado.");
        }
    }
    [HttpPost("reset-password")]
    [AllowAnonymous]
    public async Task<ActionResult<string>> ResetearContrasena(ResetearContrasenaCommand command)
    {
        try
        {
            var mensaje = await Mediator.Send(command);
            return Ok(mensaje);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}