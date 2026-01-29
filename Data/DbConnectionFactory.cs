using Microsoft.Data.SqlClient;

namespace MovieAPI.Data
{
    public class DbConnectionFactory
    {
        private readonly IConfiguration _config;


        public DbConnectionFactory(IConfiguration config)
        {
            _config = config;
        }


        public SqlConnection Create()
        {
            return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        }
    }
}
