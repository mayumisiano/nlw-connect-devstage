using System.Net;

namespace TechLibrary.Exceptions;

public class InvalidLoginException : TechLibraryException
{
    public override List<string> GetErrorMessages() => new List<string> { "Invalid Email and/or password" };

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}