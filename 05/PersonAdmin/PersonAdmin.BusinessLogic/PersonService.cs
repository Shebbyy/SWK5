using System.Transactions;
using PersonAdmin.Dal.Interface;
using PersonAdmin.Domain;

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

    public bool UpdateDateOfBirth(int id, DateTime newDob) {
        var newPerson = personDao.findById(id);
        if (newPerson is null) {
            throw new InvalidDataException($"Person with ID {id} does not exist!");
        }
        
        newPerson.DateOfBirth = newDob;
        return personDao.update(newPerson);
    }

    public void UpdateMultiplePeople() {
        writer.WriteLine("Updating multiple");

        try {
            // only commits transaction after scope.Complete()
            using (TransactionScope scope = new TransactionScope()) {
                personDao.update(new Person(2, "Test1", "Test2", DateTime.Now));

                //throw new Exception(); // Any Exception would cause inconsistent DB State
                personDao.update(new Person(3, "Test2", "Test3", DateTime.Now));
                
                scope.Complete();
            }
        }
        catch (Exception e) {
            writer.WriteLine(e);
        }
        
        writer.WriteLine();
    }
}