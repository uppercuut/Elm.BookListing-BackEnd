using Elm.BookListing.Application.Queries;
using Elm.BookListing.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Elm.BookListing.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly IBookQueries _bookQueries;
        public BooksController(IBookQueries bookQueries)
        {
            _bookQueries = bookQueries;
        }

        [Route("")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Books([FromQuery] string? term = "", [FromQuery] PaginationQueryParameters paginationQueryParameters = default)
        {
            var result = await _bookQueries.GetAllWithoutCover(term, paginationQueryParameters);
            return Ok(result);
        }

        [Route("image/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Image([FromRoute] int id)
        {
            var result = await _bookQueries.GetCoverByBookId(id);
            if (result == null)
                return NoContent();

            return new FileContentResult(System.Convert.FromBase64String(result.Content), result.Format);
        }
    }
}
