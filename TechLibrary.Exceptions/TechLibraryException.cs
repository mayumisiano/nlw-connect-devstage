using System.Net;

namespace TechLibrary.Exceptions;

public abstract class TechLibraryException : SystemException
{
    public abstract List<string> GetErrorMessages();
    public abstract HttpStatusCode GetStatusCode();
}