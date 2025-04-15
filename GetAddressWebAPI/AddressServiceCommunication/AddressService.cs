using CSharpFunctionalExtensions;
using GetAddressWebAPI.Core.CustomErrors;
using GetAddressWebAPI.Options;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GetAddressWebAPI.AddressServiceCommunication;

public class AddressService : IAddressService
{
    private readonly HttpClient _httpClient;

    public AddressService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(DaDataOptions.DaDataClient);
    }

    public async Task<Result<GetStandartAddressResponse, ErrorList>> GetStandartAddress(string request, CancellationToken cancellationToken)
    {
        var requestBody = new[] { request };

        var response = await _httpClient.PostAsJsonAsync(
            "clean/address",
            requestBody,
            cancellationToken);

        if (response.StatusCode != HttpStatusCode.OK)
        {
            return Error.Failure("failure.get.address", "failure to ger address").ToErrorList();
        }
            
        var fileResponse = await response.Content
        .ReadFromJsonAsync<GetStandartAddressResponse[]>(
            options: new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            },
            cancellationToken: cancellationToken);

        return fileResponse!.First();
    }
}
