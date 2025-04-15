using AutoMapper;
using GetAddressWebAPI.AddressServiceCommunication;
using GetAddressWebAPI.Dto;

namespace GetAddressWebAPI;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<GetStandartAddressResponse, AddressDto>();
    }
}
