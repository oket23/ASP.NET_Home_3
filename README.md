# 📚 Bookstore Management API (EF Core Hybrid Edition)

A structured, educational, and extensible ASP.NET Core Web API designed to manage books and authors.

The project demonstrates essential enterprise practices such as layered architecture, Entity Framework Core integration, DTO-based communication, and a hybrid repository system that switches between In-Memory and SQL Server storage depending on the environment.

## 🚀 Technologies Used

* **.NET 9**
* **ASP.NET Core Web API**
* **Entity Framework Core** (SQL Server)
* **C# 12**
* **Swagger / OpenAPI**
* **Layered Architecture**: API → BLL (Services) → DAL (Repositories)
* **Asynchronous Programming**: `async/await` and `ValueTask` where appropriate

## 📋 Features Overview

This API provides complete CRUD functionality for Books and Authors, including validation rules and environment-based repository selection.

### ⭐ Key Features

#### 1. Hybrid Repository Pattern
The application dynamically selects the data storage approach:

* **Development Mode (Debug):** Uses lightweight **in-memory repositories** (`List<T>`) for fast testing without database setup.
* **Production Mode (Release):** Uses **Entity Framework Core with SQL Server**, enabling migrations, relational mapping, and persistent data.

*Configured in `Program.cs`:*

```csharp
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSingleton<IAuthorsRepository, AuthorsRepository>();
    builder.Services.AddSingleton<IBooksRepository, BooksRepository>();
}
else
{
    builder.Services.AddScoped<IAuthorsRepository, AuthorsDBRepository>();
    builder.Services.AddScoped<IBooksRepository, BooksDBRepository>();
}
```
#### 2. Books Management
* Fetch all books / Fetch a book by ID
* Create, Update, Delete a book
* **Logic includes:**
    * Validating author existence
    * Validating description against banned words
    * Clean separation between Controller → Service → Repository

#### 3. Authors Management
* Full CRUD operations
* Fetching a single author with all associated books (`AuthorWithBooksDTO`)
* Validation of author input

#### 4. Validation Layer
A dedicated `ValidationService` ensures:
* Descriptions do not contain banned words.
* Books reference existing authors.
* Business rules remain isolated from data and controller layers.

## 🧩 Project Structure

```text
Home_3.sln
│
├── Home_3.API        → Controllers, Services implementations, DI, Swagger
├── Home_3.BLL        → DTOs, Models, Interfaces (Services/Repositories)
└── Home_3.DAL        → EF Core DbContext, Repository implementations, Migrations
```
*This structure follows clean layering principles, ensuring maintainability and testability.*

## 🛠 Getting Started

### 1. Clone the repository
```bash
git clone [https://github.com/oket23/ASP.NET_Home_3](https://github.com/oket23/ASP.NET_Home_3)
```
### 2. Navigate to the project
```bash
cd ASP.NET_Home_3/Home_3.API
```
### 3. Install EF Core tools (if needed)
```bash
dotnet tool install --global dotnet-ef
```
## ▶️ Running the Application

### Option A – Development Mode (In-memory storage)
No database required. Data is stored in RAM.

```bash
dotnet run --environment "Development"
```
*Or simply run the app in **Debug** mode through Rider/Visual Studio.*

### Option B – Production Mode (SQL + EF Core)
First, apply migrations to create the database:

```bash
dotnet ef database update --project ../Home_3.DAL --startup-project .
```
Run the API:
```bash
dotnet run --environment "Production"
```
## ⚙️ Configuration (SQL Server)

To run in Production mode, ensure your connection string is set in `Home_3.API/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=Home3DB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```
*You may use LocalDB, SQL Express, or a full SQL Server instance.*

## 🔄 Database Migrations

Since this project uses Entity Framework Core, you need to manage database schemas using migrations.

**Apply existing migrations:**
To create the database and tables for the first time:
```bash
dotnet ef database update --project Home_3.DAL --startup-project Home_3.API
```
**Add a new migration:**
If you modify `Book` or `Author` models, create a new migration:
```bash
dotnet ef migrations add <MigrationName> --project Home_3.DAL --startup-project Home_3.API
```
*Replace `<MigrationName>` with a descriptive name, e.g., `AddBookGenre`.*

Then, apply it to the database:
```bash
dotnet ef database update --project Home_3.DAL --startup-project Home_3.API
```
## 🧪 Testing

### Swagger UI (Development Only)
When running in `Development` mode, Swagger UI is available at:
* `https://localhost:7069/swagger`
* `http://localhost:5213/swagger`

Use this interface to manually test endpoints without writing code.

### Postman (Production)
In `Production` mode, Swagger is disabled by default. Use [Postman](https://www.postman.com/) or `curl` to send requests to the API.

## 📁 Endpoints Overview

### 📚 Books
Base path: `/v1/books`

| Method | Route | Description |
| :--- | :--- | :--- |
| `GET` | `/v1/books` | Get all books (includes Author data) |
| `GET` | `/v1/books/{id}` | Get book by ID |
| `POST` | `/v1/books` | Create a new book |
| `PUT` | `/v1/books/{id}` | Update an existing book |
| `DELETE` | `/v1/books/{id}` | Delete a book |

### ✍️ Authors
Base path: `/v1/authors`

| Method | Route | Description |
| :--- | :--- | :--- |
| `GET` | `/v1/authors` | Get all authors |
| `GET` | `/v1/authors/{id}` | Get author by ID (includes list of their books) |
| `POST` | `/v1/authors` | Create a new author |
| `PUT` | `/v1/authors/{id}` | Update an author |
| `DELETE` | `/v1/authors/{id}` | Delete an author |

## 🎓 Educational Value

This project demonstrates:
* **Clean Architecture**: Core/Domain logic is independent of Data Access.
* **Entity Framework Core**: Code-First migrations, relationships (`Include`), and optimization (`AsNoTracking`).
* **Hybrid Repository Pattern**: Strategy pattern implementation via Dependency Injection to swap between In-Memory and SQL storage.
* **Async Programming**: Proper use of `async/await` and `ValueTask` for performance.
* **Environment Configuration**: Handling `Development` vs `Production` settings.

It is an excellent example for students transitioning from basic ASP.NET exercises to real-world architectural patterns.

## 📜 License

This project is intended for educational use. You may freely modify or extend it for your own learning.
