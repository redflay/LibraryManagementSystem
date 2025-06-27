namespace LibraryManagementSystem
{
    // the main class for all library elements that can display information
    public abstract class LibraryItem
    {
        public Guid Id { get; protected set; } // protected set so that it can be set in child classes or constructor

        public LibraryItem() // generate a unique ID upon creation
        {
            Id = Guid.NewGuid();
        }
        // virtual method for default using or overwriting
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"ID: {Id}");
        }
    }
}
