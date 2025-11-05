using Dal.Common;
using PersonAdmin.Dal.Interface;
using PersonAdmin.Domain;

namespace PersonAdmin.Dal.Ado;

public class AdoPersonDao(IConnectionFactory connectionFactory) : IPersonDao {

    private readonly AdoTemplate template = new AdoTemplate(connectionFactory);
    
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