using LinqSamples.Data;

var repository = new CustomerRepository();
var customers = repository.GetCustomers();

// is lazy requesting, eg. only requests when iterated over
// no caching, so if iterated twice, will evaluate all conditions again
var customersStartWithA =
    from   c in customers
    where  c.Name.StartsWith("a", StringComparison.CurrentCultureIgnoreCase)
    select c.Name;

Print("Customers starting with a", customersStartWithA);

// Var necessary due to anonymous result object
var customerByRevenue =
    from c in customers
    where c.Revenue > 1_000_000
    orderby c.Revenue descending 
    select new {
        c.Name,
        c.Revenue
    };

Print("Customers sorted by revenue", customerByRevenue);

customerByRevenue = customers
    .Where(c => c.Revenue > 1_000_000)
    .OrderByDescending(c => c.Revenue)
    .Select(c => new {
        c.Name,
        c.Revenue
    });

customerByRevenue = Enumerable.Select(
    Enumerable.OrderByDescending(
        Enumerable.Where(customers, c => c.Revenue > 1_000_000),
        c => c.Revenue
    ),
    c => new {
        c.Name,
        c.Revenue
    }
);

var largestCustomer =
    (from c in customers
        orderby c.Revenue descending
        select c
    ).Take(1);

Print("Largest customer (revenue)", largestCustomer);

var aCustomers = customers
    .Where(c => c.Rating == Rating.A);

if (aCustomers.Any()) {
    var revenueAvgCustomers = aCustomers
        .Where(c => c.Rating == Rating.A)
        .Average(c => c.Revenue); // beware of null division in case of where returning 0 results

    Console.WriteLine($"Customer with the rating A average revenue: {revenueAvgCustomers}");
}
else {
    Console.WriteLine("No Customers with Rating A found");
}

String name = "pixope";
var first = customers.FirstOrDefault(c => c.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
Console.WriteLine(first.ToString() ?? $"Name {name} nicht gefunden");

var customersPerCountry =
    from c in customers
    group c by c.Location.Country
    into countryGroup
    select new {
        Country = countryGroup.Key,
        Customers = (IEnumerable<Customer>) countryGroup,
        AverageRevenue = countryGroup.Average(c => c.Revenue)
    };

foreach (var group in customersPerCountry.OrderBy(c => c.Country)) {
    Console.WriteLine(group.Country);
    Console.WriteLine($"Average Revenue in Country: {Decimal.Round(group.AverageRevenue, 2)}");

    foreach (var customer in group.Customers) {
        Console.WriteLine(customer);
    }
}

void Print<T>(string title, IEnumerable<T> items)
{
    Console.WriteLine($"{title}:");
    Console.WriteLine();
    
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }    

    Console.WriteLine();
    Console.WriteLine();
}

var students = new List<Student> {
    new ("s12345", "Huber",   "Se", new [] { 2, 3, 2, 1, 3 }),
    new ("s12388", "Mayr",    "MC", new [] { 1, 2, 3, 2, 1 }),
    new ("s12321", "Bauer",   "se", new [] { 3, 5, 5, 2, 3 }),
    new ("s12353", "Schmidt", "SE", new [] { 2, 4, 3, 2, 1 }),
};

Console.WriteLine("Students SE");
var seStudents =
    from s in students
    where s.Subject.Equals("se", StringComparison.CurrentCultureIgnoreCase)
    select new { s.MatNr, s.Name };

foreach (var student in seStudents) {
    Console.WriteLine(student);
}

Console.WriteLine("Students SE 2");
seStudents = students
    .Where(s => s.Subject.Equals("Se", StringComparison.CurrentCultureIgnoreCase))
    .Select(s => new {s.MatNr, s.Name});

foreach (var student in seStudents) {
    Console.WriteLine(student);
}

Console.WriteLine("Grades 3");
var studentsWithOnlyGradesAtLeast3 = students
    .Where(s => s.Grades.All(g => g <= 3));
    
foreach (var student in studentsWithOnlyGradesAtLeast3) {
    Console.WriteLine(student);
}

var avgGradesSortedAscending = students
    .Select(s => new {
        Name = s.Name,
        AvgGrade = s.Grades.Average()
    })
    .OrderBy(s => s.AvgGrade);
    
Console.WriteLine("AvgGrades");
foreach (var student in avgGradesSortedAscending) {
    Console.WriteLine(student);
}

var gradeStrings = new [] { "Sehr gut", "Gut", "Befriedigend", "Genügend", "Nicht genügend" };

Console.WriteLine("GradesMapped");
var studentGrades = students
    .Where(s => s.MatNr.Equals("s12321", StringComparison.CurrentCultureIgnoreCase))
    .Select(s => s.Grades.Select(g => gradeStrings[g - 1]).ToList()).First();

foreach (var grades in studentGrades) {
Console.WriteLine(grades);
}


record Student (string MatNr, string Name, string Subject, int[] Grades);
