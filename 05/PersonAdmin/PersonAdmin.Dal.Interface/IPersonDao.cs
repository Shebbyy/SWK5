using PersonAdmin.Domain;

namespace PersonAdmin.Dal.Interface;

public interface IPersonDao {
    IEnumerable<Person> findAll();
    Person findById(int id);
    
}