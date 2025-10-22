namespace PersonManagement;

public class PersonRepository
{
    private readonly IList<Person> persons = new List<Person>();

    public void AddPerson(Person person)
    {

    }

    public void AddPersons(IEnumerable<Person> persons)
    {

    }

    public void PrintPersons(TextWriter textWriter)
    {

    }

    public IEnumerable<(string?, string?)> GetPersonNames()
    {
        return null;
    }

    public IEnumerable<Person> FindPersonsByCity(string city)
    {
        return null;
    }

    public Person FindYoungestPerson()
    {
        return null;
    }


    public IEnumerable<Person> FindPersonsSortedByAgeAscending()
    {
        return null;
    }
}
