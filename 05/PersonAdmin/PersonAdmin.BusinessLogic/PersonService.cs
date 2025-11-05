using PersonAdmin.Dal.Interface;

namespace PersonAdmin.BusinessLogic;

public class PersonService(IPersonDao personDao, TextWriter writer) {
    public void PrintAll() {
        writer.WriteLine("All Persons:");
        foreach (var person in personDao.findAll()) {
            writer.WriteLine($"{person.Id, 5}: {person.FirstName, -10} {person.LastName, -15} Age: {person.DateOfBirth, 10:yyyy-MM-dd}");
        }
        Console.WriteLine();
    }

    public void PrintById(int id) {
        writer.WriteLine(personDao.findById(id));
        Console.WriteLine();
    }
}