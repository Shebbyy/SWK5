using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace Dal.Common;

// alternative func<DbDataReader/IDataRecord, T>
public delegate T RowMapper<T>(IDataRecord reader);

public class AdoTemplate(string connectionString) {
    public DbConnection GetConnection() {
        DbConnection conn = new SqlConnection();
        conn.ConnectionString = connectionString;
        conn.Open();

        return conn;
    }
    
    public IEnumerable<T> Query<T>(string statement, RowMapper<T> rowMapper) {
        using DbConnection conn = GetConnection();
        using DbCommand command = conn.CreateCommand();
        command.CommandText = statement;

        using DbDataReader reader = command.ExecuteReader();

        var items = new List<T>();

        while (reader.Read()) {
            items.Add(rowMapper(reader));
        }

        return items;
    }
}