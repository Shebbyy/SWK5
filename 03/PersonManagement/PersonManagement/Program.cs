using System.Text.Json;
using PersonManagement;

// Project Settings: CopyToOutputDirectory to add files to build dir, like persons.json

PersonRepository personRepository = new PersonRepository();
IEnumerable<Person>? persons = new List<Person>();

try
{
	// ReadAll good for small files, stream better for large files
	using (var reader = new StreamReader("persons.json")) {
		string json = reader.ReadToEnd();

		// List of Persons inside, so deserialize into that;
		// JsonSerializerOptions allows more variance, up to full converter logic
		persons = JsonSerializer.Deserialize<IEnumerable<Person>>(json, new JsonSerializerOptions {
			PropertyNameCaseInsensitive = true
		});
		
	} // Calls reader.Dispose() at the end;
}
catch (FileNotFoundException fnfEx)
{
	Console.WriteLine(fnfEx.Message);
}
// outside of try so it is for sure not possible to be null
if (persons is null) {
	throw new InvalidDataException("Persons.json does not contain a list of Person Objects");
}

personRepository.AddPersons(persons);

var textWriter = Console.Out; 

textWriter.WriteLine("=====================================================");
textWriter.WriteLine("Person list");
textWriter.WriteLine("=====================================================");

personRepository.PrintPersons(textWriter);

textWriter.WriteLine();
textWriter.WriteLine("=====================================================");
textWriter.WriteLine("Persons in Hagenberg");
textWriter.WriteLine("=====================================================");

personRepository.FindPersonsByCity("Hagenberg").ForEach(p => textWriter.WriteLine(p));


//textWriter.WriteLine();
//textWriter.WriteLine("=====================================================");
//textWriter.WriteLine("Person names");
//textWriter.WriteLine("=====================================================");
//
// TODO
//

//textWriter.WriteLine();
//textWriter.WriteLine("=====================================================");
//textWriter.WriteLine($"Youngest person");
//textWriter.WriteLine("=====================================================");
//
// TODO
//

//textWriter.WriteLine();
//textWriter.WriteLine("=====================================================");
//textWriter.WriteLine("Persons sorted by age ascending");
//textWriter.WriteLine("=====================================================");
//
// TODO
//
