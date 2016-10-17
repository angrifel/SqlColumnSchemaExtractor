# SQL Column Schema Extractor

A Tool to extract column schema information from SQL server.

## Description

Executes a SQL query against a database an produces schema information
of the columns in csv format.

## Features

* Outputs all the data from SqlDataReader.GetColumnSchema() in csv format.
* All result sets are enumerated, all properties from DbColumn are extracted. 
* It prepends the output with a ResultSetIndex column indicate to 
which result set the DbColumn propeties information belongs to.
* All queries are rolled back. Executions are wrapped in a transaction
with `READ UNCOMMITTED` isolation level that is rolled back after all 
the schema information is extracted.

## Requirements

* .NET Core 1.0 (CLI tools mut be installed in the path).
  * Go to https://dot.net/ to get .NET Core

``` 
Usage: dotnet sqlcolumnschemaextractor [options]
Options:
  -ConnectionString 
  [mandatory] The connection string to connect to the database.
  
  -SqlStatement
  [mandatory] The statement from which column schema information is extracted.
  
  -OutputFile
  [optional]  The outputFile, if none is provided the output will be written 
              to the stdout.
  
  -UseSchemeOnlyCommandBehavior
  [optional]  This will cause the query to only return the schema and
              not produce data.
  
  -Verbose
  [optional]  Write detailed information.
```
