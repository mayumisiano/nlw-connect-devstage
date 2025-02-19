using Microsoft.AspNetCore.Mvc;
using TechLibrary.Api.UseCases.Book.Filter;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;

namespace TechLibrary.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly FilterBookUseCase _useCase;

        public BooksController(FilterBookUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet("filter")]
        [ProducesResponseType(typeof(ResponseBooksJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Filter(int pageNumber, string? title)
        {
            var request = new RequestFilterBooksJson
            {
                PageNumber = pageNumber,
                Title = title
            };
            
            var result = _useCase.Execute(request);
            if (result.Books.Count > 0)
                return Ok(result);

            return NoContent();
        }
    }
}

