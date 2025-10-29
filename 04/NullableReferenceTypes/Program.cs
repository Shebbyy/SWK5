var person = new Person("Huber", "Franz");
person.FirstName = null;
person.LastName = null;
person.LastName = "Huber-Mayr";

var firstUpper = person.FirstName.ToUpper();
var lastUpper = person.LastName.ToUpper();

IEnumerable<Person> persons = GetPersons();
PrintPersons(persons);

if (TryGetPerson(persons, "Huber", out Person p))
{
    Console.WriteLine(p.LastName);
}

static IEnumerable<Person> GetPersons()
{
    // returns null for whatever reason
    return null;
}

static void PrintPersons(IEnumerable<Person> persons)
{
    foreach (var p in persons)
    {
        Console.WriteLine(p);
    }
}

static bool TryGetPerson(IEnumerable<Person> persons, string lastName, out Person person)
{
    if (persons is not null)
    {
        foreach (var p in persons)
        {
            if (p.LastName == lastName)
            {
                person = p;
                return true;
            }
        }
    }

    person = null;
    return false;
}

public class Person(string lastName, string firstName = null)
{
    public string FirstName { get; set; } = firstName;

    public string LastName { get; set; } = lastName ?? throw new ArgumentNullException(nameof(lastName));
}