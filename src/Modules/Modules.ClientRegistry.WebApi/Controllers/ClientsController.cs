using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modules.ClientRegistry.Application.Commands.ClientCommands.CreateClient;
using Modules.ClientRegistry.Application.Commands.ClientCommands.DeleteClient;
using Modules.ClientRegistry.Application.Commands.ClientCommands.UpdateClient;
using Modules.ClientRegistry.Application.Queries.ClientQueries.GetClientCollection;
using Modules.ClientRegistry.Application.Queries.ClientQueries.GetClientDetailsById;
using Modules.ClientRegistry.Domain.Clients;

namespace Modules.ClientRegistry.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Create a new client
    /// </summary>
    /// <param name="command">Command containing client data to create</param>
    /// <returns>Created client response with client ID and location header</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/clients
    ///     {
    ///       "name": "Acme Corporation",
    ///       "inn": "1234567890",
    ///       "clientType": 1
    ///     }
    ///
    /// </remarks>
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

    /// <summary>
    /// Get client details by client identifier
    /// </summary>
    /// <param name="clientId">Unique identifier of the client</param>
    /// <returns>Client details for the specified client ID</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/clients/{clientId}
    ///
    /// </remarks>
    [HttpGet("{clientId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(ClientId clientId)
    {
        var request = new GetClientDetailsByIdRequest
        {
            ClientId = clientId
        };

        var response = await sender.Send(request);
        return Ok(value: response);
    }

    /// <summary>
    /// Search clients by name with pagination support
    /// </summary>
    /// <param name="searchTerm">Optional search term to filter clients by name</param>
    /// <param name="pageIndex">Page number for pagination (default: 1)</param>
    /// <param name="pageSize">Number of records per page (default: 10)</param>
    /// <returns>Collection of clients matching the search criteria</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/clients?searchTerm=Acme&amp;pageIndex=1&amp;pageSize=10
    ///
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchClientsByName(
        string? searchTerm = null,
        int     pageIndex  = 1,
        int     pageSize   = 10)
    {
        var request = new GetClientCollectionRequest
        {
            SearchTerm = searchTerm,
            PageIndex  = pageIndex,
            PageSize   = pageSize
        };

        var response = await sender.Send(request);
        return Ok(value: response);
    }

    /// <summary>
    /// Update an existing client
    /// </summary>
    /// <param name="command">Command containing updated client data</param>
    /// <returns>Updated client response</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /api/clients
    ///     {
    ///       "clientId": "{clientId}",
    ///       "clientName": "Acme Corporation Updated",
    ///     }
    ///
    /// </remarks>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateClient([FromBody] UpdateClientCommand command)
    {
        var response = await sender.Send(command);
        return Ok(value: response);
    }

    /// <summary>
    /// Delete a client by client identifier
    /// </summary>
    /// <param name="clientId">Unique identifier of the client to delete</param>
    /// <returns>Deletion result for the specified client ID</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /api/clients/{clientId}
    ///
    /// </remarks>
    [HttpDelete("{clientId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteClient(ClientId clientId)
    {
        var command = new DeleteClientCommand
        {
            ClientId = clientId
        };
        var response = await sender.Send(command);
        return Ok(value: response);
    }
}
