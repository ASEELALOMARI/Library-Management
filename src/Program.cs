using System;

public abstract class Base
{

    private Guid id;
    private DateTime date;

    public Guid ID { get { return id; } set { id = value; } }
    public DateTime Date { get { return date; } set { date = value; } }

    public Base(DateTime? createdDate = null)
    {

        ID = Guid.NewGuid();
        Date = (DateTime)(createdDate == null ? DateTime.Now : createdDate);

    }

    public virtual void DisplayInfo()
    {

        Console.Write($"ID: {ID}, Date of created: {Date.ToShortDateString()} ");

    }

}

public class Book : Base
{

    private string title;

    public string Title { get { return title; } set { title = value; } }

    public Book(string title, DateTime? createdDate = null) : base(createdDate)
    {

        Title = title;

    }


    public override void DisplayInfo()
    {

        base.DisplayInfo();
        Console.WriteLine($"Title: {Title}");

    }

}

public class User : Base
{

    private string name;

    public string Name { get { return name; } set { name = value; } }

    public User(string name, DateTime? createdDate = null) : base(createdDate)
    {

        Name = name;

    }

    public override void DisplayInfo()
    {

        base.DisplayInfo();
        Console.WriteLine($"Name: {Name}");

    }

}

public class Library
{
    private List<Book> books = new List<Book>();
    private List<User> users = new List<User>();

    //Add / Delete book
    public void AddBook(Book newBook)
    {

        books.Add(newBook);
        Console.WriteLine($"Book '{newBook.Title}' added to the library.");

    }

    public void RemoveBook(Book bookToDelete)
    {

        if (books.Any(book => book == bookToDelete))
        {
            books.Remove(bookToDelete);
            Console.WriteLine($"book '{bookToDelete.Title}' has deleted from the library successfully.");
        }
        else
        {
            Console.WriteLine($"book '{bookToDelete.Title}' not in the library");//throw
        }

    }


    //Add /Delete user
    public void AddUser(User newMember)
    {

        users.Add(newMember);
        Console.WriteLine($"User {newMember.Name} Add successfully.");

    }


    public void RemoveUser(User memberToDelete)
    {

        if (users.Any(user => user == memberToDelete))
        {

            users.Remove(memberToDelete);
            Console.WriteLine($"User '{memberToDelete.Name}' Deleted successfully.");

        }
        else
        {
            Console.WriteLine($"User '{memberToDelete.Name}' not it the list");//throw
        }

    }


    // Get all book in the library
    public IOrderedEnumerable<Book> getAllBooks()
    {

        return books.OrderBy(book => book.Date); // add pagination

    }


    //Get all Users
    public IOrderedEnumerable<User> getAllUser()
    {

        return users.OrderBy(user => user.Date); //add pagination

    }


    // Find book by title
    public Book? FindBook(string title)
    {

        return books.FirstOrDefault(books => books.Title == title);

    }


    //Find user By name
    public User? FindUser(string name)
    {

        return users.FirstOrDefault(users => users.Name == name);

    }

}





internal class Program
{
    private static void Main()
    {

            var user1 = new User("Alice", new DateTime(2023, 1, 1));
            var user2 = new User("Bob", new DateTime(2023, 2, 1));
            var user3 = new User("Charlie", new DateTime(2023, 3, 1));
            var user4 = new User("David", new DateTime(2023, 4, 1));
            var user5 = new User("Eve", new DateTime(2024, 5, 1));
            var user6 = new User("Fiona", new DateTime(2024, 6, 1));
            var user7 = new User("George", new DateTime(2024, 7, 1));
            var user8 = new User("Hannah", new DateTime(2024, 8, 1));
            var user9 = new User("Ian");
            var user10 = new User("Julia");

            var book1 = new Book("The Great Gatsby", new DateTime(2023, 1, 1));
            var book2 = new Book("1984", new DateTime(2023, 2, 1));
            var book3 = new Book("To Kill a Mockingbird", new DateTime(2023, 3, 1));
            var book4 = new Book("The Catcher in the Rye", new DateTime(2023, 4, 1));
            var book5 = new Book("Pride and Prejudice", new DateTime(2023, 5, 1));
            var book6 = new Book("Wuthering Heights", new DateTime(2023, 6, 1));
            var book7 = new Book("Jane Eyre", new DateTime(2023, 7, 1));
            var book8 = new Book("Brave New World", new DateTime(2023, 8, 1));
            var book9 = new Book("Moby-Dick", new DateTime(2023, 9, 1));
            var book10 = new Book("War and Peace", new DateTime(2023, 10, 1));
            var book11 = new Book("Hamlet", new DateTime(2023, 11, 1));
            var book12 = new Book("Great Expectations", new DateTime(2023, 12, 1));
            var book13 = new Book("Ulysses", new DateTime(2024, 1, 1));
            var book14 = new Book("The Odyssey", new DateTime(2024, 2, 1));
            var book15 = new Book("The Divine Comedy", new DateTime(2024, 3, 1));
            var book16 = new Book("Crime and Punishment", new DateTime(2024, 4, 1));
            var book17 = new Book("The Brothers Karamazov", new DateTime(2024, 5, 1));
            var book18 = new Book("Don Quixote", new DateTime(2024, 6, 1));
            var book19 = new Book("The Iliad");
            var book20 = new Book("Anna Karenina");


            Library library = new Library();


            //Add books and user to the library
            library.AddUser(user1);
            library.AddUser(user2);
            library.AddUser(user3);
            library.AddUser(user9);
            library.AddUser(user10);

            Console.WriteLine();

            library.AddBook(book1);
            library.AddBook(book2);
            library.AddBook(book3);
            library.AddBook(book4);
            library.AddBook(book19);
            library.AddBook(book20);

            Console.WriteLine();


            //Find books or user by Name/Title
            Console.WriteLine($"Find book/User:");
            library.FindBook("1984").DisplayInfo();

            library.FindUser("Ian").DisplayInfo();

            Console.WriteLine();

            //Delete the user/book
            Console.WriteLine($"Remove book/user");
            library.RemoveBook(book3);
            library.RemoveBook(book6);

            Console.WriteLine();

            library.RemoveUser(user3);
            Console.WriteLine();

            //get all book/user
            Console.WriteLine($"Get All books");
            var libraryBooks = library.getAllBooks();

            foreach (var book in libraryBooks)
            {
                book.DisplayInfo();
            }
            Console.WriteLine();

            Console.WriteLine($"Get All users");
            var libraryUsers = library.getAllUser();

            foreach (var user in libraryUsers)
            {
                user.DisplayInfo();
            }


            Console.WriteLine();



    }
}