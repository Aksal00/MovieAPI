using Microsoft.Data.SqlClient;
using MovieAPI.Data;
using MovieAPI.Models;

namespace MovieAPI.Repositories
{
    public class MovieRepository
    {
        private readonly DbConnectionFactory _factory;


        public MovieRepository(DbConnectionFactory factory)
        {
            _factory = factory;
        }


        public async Task<List<Movie>> GetAllAsync()
        {
            var movies = new List<Movie>();


            using var conn = _factory.Create();
            await conn.OpenAsync();


            var cmd = new SqlCommand("SELECT * FROM Movies", conn);
            using var reader = await cmd.ExecuteReaderAsync();


            while (await reader.ReadAsync())
            {
                movies.Add(Map(reader));
            }


            return movies;
        }


        public async Task<Movie?> GetByImdbIdAsync(string imdbId)
        {
            using var conn = _factory.Create();
            await conn.OpenAsync();


            var cmd = new SqlCommand(
            "SELECT * FROM Movies WHERE ImdbId = @ImdbId", conn);
            cmd.Parameters.AddWithValue("@ImdbId", imdbId);


            using var reader = await cmd.ExecuteReaderAsync();
            return await reader.ReadAsync() ? Map(reader) : null;
        }


        public async Task InsertAsync(Movie movie)
        {
            using var conn = _factory.Create();
            await conn.OpenAsync();


            var cmd = new SqlCommand(
            @"INSERT INTO Movies (ImdbId, Title, Year, Type, Poster)
VALUES (@ImdbId, @Title, @Year, @Type, @Poster)", conn);


            cmd.Parameters.AddWithValue("@ImdbId", movie.ImdbId);
            cmd.Parameters.AddWithValue("@Title", movie.Title);
            cmd.Parameters.AddWithValue("@Year", movie.Year ?? "");
            cmd.Parameters.AddWithValue("@Type", movie.Type ?? "");
            cmd.Parameters.AddWithValue("@Poster", movie.Poster ?? "");


            await cmd.ExecuteNonQueryAsync();
        }


        private Movie Map(SqlDataReader r) => new()
        {
            Id = r.GetInt32(0),
            ImdbId = r.GetString(1),
            Title = r.GetString(2),
            Year = r.GetString(3),
            Type = r.GetString(4),
            Poster = r.GetString(5)
        };
    }
}
