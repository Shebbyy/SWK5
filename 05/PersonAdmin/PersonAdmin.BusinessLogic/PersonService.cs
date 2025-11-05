using System.Transactions;
using PersonAdmin.Dal.Interface;
using PersonAdmin.Domain;

namespace PersonAdmin.BusinessLogic;

public class PersonService(IPersonDao personDao, TextWriter writer) {
    public async Task PrintAll() {
        writer.WriteLine("All Persons:");
        foreach (var person in await personDao.findAll()) {
            writer.WriteLine($"{person.Id, 5}: {person.FirstName, -10} {person.LastName, -15} Age: {person.DateOfBirth, 10:yyyy-MM-dd}");
        }
        Console.WriteLine();
    }

    public async Task PrintById(int id) {
        writer.WriteLine((await personDao.findById(id))?.ToString() ?? "<null>");
        Console.WriteLine();
    }

    public async Task<bool> UpdateDateOfBirth(int id, DateTime newDob) {
        var newPerson = await personDao.findById(id);
        if (newPerson is null) {
            throw new InvalidDataException($"Person with ID {id} does not exist!");
        }
        
        newPerson.DateOfBirth = newDob;
        return await personDao.update(newPerson);
    }

    public async Task UpdateMultiplePeople() {
        writer.WriteLine("Updating multiple");

        try {
            // only commits transaction after scope.Complete()
            // Needs special flag enabled when in async method
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled)) {
                await personDao.update(new Person(2, "Test1", "Test2", DateTime.Now));

                //throw new Exception(); // Any Exception would cause inconsistent DB State
                await personDao.update(new Person(3, "Test2", "Test3", DateTime.Now));
                
                scope.Complete();
            }
        }
        catch (Exception e) {
            writer.WriteLine(e);
        }
        
        writer.WriteLine();
    }

    public async Task InsertPersonAsync(Person person) {
        await personDao.InsertAsync(person);
        writer.WriteLine($"Inserted new Person {person}");
    }
}