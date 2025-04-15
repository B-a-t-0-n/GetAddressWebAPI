using CSharpFunctionalExtensions;
using System.Collections;

namespace GetAddressWebAPI.Core.CustomErrors;

public class ErrorList : IEnumerable<Error>
{
    private readonly List<Error> _errors;

    public ErrorList(IEnumerable<Error> errors)
    {
        _errors = [..errors];
    }

    public IEnumerator<Error> GetEnumerator()
    {
        return _errors.GetEnumerator();
    }

    public Result<Guid, ErrorList> ToErrorList()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static implicit operator ErrorList(Error error)
    {
        return new ([error]);
    }

    public static implicit operator ErrorList(List<Error> errors)
    {
        return new(errors);
    }
}
