# Lesson: Working with CSV Files in C#

## Overview

Today we worked with CSV files in two ways:

1. Manual file processing
2. Using the CsvHelper library

We wrote and read the same data using both approaches.

The focus was on how the data moves between a text file and C# objects.

## What Is a CSV File?

A CSV file is plain text.

Example:

```
Name,YearBorn,Role
Bob,1992,Employee
Alice,1892,Sales Rep
Clive,2008,CEO
```

Each line represents a record.
Values are separated by commas.
The first line usually contains headers.

There is no special structure beyond that.
It is just text formatted in a consistent way.

## Part 1 – Writing and Reading CSV Manually

We started by treating the CSV file as text.

### Writing the file

We:

* Opened a file using `StreamWriter`
* Wrote the header row
* Constructed each record line manually
* Ensured commas were placed correctly

Each row had to match the structure of the header.

Because we were building the lines ourselves, we had to think about:

* Formatting
* Data types
* Ordering
* Consistency

### Reading the file

We then read the file using:

* `StreamReader`
* A loop that processes each line
* `Split(',')` to separate values
* Manual conversion from string to the correct type
* Explicit creation of `Person` objects

Every step was visible.

If something was formatted differently than expected, the parsing would fail.

### Observations

Manual parsing works when:

* The structure is predictable
* The data is clean
* The formatting is consistent

It also means we are responsible for:

* Splitting correctly
* Converting safely
* Handling unexpected input

All of that logic lives in our code.

## Part 2 – Using CsvHelper

Next, we introduced CsvHelper.

Instead of manually splitting and converting values, we:

* Created a `CsvReader`
* Told it what type we want (`Person`)
* Let it read records directly into objects

Requirements:

* A `Person` class
* Property names that match the CSV headers
* A parameterless constructor

The library:

* Reads each row
* Matches headers to property names
* Converts values to the correct types
* Instantiates objects

The structure of the file stays the same.
The way we interact with it changes.

## Why the Parameterless Constructor Is Needed

When reading a row, CsvHelper:

1. Creates an empty instance of `Person`
2. Assigns values to its properties

To create the object, it needs a constructor with no parameters.

Without it, the library cannot instantiate the class.

## Comparing the Two Approaches

| Manual Processing            | CsvHelper                     |
| ---------------------------- | ----------------------------- |
| Explicit parsing logic       | Parsing handled by library    |
| Manual type conversion       | Automatic type conversion     |
| Full visibility of mechanics | Reduced implementation detail |
| More setup code              | Less setup code               |

Both approaches read the same file.

The difference is where the parsing logic lives:

* In our code
* Or inside the library
