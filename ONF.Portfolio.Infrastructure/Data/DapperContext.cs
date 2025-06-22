using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ONF.Portfolio.Infrastructure.Data;

public class DapperContext
{
    private readonly IConfiguration _configuration;
    private const string DefaultConnectionName = "DefaultConnection";
    private readonly string _connectionString;
    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _connectionString = _configuration.GetConnectionString(DefaultConnectionName)
            ?? throw new InvalidOperationException($"Connection string '{DefaultConnectionName}' not found.");
    }

    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}
