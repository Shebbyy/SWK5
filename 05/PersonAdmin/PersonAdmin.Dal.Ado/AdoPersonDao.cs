using System.Data.Common;
using Microsoft.Data.SqlClient;
using PersonAdmin.Dal.Interface;
using PersonAdmin.Domain;

namespace PersonAdmin.Dal.Ado;

public class AdoPersonDao : IPersonDao {
    public IEnumerable<Person> findAll() {
        var pwd = Environment.GetEnvironmentVariable("DB_PWD");
        var connectionString = $"Data Source=localhost;Initial Catalog=person_db;User ID=sa;Password={pwd};Trust Server Certificate=True";
        
        using DbConnection conn = new SqlConnection();
        conn.ConnectionString = connectionString;
        conn.Open();

        using DbCommand command = conn.CreateCommand();
        command.CommandText = "SELECT * FROM person";

        using DbDataReader reader = command.ExecuteReader();

        var l = new List<Person>();
        // yield rather bad, as if not all results are iterated upon, while is never finished, therefore never finishing the method and closing the db conn
        while (reader.Read()) {
            l.Add(new Person(
                (int)reader["Id"], 
                (string)reader["first_name"], 
                (string)reader["last_name"], 
                (DateTime)reader["date_of_birth"]));
        }

        return l;
    }

    public Person findById(int id) {
        throw new NotImplementedException();
    }
}