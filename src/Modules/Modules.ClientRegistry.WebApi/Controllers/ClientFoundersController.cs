using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modules.ClientRegistry.Application.Commands.ClientCommands.AddClientFounder;
using Modules.ClientRegistry.Application.Commands.ClientCommands.RemoveClientFounder;
using Modules.ClientRegistry.Application.Commands.ClientCommands.UpdateClientFounder;
using Modules.ClientRegistry.Domain.Clients;

namespace Modules.ClientRegistry.WebApi.Controllers;

[Route("api/clients/{clientId}/founders")]
[ApiController]
public class ClientFoundersController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Adds a founder to the specified client.
    /// </summary>
    /// <param name="clientId">Client identifier from the route.</param>
    /// <param name="payload">Founder data from the request body.</param>
    /// <returns>Added new founder response</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/clients/{clientId}/founders
    ///     {
    ///       "founderInn": "123456789012",
    ///       "founderFullName": "John Doe"
    ///     }
    /// </remarks>
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

    /// <summary>
    /// Removes a founder from the specified client.
    /// </summary>
    /// <param name="clientId">Client identifier from the route.</param>
    /// <param name="founderId">Founder identifier from the route.</param>
    /// <returns>Removed founder response</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /api/clients/{clientId}/founders/{founderId}
    /// </remarks>
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

    /// <summary>
    /// Updates founder information for the specified client.
    /// </summary>
    /// <param name="clientId">Client identifier from the route.</param>
    /// <param name="founderId">Founder identifier from the route.</param>
    /// <param name="payload">Updated founder data from the request body.</param>
    /// <returns>Updated client response</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /api/clients/{clientId}/founders/{founderId}
    ///     {
    ///       "founderFullName": "John Smith"
    ///     }
    /// </remarks>
    [HttpPut("{founderId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateFounder(
        ClientId                              clientId,
        FounderId                             founderId,
        [FromBody] UpdateClientFounderPayload payload)
    {
        var command = new UpdateClientFounderCommand
        {
            ClientId        = clientId,
            FounderId       = founderId,
            FounderFullName = payload.FounderFullName
        };

        var result = await sender.Send(command);
        return Ok(result);
    }
}
