using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modules.ClientRegistry.Application.Commands.ClientCommands.CreateClient;
using Modules.ClientRegistry.Domain.Clients;

namespace Modules.ClientRegistry.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Create new Client
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateClient([FromBody] CreateClientCommand command)
    {
        var response = await sender.Send(command);
        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { clientId = response.ClientId },
            value: response
        );
    }

    [HttpGet("{clientId}")]
    public async Task<IActionResult> GetById(ClientId clientId)
    {
        throw new NotImplementedException();
    }
}
