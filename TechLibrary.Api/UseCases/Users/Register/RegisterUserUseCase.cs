using TechLibrary.Api.Domain.Entities;
using TechLibrary.Api.Infrastructure;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exceptions;

namespace TechLibrary.Api.UseCases.Users.Register;

public class RegisterUserUseCase
{
    public ResponseRegisteredUserJson Execute(RequestUserJson request)
    {
        Validate(request);

        var entity = new User
        {
            Email = request.Email,
            Name = request.Name,
            Password = request.Password
        };

        var dbContext = new TechLibraryDbContext();

        dbContext.Users.Add(entity);
        dbContext.SaveChanges();
        
        return new ResponseRegisteredUserJson
        {
            Name = request.Name,
        };
    }

    private void Validate(RequestUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}