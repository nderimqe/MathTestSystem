# Math Test System

## Overview
The Math Test System is an ASP.NET Core application that processes XML-based exam data for teachers and students.

It reads XML exam files in the expected structure, evaluates mathematical expressions, grades submitted answers, stores processed results in a database, and displays structured outputs through both a web UI and a REST API.

## Features
- XML upload and processing for teacher exams
- Automatic evaluation of mathematical expressions
- Independent math processor service for arithmetic operations
- Validation of submitted vs calculated results (Correct / Wrong)
- Support for multiple students in one XML file
- Support for multiple exams per student
- Student-wise grouping and score calculation
- Teacher dashboard UI for upload and result visualization
- Teacher history UI for reviewing saved exam results
- Student analytics UI for reviewing exam results by Student ID
- Entity Framework Core integration for database persistence
- SQL Server LocalDB database connection
- REST API for third-party XML processing integration
- Swagger integration for API testing
- Support for both Postman and Swagger clients

## Architecture
The project follows a layered architecture inspired by Clean Architecture principles:

- **Web Layer** - ASP.NET Core MVC controllers, views, and API endpoints
- **Application Layer** - Interfaces, services, DTOs, XML processing, analytics, and math processing
- **Domain Layer** - Core entities such as Teacher, Student, Exam, and TaskItem
- **Infrastructure Layer** - Entity Framework Core DbContext and repository implementation
- **Middleware** - Global exception handling

Dependency Injection (DI) is used to ensure loose coupling and testability.

## Technologies Used
- ASP.NET Core MVC (.NET 8.0)
- C#
- Entity Framework Core
- SQL Server LocalDB
- LINQ to XML (`XDocument`)
- Dependency Injection (Built-in .NET DI)
- Swagger
- REST API

## How to Run the Project
1. Clone the repository
2. Open the solution in Visual Studio
3. Make sure SQL Server LocalDB is available
4. Run the project
5. Upload an XML file through the teacher dashboard
6. Review saved results in the teacher history view
7. Review student results through the student analytics view by entering a Student ID
8. Optional: Test the API via Swagger UI at `/swagger`

## Web UI Pages
### Teacher Dashboard
Used by teachers to upload XML exam files and immediately view processed results.

### Teacher History
Used by teachers to review students, exams, and processed task results saved in the database.

### Student Analytics
Used by students to enter their Student ID and review their exam results, including correct and wrong tasks.

## API Endpoints
The project exposes a REST API for processing XML exam data.

### Upload XML File
`POST /api/exams/upload`

Upload an XML file using `multipart/form-data`.

### Process Raw XML
`POST /api/exams/process`

Send raw XML in the request body using `text/xml`.

## Database
The project uses Entity Framework Core with SQL Server LocalDB.

Default connection string:

```json
"ConnectionStrings": {
  "MathTestDb": "Server=(localdb)\\mssqllocaldb;Database=MathTestSystemDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

## Example XML for Testing
```xml
<Teacher ID="11111">
  <Students>
    <Student ID="12345">
      <Name>Arben Krasniqi</Name>
      <Exam Id="1">
        <Task id="1">2+3/6-4 = 74</Task>
        <Task id="2">6*2+3-4 = 22</Task>
      </Exam>
    </Student>
  </Students>
</Teacher>
```
