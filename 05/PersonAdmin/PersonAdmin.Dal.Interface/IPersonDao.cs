using PersonAdmin.Domain;

namespace PersonAdmin.Dal.Interface;

public interface IPersonDao {
    Task<IEnumerable<Person>> findAll();
    Task<Person?> findById(int id);
    Task<bool> update(Person person);

    Task InsertAsync(Person person);
}