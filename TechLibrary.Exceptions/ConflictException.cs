using System.Net;

namespace TechLibrary.Exceptions;

public class ConflictException : TechLibraryException
{
    public ConflictException(string message) : base(message) {}

    public override List<string> GetErrorMessages() => new List<string>();

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Conflict;
}