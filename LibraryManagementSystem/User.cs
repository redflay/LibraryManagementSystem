namespace LibraryManagementSystem
{
    // Book class inherits from LibraryItem and represents a book in the library system
    public class User : LibraryItem
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
        public List<Book> BorrowedBooks { get; private set; } // List to hold borrowed books

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

        public static bool operator ==(User? user1, User? user2) // operator overload == for simplified user comparison
        {
            if (ReferenceEquals(user1, null)) return ReferenceEquals(user2, null); // if both are null, they are equal
            return user1.Equals(user2);
        }

        public static bool operator !=(User? user1, User? user2)
        {
            return !(user1 == user2);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            User other = (User)obj;
            return UserId == other.UserId; // Compare based on UserId
        }

        /// <summary>
        /// Computes a hash code for the current User instance, based on its userId.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return UserId.GetHashCode();
        }
    }
}
