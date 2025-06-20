# Piano Events Information System 🎹

This is a command-line information system application developed in C# using a JDBC-style layered architecture. It manages piano-related events such as lessons, concerts, and competitions.

## 💻 Technologies
- C# (.NET CLI application)
- PostgreSQL database
- Npgsql (.NET PostgreSQL driver)
- No Entity Framework – JDBC-style architecture (manual SQL queries, DAO, DTO, etc.)

## 📦 Architecture
The application is built using a JDBC-style layered architecture:
- `Model/DTO` – data representation objects
- `DAO` – database access layer
- `Service` – business logic layer
- `UIHandler` – command-line interface (CLI)

## 📊 Features
- in progress...
## 🛠️ How to Run
1. Run PostgreSQL and import the DDL + DML scripts
2. Configure connection parameters in the code (e.g. connection string)
3. Build and run the app via terminal:
dotnet build
dotnet run
