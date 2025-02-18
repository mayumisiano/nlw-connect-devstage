namespace TechLibrary.Api.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public String Name { get; set; } = string.Empty;
    public String Email { get; set; } = string.Empty;
    public String Password { get; set; } = string.Empty;
}