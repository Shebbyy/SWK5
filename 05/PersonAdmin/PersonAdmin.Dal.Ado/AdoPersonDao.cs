using System.Data.Common;
using System.Runtime.CompilerServices;
using Dal.Common;
using Microsoft.Data.SqlClient;
using PersonAdmin.Dal.Interface;
using PersonAdmin.Domain;

namespace PersonAdmin.Dal.Ado;

public class AdoPersonDao : IPersonDao {

    private readonly AdoTemplate template = new AdoTemplate(GetConnectionString());

    private static string GetConnectionString() {
        var pwd = Environment.GetEnvironmentVariable("DB_PWD");
        var connectionString = $"Data Source=localhost;Initial Catalog=person_db;User ID=sa;Password={pwd};Trust Server Certificate=True";
        return connectionString;
    }
    
    public IEnumerable<Person> findAll() => template.Query("SELECT * FROM person", reader => new Person(
        (int)reader["Id"],
        (string)reader["first_name"],
        (string)reader["last_name"],
        (DateTime)reader["date_of_birth"]
    ));

    public Person? findById(int id) => template.Query($"SELECT * FROM person where Id = {id}", reader => new Person(
        (int)reader["Id"],
        (string)reader["first_name"],
        (string)reader["last_name"],
        (DateTime)reader["date_of_birth"]
    )).FirstOrDefault();
}