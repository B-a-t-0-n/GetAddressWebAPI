using GetAddressWebAPI.Dto;
using GetAddressWebAPI.Extensions;
using GetAddressWebAPI.Features;
using Microsoft.AspNetCore.Mvc;

namespace GetAddressWebAPI.Controllers;

public class AddressController : ApplicationController
{
    [HttpGet]
    public async Task<ActionResult<AddressDto>> Get(
        [FromQuery] string address,
        [FromServices] GetAddressHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAddressQuery(address);

        var response = await handler.Handle(query, cancellationToken);

        if (response.IsFailure)
            return response.Error.ToResponse();

        return Ok(response.Value);
    }
}

