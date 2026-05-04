using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modules.ClientRegistry.Application.Commands.ClientFounderCommands.AddClientFounder;
using Modules.ClientRegistry.Application.Commands.ClientFounderCommands.RemoveClientFounder;
using Modules.ClientRegistry.Domain.Clients;

namespace Modules.ClientRegistry.WebApi.Controllers;

[Route("api/clients/{clientId}/founders")]
[ApiController]
public class ClientFoundersController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddFounder(
        ClientId                           clientId,
        [FromBody] AddClientFounderPayload payload)
    {
        var command = new AddClientFounderCommand
        {
            ClientId        = clientId,
            FounderInn      = payload.FounderInn,
            FounderFullName = payload.FounderFullName
        };

        var result = await sender.Send(command);
        return Ok(result);
    }

    [HttpDelete("{founderId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveFounder(ClientId clientId, FounderId founderId)
    {
        var command = new RemoveClientFounderCommand
        {
            ClientId  = clientId,
            FounderId = founderId
        };

        var result = await sender.Send(command);
        return Ok(result);
    }
}
