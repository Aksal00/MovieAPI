using MovieAPI.Models;
using MovieAPI.Repositories;

namespace MovieAPI.Services
{
    public class MovieService
    {
        private readonly MovieRepository _repo;
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public MovieService(MovieRepository repo, HttpClient http, IConfiguration config)
        {
            _repo = repo;
            _http = http;
            _config = config;
        }

        public async Task<List<Movie>> GetSpiderManMoviesAsync()
        {
            var existing = await _repo.GetAllAsync();
            if (existing.Any())
                return existing;


            var apiKey = _config["OMDb:ApiKey"];
            var url = $"https://www.omdbapi.com/?s=spider-man&apikey={apiKey}";


            var response = await _http.GetFromJsonAsync<OmdbResponse>(url)
            ?? throw new Exception("OMDb API failed");


            foreach (var m in response.Search)
            {
                await _repo.InsertAsync(new Movie
                {
                    ImdbId = m.imdbID,
                    Title = m.Title,
                    Year = m.Year,
                    Type = m.Type,
                    Poster = m.Poster
                });
            }


            return await _repo.GetAllAsync();
        }

        public Task<Movie?> GetByImdbIdAsync(string imdbId)
        {
            return _repo.GetByImdbIdAsync(imdbId);
        }
    }
}
