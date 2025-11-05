using System.Data.Common;
using System.Runtime.InteropServices;

namespace Dal.Common;

public class ProviderIndependentConnectionFactory : IConnectionFactory
{
    private readonly DbProviderFactory dbProviderFactory;
    private readonly string connectionString;

    public const string MICROSOFT_SQL_CLIENT_PROVIDER_NAME = "Microsoft.Data.SqlClient";
    public const string MYSQL_CLIENT_PROVIDER_NAME = "MySqlConnector";
    public ProviderIndependentConnectionFactory(string providerName, string connectionString)
    {
        DbProviderFactories.RegisterFactory(MICROSOFT_SQL_CLIENT_PROVIDER_NAME, Microsoft.Data.SqlClient.SqlClientFactory.Instance);
        DbProviderFactories.RegisterFactory(MYSQL_CLIENT_PROVIDER_NAME, MySqlConnector.MySqlConnectorFactory.Instance); ;
        // For Oracle, add additional RegisterFactory
        this.dbProviderFactory = DbProviderFactories.GetFactory(providerName);
        this.connectionString = connectionString;
    }

    public DbConnection CreateConnection() {
        var conn = dbProviderFactory.CreateConnection();
        if (conn is null) {
            throw new ExternalException("DB not accessible");
        }
        
        conn.ConnectionString = connectionString;
        conn.Open();
        return conn;
    }

    public async Task<DbConnection> CreateConnectionAsync() {
        var conn = dbProviderFactory.CreateConnection();
        if (conn is null) {
            throw new ExternalException("DB not accessible");
        }
        
        conn.ConnectionString = connectionString;
        await conn.OpenAsync();
        return conn;
    }
}
