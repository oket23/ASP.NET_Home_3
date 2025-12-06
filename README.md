# Bookstore Management API

A REST API server built with ASP.NET Core for managing a bookstore's inventory and authors. This project implements CRUD operations, data validation, and relationships between entities.

## 🚀 Technologies

* **Platform**: .NET 9
* **Framework**: ASP.NET Core Web API
* **Language**: C#
* **Architecture**: N-Layer Architecture (Controller-Service-Repository)
* **Dependency Injection**: Native DI Container
* **Documentation**: Swagger UI

## 📋 Features

The API provides functionality to work with `Book` and `Author` entities. Data is stored in **In-Memory** storage (RAM), utilizing Repositories with Lists.

### Key Capabilities:
* **Books Management**: Create, Read, Update, and Delete books.
* **Authors Management**: Create, Read, Update, and Delete authors.
* **Relationships**: When retrieving an author by ID, the API also returns a list of all books written by them.
* **Validation Logic**:
    * **Censorship**: Book descriptions are validated to ensure they do not contain banned words (e.g., "the", "no").
    * **Integrity**: When creating a book, the system verifies that the specified `AuthorId` exists.
* **Architecture**: Clean separation of concerns using DTOs, Services, and Repositories.

## 🛠 Getting Started

1.  **Clone the repository:**
    ```bash
    git clone <your-repo-url>
    ```
2.  **Navigate to the project directory:**
    ```bash
    cd Home_2
    ```
3.  **Run the project:**
    ```bash
    dotnet run
    ```
4.  **Open Swagger Documentation:**
    After starting the application, navigate to the following URL in your browser:
    `http://localhost:5000/swagger` or `https://localhost:5001/swagger`

## 🔌 API Endpoints

### 📚 Books
Base path: `/v1/books`

#### 1. Get All Books
Retrieves a list of all available books.
* **URL**: `GET /v1/books`

#### 2. Get Book by ID
* **URL**: `GET /v1/books/{id}`
* **Response 200**: Book object.
* **Response 404**: If the book is not found.

#### 3. Create Book
Adds a new book to the store. Validates that the `authorId` exists and the `description` does not contain banned words.
* **URL**: `POST /v1/books`
* **Body (JSON)**:
    ```json
    {
      "name": "The Great Gatsby",
      "publicationYear": 1925,
      "authorId": 1,
      "description": "A story about the Jazz Age."
    }
    ```

#### 4. Update Book
Updates an existing book. Supports partial updates (nullable fields).
* **URL**: `PUT /v1/books/{id}`
* **Body (JSON)**:
    ```json
    {
      "id": 1,
      "name": "Updated Title",
      "description": "New valid description"
    }
    ```

#### 5. Delete Book
* **URL**: `DELETE /v1/books/{id}`

---

### ✍️ Authors
Base path: `/v1/authors`

#### 1. Get All Authors
Retrieves a list of all authors.
* **URL**: `GET /v1/authors`

#### 2. Get Author by ID (With Books)
Retrieves detailed information about an author, including a list of all books associated with them.
* **URL**: `GET /v1/authors/{id}`
* **Response Example**:
    ```json
    {
      "id": 1,
      "firstName": "F. Scott",
      "lastName": "Fitzgerald",
      "birthdayDate": "1896-09-24T00:00:00",
      "books": [
        {
          "id": 1,
          "name": "The Great Gatsby",
          "publicationYear": 1925
        }
      ]
    }
    ```

#### 3. Create Author
* **URL**: `POST /v1/authors`
* **Body (JSON)**:
    ```json
    {
      "firstName": "Stephen",
      "lastName": "King",
      "birthdayDate": "1947-09-21T00:00:00"
    }
    ```

#### 4. Update Author
* **URL**: `PUT /v1/authors/{id}`
* **Body (JSON)**:
    ```json
    {
      "id": 1,
      "firstName": "Stephen",
      "lastName": "Edwin King"
    }
    ```

#### 5. Delete Author
* **URL**: `DELETE /v1/authors/{id}`
