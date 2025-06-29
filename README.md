# Library Management System

This is a console-based Library Management System built in C#. It allows for basic operations such as adding, viewing, searching, borrowing, and returning books, as well as managing users within a simulated library environment. The system demonstrates the use of asynchronous programming (`async`/`await`) for long-running operations and event handling for notifications.

## Features

* **Book Management:**
    * Add new books with details like title, author, publication year, ISBN, and total copies.
    * View all books currently in the library.
    * Search for books by title, author, or ISBN.
    * Remove books from the library.
* **User Management:**
    * Add new library users with first name, last name, and a unique user ID.
    * View all registered users.
* **Borrowing & Returning:**
    * Borrow books by specifying ISBN and User ID.
    * Return borrowed books.
* **Asynchronous Operations:**
    * Simulated long-running operations (e.g., generating reports) use `async`/`await` to keep the console application responsive.
* **Event-Driven Notifications:**
    * Events notify the console when a book is borrowed or returned.

## Technologies Used

* **C#**: The core programming language.
* **.NET**: The framework for building the application.
* **Asynchronous Programming (`async`/`await`)**: For non-blocking I/O operations and responsive UI (console in this case).
* **Event Handling**: For loosely coupled communication within the system.
* **LINQ**: For simplified data querying (e.g., searching books, finding users).

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

* [.NET SDK](https://dotnet.microsoft.com/download) (Version 6.0 or higher is recommended)
* A C# IDE like [Visual Studio](https://visualstudio.microsoft.com/) (Community Edition is free) or [Visual Studio Code](https://code.visualstudio.com/) with the C# extension.

### Installation

1.  **Clone the repository:**
    ```bash
    git clone <your-repository-url>
    cd LibraryManagementSystem
    ```
    (Replace `<your-repository-url>` with the actual URL of your Git repository.)

2.  **Open the project:**
    * If using Visual Studio: Open the `LibraryManagementSystem.sln` file.
    * If using Visual Studio Code: Open the `LibraryManagementSystem` folder.

3.  **Restore dependencies:** (Usually done automatically by the IDE)
    ```bash
    dotnet restore
    ```

## How to Run

1.  **Build the project:**
    ```bash
    dotnet build
    ```

2.  **Run the application:**
    ```bash
    dotnet run
    ```

    The console application will start, and you will see a menu of options to interact with the library system.

## Usage

Follow the on-screen menu prompts to:
* Add books and users.
* View lists of books and users.
* Search for specific books.
* Borrow and return books.
* Generate a library report (which simulates a delay using `async`/`await`).
