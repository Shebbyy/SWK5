using PersonAdmin.BusinessLogic;
using PersonAdmin.Dal.Ado;
using PersonAdmin.Dal.Interface;
using PersonAdmin.Dal.Simple;

Test(new SimplePersonDao());
Test(new AdoPersonDao());

void Test(IPersonDao personDao) {
    Console.WriteLine(personDao.GetType());
    Console.WriteLine();

    var service = new PersonService(personDao, Console.Out);
    
    service.PrintAll();
}