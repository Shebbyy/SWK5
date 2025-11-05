using System.Data;
using System.Data.Common;

namespace Dal.Common;

// alternative func<DbDataReader/IDataRecord, T>
public delegate T RowMapper<T>(IDataRecord reader);

public class AdoTemplate(IConnectionFactory connectionFactory) {
    
    public IEnumerable<T> Query<T>(string statement, RowMapper<T> rowMapper, params QueryParameter[] parameters) {
        using DbConnection conn = connectionFactory.CreateConnection();
        using DbCommand command = conn.CreateCommand();
        command.CommandText = statement;
        AddParameters(command, parameters);

        using DbDataReader reader = command.ExecuteReader();

        var items = new List<T>();

        while (reader.Read()) {
            items.Add(rowMapper(reader));
        }

        return items;
    }

    private void AddParameters(DbCommand command, QueryParameter[] queryParams) {
        foreach (var queryParam in queryParams) {
            DbParameter dbParam = command.CreateParameter();
            dbParam.ParameterName = queryParam.Name;
            dbParam.Value = queryParam.Value;
            command.Parameters.Add(dbParam);
        }
    }
    
    public int Execute(string statement, params QueryParameter[] parameters) {
        using DbConnection conn = connectionFactory.CreateConnection();
        using DbCommand command = conn.CreateCommand();
        command.CommandText = statement;
        AddParameters(command, parameters);

        return command.ExecuteNonQuery();
    }
}