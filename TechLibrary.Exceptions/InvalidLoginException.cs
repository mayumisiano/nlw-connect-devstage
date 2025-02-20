using System.Net;

namespace TechLibrary.Exceptions;

public class InvalidLoginException : TechLibraryException
{
    public InvalidLoginException(string message) : base("Invalid Email and/or password") {}
    public override List<string> GetErrorMessages() => new List<string> {};

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}