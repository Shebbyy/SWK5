// enable for file:
#nullable enable
using System.Diagnostics.CodeAnalysis;

// enable for project in project settings
// It is just for the compiler, during runtime it is still possible to set null to a string, which would  throw an error during compilation

var person = new Person("Huber", "Franz");
// every reference datatype can be null, NullableReferenceTypes  has the goal to fix this in hindsight, very common error, can break old programs, therefore must be activated explicitly
person.FirstName = null;
person.LastName = null;
person.LastName = "Huber-Mayr";

var firstUpper = person.FirstName?.ToUpper();
var lastUpper = person.LastName.ToUpper();

IEnumerable<Person>? persons = GetPersons();
PrintPersons(persons ?? []);

if (TryGetPerson(persons, "Huber", out Person? p))
{
    Console.WriteLine(p.LastName);
}

static IEnumerable<Person>? GetPersons()
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

static bool TryGetPerson(IEnumerable<Person>? persons, string lastName, [NotNullWhen(true)]out Person? person)
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

public class Person(string lastName, string? firstName = null)
{
    public string? FirstName { get; set; } = firstName; // without nullablereference, can be null, with it active it needs to be set explicitly with string?

    public string LastName { get; set; } = lastName ?? throw new ArgumentNullException(nameof(lastName));
}