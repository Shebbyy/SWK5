namespace PersonManagement;

public class PersonRepository
{
    private readonly IList<Person> persons = new List<Person>();

    public void AddPerson(Person person)
    {
        persons.Add(person);
    }
    
    public void AddPersons(IEnumerable<Person> persons) {
        // Regular Call CollectionExtensions.AddAll also possible for Extension Methods
        this.persons.AddAll(persons);
    }

    public void PrintPersons(TextWriter textWriter) {
        persons.ForEach(textWriter.WriteLine);
    }

    public IEnumerable<(string?, string?)> GetPersonNames()
    {
        return null;
    }

    public IEnumerable<Person> FindPersonsByCity(string city) {
        return persons.Filter(p => p.City == city);
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
