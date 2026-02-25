## Kata: Legacy Employee Export

You have been given a CSV export from an older system. It is *almost* what we want, but not quite.

Your job is to read the legacy file, convert each row into an `Employee` object, and then write a new cleaned CSV file.

### Input file

You will receive a file named:

`legacy-employees.csv`

It uses **semicolon (`;`)** as the separator and has this structure:

```txt
FullName;BirthYear;Department
Anna Smith;1998;IT
John Doe;2001;Sales
Clive Brown;1987;Management
```

### Output file

Create a new file named:

`employees-clean.csv`

This file must use **comma (`,`)** as the separator and have these columns (in this order):

```txt
FirstName,LastName,Age,Department
...
```

### Employee model

Create an `Employee` class with these properties:

* `FirstName` (string)
* `LastName` (string)
* `Age` (int)
* `Department` (string)

### Requirements

1. **Read the legacy file manually**

   * Use `StreamReader`
   * Read the file line-by-line
   * Skip the header row
   * Split each data row using `;`

2. **Convert each row into an `Employee`**

   * `FullName` contains **two parts**: first name + last name
   * `BirthYear` should be used to calculate `Age`
   * `Department` maps directly

3. **Write the cleaned file using CsvHelper**

   * Use `CsvWriter`
   * Write all employees to `employees-clean.csv`
   * Use `WriteRecords(...)` (or equivalent) so the library handles the rows

### Age calculation

Use the current year from `DateTime.Now.Year`.

Age is:

`Age = currentYear - birthYear`

### Notes

* Assume the input file is well-formed (no missing columns).
* Keep the manual part focused on reading + mapping.
* The “clean” part should rely on CsvHelper for writing.

### Done when

* Your program reads all employees from `legacy-employees.csv`
* Produces `employees-clean.csv`
* The output file has the correct header and values
* Each row is correctly transformed into `FirstName,LastName,Age,Department`
