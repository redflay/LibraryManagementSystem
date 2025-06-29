using System;
using System.Linq; // Для Linq запросов
using System.Collections.Generic;
using System.Runtime.CompilerServices; // Для List

namespace LibraryManagementSystem
{
    class Program
    {
        static Library _library = new Library();

        static void Main(string[] args)
        {
            _library.BookBorrowed += OnBookBorrowed;
            _library.BookReturned += OnBookReturned;

            // Adding some initial data for testing
            _library.AddBook(new Book("The Lord of the Rings", "J.R.R. Tolkien", 1954, "978-0618053267", 5));
            _library.AddBook(new Book("1984", "George Orwell", 1949, "978-0451524935", 3));
            _library.AddBook(new Book("To Kill a Mockingbird", "Harper Lee", 1960, "978-0446310789", 2));

            _library.AddUser(new User("Alice", "Smith", "U001"));
            _library.AddUser(new User("Bob", "Johnson", "U002"));

            bool running = true;
            while (running)
            {
                DisplayMenu();
                string? choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": AddBook(); break;
                    case "2": ViewAllBooks(); break;
                    case "3": SearchBooks(); break;
                    case "4": RemoveBook(); break;
                    case "5": AddUser(); break;
                    case "6": ViewAllUsers(); break;
                    case "7": BorrowBook(); break;
                    case "8": ReturnBook(); break;
                    case "9": GenerateReportAsync().Wait(); break; // .Wait() для консоли, в реальном приложении лучше async main или Task.Run
                    case "0": running = false; break;
                    default: Console.WriteLine("Invalid choice. Please try again."); break;
                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }

            Console.WriteLine("Exiting Library Application. Goodbye!");
        }

        static void DisplayMenu()
        {
            Console.WriteLine("--- Library Management System ---");
            Console.WriteLine("1. Add New Book");
            Console.WriteLine("2. View All Books");
            Console.WriteLine("3. Search Books");
            Console.WriteLine("4. Remove Book");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("5. Add New User");
            Console.WriteLine("6. View All Users");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("7. Borrow Book");
            Console.WriteLine("8. Return Book");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("9. Generate Library Report (Async)");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");
        }

        static void AddBook()
        {
            Console.WriteLine("--- Add New Book ---");
            Console.Write("Title: ");
            string title = CheckForEmptyString();
            Console.Write("Author: ");
            string author = CheckForEmptyString();
            Console.Write("Publication Year: ");
            int year;
            while (!int.TryParse(Console.ReadLine(), out year))
            {
                Console.Write("Invalid year. Please enter a number: ");
            }
            Console.Write("ISBN: ");
            string isbn = CheckForEmptyString();
            Console.Write("Total Copies: ");
            int totalCopies;
            while (!int.TryParse(Console.ReadLine(), out totalCopies) || totalCopies <= 0)
            {
                Console.Write("Invalid total copies. Please enter a positive number: ");
            }
            _library.AddBook(new Book(title, author, year, isbn, totalCopies));
        }

        static void ViewAllBooks()
        {
            Console.WriteLine("----------All books in library ----------");
            var books = _library.GetAllBooks();
            if (!books.Any())
            {
                Console.WriteLine("No books in the library");
                return;
            }
            foreach (var book in books)
            {
                book.DisplayInfo();
                Console.WriteLine("--------------------");
            }
        }
        static void SearchBooks()
        {
            Console.WriteLine("--- Search Books ---");
            Console.Write("Enter search query (title, author, or ISBN): ");
            string query = CheckForEmptyString();
            var results = _library.SearchBooks(query);

            if (!results.Any())
            {
                Console.WriteLine($"No books found matching '{query}'.");
                return;
            }

            Console.WriteLine("Search Results:");
            foreach (var book in results)
            {
                book.DisplayInfo();
                Console.WriteLine("--------------------");
            }
        }

        static void RemoveBook()
        {
            Console.WriteLine("--- Remove Book ---");
            Console.Write("Enter ISBN of the book to remove: ");
            string isbn = CheckForEmptyString();
            _library.RemoveBook(isbn);
        }

        static void AddUser()
        {
            Console.WriteLine("--- Add New User ---");
            Console.Write("First Name: ");
            string firstName = CheckForEmptyString();
            Console.Write("Last Name: ");
            string lastName = CheckForEmptyString();
            Console.Write("User ID: ");
            string userId = CheckForEmptyString();

            _library.AddUser(new User(firstName, lastName, userId));
        }

        static void ViewAllUsers()
        {
            Console.WriteLine("--- All Registered Users ---");
            var users = _library.GetAllUsers();
            if (!users.Any())
            {
                Console.WriteLine("No users registered.");
                return;
            }
            foreach (var user in users)
            {
                user.DisplayInfo();
                Console.WriteLine("--------------------");
            }
        }

        static void BorrowBook()
        {
            Console.WriteLine("--- Borrow Book ---");
            Console.Write("Enter ISBN of the book to borrow: ");
            string isbn = CheckForEmptyString();
            Console.Write("Enter User ID: ");
            string userId = CheckForEmptyString();

            _library.BorrowBook(isbn, userId);
        }

        static void ReturnBook()
        {
            Console.WriteLine("--- Return Book ---");
            Console.Write("Enter ISBN of the book to return: ");
            string isbn = CheckForEmptyString();
            Console.Write("Enter User ID: ");
            string userId = CheckForEmptyString();

            _library.ReturnBook(isbn, userId);
        }

        static async Task GenerateReportAsync()
        {
            Console.WriteLine("Generating library report (this might take a moment)...");
            // Имитация длительной операции
            await Task.Delay(2000); // Задержка на 2 секунды

            Console.WriteLine("\n--- Library Report ---");
            Console.WriteLine("Books Available: ");
            var books = _library.GetAllBooks();
            foreach (var book in books)
            {
                Console.WriteLine($"- '{book.Title}' by {book.Author} (Available: {book.AvailableCopies}/{book.TotalCopies})");
            }

            Console.WriteLine("\nUsers and Borrowed Books:");
            var users = _library.GetAllUsers();
            foreach (var user in users)
            {
                Console.WriteLine($"- {user.FirstName} {user.LastName} (ID: {user.UserId})");
                if (user.BorrowedBooks.Any())
                {
                    foreach (var borrowedBook in user.BorrowedBooks)
                    {
                        Console.WriteLine($"  -- Borrowed: {borrowedBook.Title} (ISBN: {borrowedBook.ISBN})");
                    }
                }
                else
                {
                    Console.WriteLine("  -- No books currently borrowed.");
                }
            }
            Console.WriteLine("--- Report End ---");
        }

        // Event handlers for book borrowing and return notifications
        private static void OnBookBorrowed(object sender, BookEventArgs e) 
        {
            Console.WriteLine($"[EVENT] Book '{e.Book.Title}' was borrowed by '{e.User.FirstName}'.");
        }

        private static void OnBookReturned(object sender, BookEventArgs e)
        {
            Console.WriteLine($"[EVENT] Book '{e.Book.Title}' was returned by '{e.User.FirstName}'.");
        }

        public static string CheckForEmptyString()
        {
            string? str;
            str = Console.ReadLine();
            while (string.IsNullOrEmpty(str))
            {
                Console.WriteLine("---You entered an empty string, please try again---");
                str = Console.ReadLine();
            }
            return str;
        }
    }
}