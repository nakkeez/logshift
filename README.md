# LogShift

.NET CLI application for tracking work hours for different projects.

# Getting Started

Run the following commands in Package Manager Console:

```
Add-Migration InitialCreate
Update-Database
```

The Add-Migration command scaffolds a migration that will be used in creating needed tables to the SQLite database. The Update-Database command creates the database and applies the created migrations.
