using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BackendPTDetecta.API.Controllers;

[ApiController]
[Route("api/[controller]")] // Esto define la ruta base automática
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    // Esta propiedad mágica busca el Mediator si no existe
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}