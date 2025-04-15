using CSharpFunctionalExtensions;
using GetAddressWebAPI.Core.CustomErrors;

namespace GetAddressWebAPI.AddressServiceCommunication;

public interface IAddressService
{
    Task<Result<GetStandartAddressResponse, ErrorList>> GetStandartAddress(
        string request,
        CancellationToken cancellationToken);
}
