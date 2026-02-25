using CsvHelper;
using CsvHelper.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        WriteCsv();
        ReadCsv();
    }

    private static void WriteCsv()
    {
        // ----------------------------------------
        // WRITING CSV USING CSVHELPER (Library)
        // ----------------------------------------

        // Path where the file will be written.
        // BaseDirectory = where the application is currently running (bin/Debug/netX.X/)
        string path = Path.Combine(AppContext.BaseDirectory, "test2.csv");

        // Some sample data
        var people = new List<Person>
        {
            new Person() { Name = "Bob", YearBorn = 1992, Role = "Employee"},
            new Person() { Name = "Alice", YearBorn = 1892, Role = "Sales Rep"},
            new Person() { Name = "Clive", YearBorn = 2008, Role = "CEO"}
        };

        // StreamWriter = writes text to a file
        using var writer = new StreamWriter(path);

        // CsvWriter = wraps the StreamWriter and handles CSV formatting
        // It uses reflection to map properties to columns
        using var csv = new CsvWriter(writer);

        // Writes header automatically based on property names,
        // then writes each record as a row
        csv.WriteRecords(people);
    }

    private static void ReadCsv()
    {
        // ----------------------------------------
        // READING CSV USING CSVHELPER (Library)
        // ----------------------------------------

        string path = Path.Combine(AppContext.BaseDirectory, "test.csv");

        // Reader reads raw text from file
        using var reader = new StreamReader(path);

        // CsvReader interprets the text as CSV data
        using var csv = new CsvReader(reader);

        // Converts each CSV row into a Person object.
        // Property names must match the CSV header names.
        // This works using reflection.
        var people = csv.GetRecords<Person>();

        // Force execution (GetRecords is lazy)
        people.ToList().ForEach(Console.WriteLine);
    }

    private static void ReadManualCsv()
    {
        // ----------------------------------------
        // READING CSV MANUALLY (No Library)
        // ----------------------------------------

        var people = new List<Person>();

        string file = "test.csv";
        string path = Path.Combine(AppContext.BaseDirectory, file);

        using var reader = new StreamReader(path);

        // First line is the header row (we ignore it)
        string? headerRow = reader.ReadLine();

        string? line;

        // Read file line-by-line until no more lines
        while ((line = reader.ReadLine()) != null)
        {
            // Split the line into values using comma delimiter
            var entries = line.Split(',');

            // Convert text values into proper types
            Person person = new Person(
                entries[0],
                int.Parse(entries[1]),
                entries[2]
            );

            people.Add(person);
        }

        // Print just the name to show it worked
        people.ForEach(p => Console.WriteLine($"{p.Name}"));
    }

    private static void WriteManualCsv()
    {
        // ----------------------------------------
        // WRITING CSV MANUALLY (No Library)
        // ----------------------------------------

        var people = new List<Person>()
        {
            new Person() { Name = "Bob", YearBorn = 1992, Role = "Employee"},
            new Person() { Name = "Alice", YearBorn = 1892, Role = "Sales Rep"},
            new Person() { Name = "Clive", YearBorn = 2008, Role = "CEO"}
        };

        string file = "test.csv";
        string path = Path.Combine(AppContext.BaseDirectory, file);

        using var writer = new StreamWriter(path);

        // Write header row manually
        writer.WriteLine("Name,YearBorn,Role");

        // Write each record manually.
        // Here we rely on ToString() formatting.
        people.ForEach(p => writer.WriteLine(p));
    }
}

internal class Person
{
    // Parameterless constructor required by CsvHelper
    // It creates an empty object first, then sets properties.
    public Person()
    {
    }

    public Person(string name, int yearBorn, string role)
    {
        Name = name;
        YearBorn = yearBorn;
        Role = role;
    }

    // Public properties become CSV columns
    public string Name { get; set; }
    public int YearBorn { get; set; }
    public string Role { get; set; }

    // Used when writing manually with writer.WriteLine(p)
    public override string ToString()
    {
        // return $"{Name},{YearBorn},{Role}"; // Manual way
        return $"Name = {Name}, Year = {YearBorn},Role = {Role}"; // Better formatting when using CSV Helper
    }
}