using TechLibrary.Api.Infrastructure.DataAccess;
using TechLibrary.Api.Infrastructure.Security.Cryptography;
using TechLibrary.Api.Infrastructure.Security.Tokens.Access;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exceptions;

namespace TechLibrary.Api.UseCases.Login.DoLogin;

public class DoLoginUseCase
{
    private readonly IConfiguration _configuration;

    public DoLoginUseCase(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ResponseRegisteredUserJson Execute(RequestLoginJson request)
    {
        var dbContext = new TechLibraryDbContext();

        var entity = dbContext.Users.FirstOrDefault(user => user.Email.Equals(request.Email));
        if (entity is null)
        {
            throw new InvalidLoginException("Invalid email or password");
        }

        var cryptography = new BCryptAlgorithm();
        var passwordIsValid = cryptography.Verify(request.Password, entity);
        if (passwordIsValid == false)
            throw new InvalidLoginException("Invalid email or password");

        var tokenGenerator = new JwtTokenGenerator(_configuration);
        
        return new ResponseRegisteredUserJson()
        {
            Name = entity.Name,
            AccessToken = tokenGenerator.Generate(entity)
        };
    }
}