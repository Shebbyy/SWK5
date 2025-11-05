using Dal.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.Configuration;
using PersonAdmin.BusinessLogic;
using PersonAdmin.Dal.Ado;
using PersonAdmin.Dal.Interface;
using PersonAdmin.Dal.Simple;

const string APP_SETTINGS_DB_CONNECTION = "PersonDbConnection";

Test(new SimplePersonDao());
Test(new AdoPersonDao(new ProviderIndependentConnectionFactory(ProviderIndependentConnectionFactory.MICROSOFT_SQL_CLIENT_PROVIDER_NAME, GetConnectionString())));

void Test(IPersonDao personDao) {
    Console.WriteLine(personDao.GetType());
    Console.WriteLine();

    var service = new PersonService(personDao, Console.Out);
    
    service.PrintAll();
    
    Console.WriteLine("Person with ID 1:");
    service.PrintById(1);

    service.UpdateDateOfBirth(1, DateTime.ParseExact("15/01/2003", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));
    service.PrintById(1);
    
    service.UpdateMultiplePeople();
    
    service.PrintAll();
}

string GetConnectionString() {
    IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
    var connectionString = config.GetConnectionString(APP_SETTINGS_DB_CONNECTION);
    if (connectionString is null) {
        throw new InvalidConfigurationException($"App Settings is missing Db-Connection String {APP_SETTINGS_DB_CONNECTION}");
    }

    var dbPwdEnvVar = "DB_PWD";
    var pwd = Environment.GetEnvironmentVariable(dbPwdEnvVar);
    if (pwd is null) {
        throw new InvalidConfigurationException($"Env Variable {dbPwdEnvVar} is not set");
    }

    return connectionString.Replace("TODO_PWD", pwd);
}