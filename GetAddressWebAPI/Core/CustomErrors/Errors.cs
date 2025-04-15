namespace GetAddressWebAPI.Core.CustomErrors;

public static class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(string? name = null)
        {
            var label = name ?? "value";
            return Error.Validation("value.is.invalid", $"{label} is invalid");
        }

        public static Error ValueIsRequired(string? name = null)
        {
            var label = name == null ? "" : " " + name + " ";
            return Error.Validation("length.is.invalid", $"invalid{label}length");
        }

        public static Error NotFound(string? name = null)
        {
            var label = name == null ? "" : " " + name + " ";
            return Error.Validation("record.not.found", $"record not found{label}");
        }
    }
}
