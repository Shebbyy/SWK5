using System.Data;
using System.Data.Common;

namespace Dal.Common;

// alternative func<DbDataReader/IDataRecord, T>
public delegate T RowMapper<T>(IDataRecord reader);

public class AdoTemplate(IConnectionFactory connectionFactory) {
    
    public async Task<IEnumerable<T>> QueryAsync<T>(string statement, RowMapper<T> rowMapper, params QueryParameter[] parameters) {
        using DbConnection conn = await connectionFactory.CreateConnectionAsync();
        using DbCommand command = conn.CreateCommand();
        command.CommandText = statement;
        AddParameters(command, parameters);

        using DbDataReader reader = await command.ExecuteReaderAsync();

        var items = new List<T>();

        while (await reader.ReadAsync()) {
            items.Add(rowMapper(reader));
        }

        return items;
    }

    public async Task<T?> QuerySingleAsync<T>(string statement, RowMapper<T> rowMapper,
        params QueryParameter[] parameters) {
        using DbConnection conn = await connectionFactory.CreateConnectionAsync();
        using DbCommand command = conn.CreateCommand();
        command.CommandText = statement;
        AddParameters(command, parameters);

        var reader = await command.ExecuteReaderAsync();
        await reader.ReadAsync();
        return rowMapper(reader);
    }

    private void AddParameters(DbCommand command, QueryParameter[] queryParams) {
        foreach (var queryParam in queryParams) {
            DbParameter dbParam = command.CreateParameter();
            dbParam.ParameterName = queryParam.Name;
            dbParam.Value = queryParam.Value;
            command.Parameters.Add(dbParam);
        }
    }
    
    public async Task<int> ExecuteAsync(string statement, params QueryParameter[] parameters) {
        using DbConnection conn = await connectionFactory.CreateConnectionAsync();
        using DbCommand command = conn.CreateCommand();
        command.CommandText = statement;
        AddParameters(command, parameters);

        return await command.ExecuteNonQueryAsync();
    }
    
    public async Task<T> ExecuteScalarAsync<T>(string statement, params QueryParameter[] parameters) {
        using DbConnection conn = await connectionFactory.CreateConnectionAsync();
        using DbCommand command = conn.CreateCommand();
        command.CommandText = statement;
        AddParameters(command, parameters);

        object result = await command.ExecuteScalarAsync() ?? throw new ArgumentException("SQL Statement is not valid!");

        return (T)Convert.ChangeType(result, typeof(T));
    }
}