namespace GetAddressWebAPI.Core.CustomErrors;

public record ResponseError(string? ErrorCode, string? ErrorMessage, string? InvalidField);
