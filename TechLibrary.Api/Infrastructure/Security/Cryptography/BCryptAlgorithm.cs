using TechLibrary.Api.Domain.Entities;

namespace TechLibrary.Api.Infrastructure.Security.Cryptography;

using BCrypt.Net;

public class BCryptAlgorithm
{
    public string HashPassword(string password) => BCrypt.HashPassword(password);

    public bool Verify(string password, User user) => BCrypt.Verify(password, user.Password);
}