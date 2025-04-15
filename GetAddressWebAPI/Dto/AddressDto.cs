namespace GetAddressWebAPI.Dto;

public record AddressDto(
    string? Result,
    string? Country,
    string? Region,
    string? City,
    string? House,
    string? Flat);
