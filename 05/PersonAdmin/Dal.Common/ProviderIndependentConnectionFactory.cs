using System.Data.Common;

namespace Dal.Common;

public class ProviderIndependentConnectionFactory
{
    private readonly DbProviderFactory dbProviderFactory;
    private readonly string connectionString;

    public ProviderIndependentConnectionFactory(string providerName, string connectionString)
    {
        DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", Microsoft.Data.SqlClient.SqlClientFactory.Instance);
        DbProviderFactories.RegisterFactory("MySqlConnector", MySqlConnector.MySqlConnectorFactory.Instance); ;

        this.dbProviderFactory = DbProviderFactories.GetFactory(providerName);
        this.connectionString = connectionString;
    }
}
