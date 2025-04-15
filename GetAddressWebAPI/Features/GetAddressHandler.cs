using AutoMapper;
using CSharpFunctionalExtensions;
using GetAddressWebAPI.AddressServiceCommunication;
using GetAddressWebAPI.Core.CustomErrors;
using GetAddressWebAPI.Dto;

namespace GetAddressWebAPI.Features;

public class GetAddressHandler
{
    private readonly ILogger<GetAddressHandler> _logger;
    private readonly IAddressService _addressService;
    private readonly IMapper _mapper;

    public GetAddressHandler(
        ILogger<GetAddressHandler> logger, 
        IAddressService addressService,
        IMapper mapper)
    {
        _logger = logger;
        _addressService = addressService;
        _mapper = mapper;
    }

    public async Task<Result<AddressDto, ErrorList>> Handle(GetAddressQuery query, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(query.Address))
        {
            return Errors.General.ValueIsInvalid("address").ToErrorList();
        }

        var standartAddressResult = await _addressService.GetStandartAddress(query.Address, cancellationToken);
        if (standartAddressResult.IsFailure)
        {
            return standartAddressResult.Error;
        }

        var addressDto = _mapper.Map<AddressDto>(standartAddressResult.Value);

        _logger.LogInformation("Address: {address}", standartAddressResult.Value.Result);

        return addressDto;
    }


}
