using PersonAdmin.Dal.Interface;
using PersonAdmin.Domain;

namespace PersonAdmin.Dal.Simple;

public class SimplePersonDao : IPersonDao
{
    private static IList<Person> personList = new List<Person>
    {
        new Person(1, "John", "Doe", DateTime.Now.AddYears(-10)),
        new Person(2, "Jane", "Doe", DateTime.Now.AddYears(-20)),
        new Person(3, "Max", "Mustermann", DateTime.Now.AddYears(-30))
    };

    public Task<IEnumerable<Person>> findAll() {
        return Task.FromResult<IEnumerable<Person>>(personList);
    }

    public Task<Person?> findById(int id) {
        return Task.FromResult(personList.FirstOrDefault(p => p.Id == id));
    }

    public Task<bool> update(Person person) {
        var currentPerson = personList.SingleOrDefault(p => p.Id == person.Id);
        if (currentPerson is null) {
            return Task.FromResult(false);
        }
        
        currentPerson.FirstName = person.FirstName;
        currentPerson.LastName = person.LastName;
        currentPerson.DateOfBirth = person.DateOfBirth;

        return Task.FromResult(true);
    }

    public Task InsertAsync(Person person) {
        person.Id = personList.Count > 0 ? personList.Max(p => p.Id) + 1 : 1;

        return Task.CompletedTask;
    }
}
