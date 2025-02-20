using System.Net;

namespace TechLibrary.Exceptions;

public abstract class TechLibraryException : SystemException
{
    public TechLibraryException(string message) : base(message) {}
    public abstract List<string> GetErrorMessages();
    public abstract HttpStatusCode GetStatusCode();
}