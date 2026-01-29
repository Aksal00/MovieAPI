using Microsoft.AspNetCore.Mvc;
using MovieAPI.Services;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieService _service;

        public MoviesController(MovieService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var movies = await _service.GetSpiderManMoviesAsync();
            return Ok(movies);
        }

        [HttpGet("{imdbId}")]
        public async Task<IActionResult> GetById(string imdbId)
        {
            var movie = await _service.GetByImdbIdAsync(imdbId);
            return movie is null ? NotFound() : Ok(movie);
        }
    }
}
