using System.Data.Common;

namespace Dal.Common;

public interface IConnectionFactory {
    DbConnection CreateConnection();

    public Task<DbConnection> CreateConnectionAsync();
}