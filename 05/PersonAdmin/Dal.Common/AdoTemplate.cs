using System.Data;
using System.Data.Common;

namespace Dal.Common;

// alternative func<DbDataReader/IDataRecord, T>
public delegate T RowMapper<T>(IDataRecord reader);

public class AdoTemplate(IConnectionFactory connectionFactory) {
    
    public IEnumerable<T> Query<T>(string statement, RowMapper<T> rowMapper) {
        using DbConnection conn = connectionFactory.CreateConnection();
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