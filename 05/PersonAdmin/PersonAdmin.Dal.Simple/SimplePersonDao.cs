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

    public IEnumerable<Person> findAll() {
        return personList;
    }

    public Person findById(int id) {
        return personList.First(p => p.Id == id);
    }
}
