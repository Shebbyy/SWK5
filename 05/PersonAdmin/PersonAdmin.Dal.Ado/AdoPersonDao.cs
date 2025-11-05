using System.Data;
using Dal.Common;
using PersonAdmin.Dal.Interface;
using PersonAdmin.Domain;

namespace PersonAdmin.Dal.Ado;

public class AdoPersonDao(IConnectionFactory connectionFactory) : IPersonDao {

    private readonly AdoTemplate template = new(connectionFactory);

    private Person RowToPerson(IDataRecord reader) => new(
        (int)reader["Id"],
        (string)reader["first_name"],
        (string)reader["last_name"],
        (DateTime)reader["date_of_birth"]
    );
    
    public IEnumerable<Person> findAll() => template.Query("SELECT * FROM person", RowToPerson);

    public Person? findById(int id) => template.Query("SELECT * FROM person where Id = @id", RowToPerson, new QueryParameter("@id", id)).SingleOrDefault();
    public bool update(Person person) {
        int changedRows = template.Execute("UPDATE person SET first_name = @firstName, last_name = @lastName, date_of_birth = @dateOfBirth WHERE Id = @id", [
            new QueryParameter("@id", person.Id),
            new QueryParameter("@firstName", person.FirstName),
            new QueryParameter("@lastName", person.LastName),
            new QueryParameter("@dateOfBirth", person.DateOfBirth),
        ]);

        return changedRows > 0;
    }
}