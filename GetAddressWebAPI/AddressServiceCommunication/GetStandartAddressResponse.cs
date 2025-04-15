using System.Text.Json.Serialization;

namespace GetAddressWebAPI.AddressServiceCommunication;

public record GetStandartAddressResponse(
    string? Source,
    string? Result,
    string? Country,
    string? Region,
    string? City,
    string? CityType,
    string? CityTypeFull,
    string? House,
    string? HouseType,
    string? HouseTypeFull,
    string? Flat,
    string? FlatType,
    string? FlatTypeFull
    );
