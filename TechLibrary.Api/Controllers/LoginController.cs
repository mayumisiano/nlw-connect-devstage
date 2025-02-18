using Microsoft.AspNetCore.Mvc;
using TechLibrary.Api.UseCases.Login.DoLogin;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;

namespace TechLibrary.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status401Unauthorized)]
    public class LoginController : ControllerBase
    {
        private readonly DoLoginUseCase _useCase;

        public LoginController(DoLoginUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPost]
        public IActionResult DoLogin(RequestLoginJson requestLogin)
        {
            var response = _useCase.Execute(requestLogin);
            return Ok(response);
        }
    }
}
