using Microsoft.AspNetCore.Mvc;
using TechLibrary.Api.UseCases.Users.Register;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;

namespace TechLibrary.Api.Controllers

{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly RegisterUserUseCase _useCase;

        public UserController(RegisterUserUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status400BadRequest)]
        public IActionResult Register(RequestUserJson request)
        {
            var response = _useCase.Execute(request);
            return Created(string.Empty, response);
        }
    }
}