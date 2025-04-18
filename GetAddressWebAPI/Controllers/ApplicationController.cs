using GetAddressWebAPI.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace GetAddressWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ApplicationController : ControllerBase
{
    public override OkObjectResult Ok(object? value)
    {
        var envelope = Envelope.Ok(value);

        return base.Ok(envelope);
    }
}
