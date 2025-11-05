using PersonAdmin.BusinessLogic;
using PersonAdmin.Dal.Interface;
using PersonAdmin.Dal.Simple;

Test(new SimplePersonDao());

void Test(IPersonDao personDao) {
    Console.WriteLine(personDao.GetType());
    Console.WriteLine();

    var service = new PersonService(personDao, Console.Out);
    
    service.PrintAll();
}