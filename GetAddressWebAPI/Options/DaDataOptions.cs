namespace GetAddressWebAPI.Options;

public class DaDataOptions
{
    public const string DaData = "DaData";
    public const string DaDataClient = "DaDataClient";

    public string Endpoint { get; init; } = string.Empty;
    public string AccessKey { get; init; } = string.Empty;
    public string SecretKey { get; init; } = string.Empty;
}
