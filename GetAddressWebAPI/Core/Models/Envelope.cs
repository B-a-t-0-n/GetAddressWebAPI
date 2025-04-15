using GetAddressWebAPI.Core.CustomErrors;

namespace GetAddressWebAPI.Core.Models;

public class Envelope
{
    private Envelope(object? rezult, ErrorList? errors)
    {
        Rezult = rezult;
        Errors = errors;
        TimeGenerated = DateTime.Now;
    }

    public object? Rezult { get; }

    public ErrorList? Errors { get; }

    public DateTime TimeGenerated { get; }

    public static Envelope Ok(object? rezult = null) =>
        new(rezult, null);

    public static Envelope Error(ErrorList errors) =>
        new(null, errors);

}
