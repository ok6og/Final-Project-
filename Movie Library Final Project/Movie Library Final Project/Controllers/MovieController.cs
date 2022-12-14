using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Models.Mediatr.MovieCommands;
using MovieLibrary.Models.Requests.MovieRequests;

namespace Movie_Library_Final_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovieController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("Get All Movies")]
        public async Task<IActionResult> GetAllMovies()
        {
            var result = await _mediator.Send(new GetAllMoviesCommand());
            return StatusCode((int)result.StatusCode, new { result.Value, result.Message });
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("Get a Movie")]
        public async Task<IActionResult> GetMovie(int movieId)
        {
            var result = await _mediator.Send(new GetMovieByIdCommand(movieId));
            return StatusCode((int)result.StatusCode, new { result.Value, result.Message });
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("Create A Movie")]
        public async Task<IActionResult> CreateMovie([FromBody] AddMovieRequest movie)
        {
            var result = await _mediator.Send(new AddMovieCommand(movie));
            return StatusCode((int)result.StatusCode, new { result.Value, result.Message });
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("Update A Movie")]
        public async Task<IActionResult> UpdateMovie([FromBody] UpdateMovieRequest movie)
        {
            var result = await _mediator.Send(new UpdateMovieCommand(movie));
            return StatusCode((int)result.StatusCode, new { result.Value, result.Message });
        }
        [HttpDelete("Delete a Movie")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int movieId)
        {
            var result = await _mediator.Send(new DeleteMovieCommand(movieId));
            return StatusCode((int)result.StatusCode, new { result.Value, result.Message });
        }
    }
}