using PersonAdmin.Dal.Simple;

namespace PersonAdmin.BusinessLogic.Tests;

[TestClass]
public class PersonServiceTests {
    [TestMethod]
    public async Task PrintPerson_WithValidId_PrintsPerson() {
        var writer = new StringWriter();

        var service = new PersonService(new SimplePersonDao(), writer);

        await service.PrintById(1);

        var output = writer.ToString();
        
        Assert.IsTrue(output.Contains("Doe"));
    }
    
    [TestMethod]
    public async Task PrintPerson_WithInvalidId_PrintsPerson() {
        var writer = new StringWriter();

        var service = new PersonService(new SimplePersonDao(), writer);

        await service.PrintById(500);

        var output = writer.ToString();
        Assert.IsTrue(output.StartsWith("<null>"));
    }
}