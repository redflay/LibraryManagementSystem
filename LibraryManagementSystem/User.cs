namespace LibraryManagementSystem
{
    // Book class inherits from LibraryItem and represents a book in the library system
    public class User : LibraryItem
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
        public List<Book> BorrowedBooks { get; private set; }

        public User(string firstName, string lastName, string userId)
        {
            FirstName = firstName;
            LastName = lastName;
            UserId = userId;
            BorrowedBooks = new List<Book>();
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo(); // call the base class method to display the ID
            Console.WriteLine($"First Name: {FirstName}");
            Console.WriteLine($"Last Name: {LastName}");
            Console.WriteLine($"User ID: {UserId}");
            if (BorrowedBooks.Any())
            {
                Console.WriteLine("BorrowedBooks:");
                foreach (var book in BorrowedBooks)
                {
                    Console.WriteLine($"- {book.Title} ({book.ISBN})"); // Display information for each borrowed book
                }
            }
        }
    }
}
