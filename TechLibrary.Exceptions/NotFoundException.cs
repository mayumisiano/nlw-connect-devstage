using System.Net;

namespace TechLibrary.Exceptions;

public class NotFoundException : TechLibraryException
{
    public NotFoundException(string message) : base(message) {}

    public override List<string> GetErrorMessages() => new List<string>();

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
}