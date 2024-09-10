using System;

public interface INotificationService
{
    public void SendNotificationOnSuccess(Base Object, string ObjectType, string Action, string SupportContact);
    public void SendNotificationOnFailure(Base Object, string ObjectType, string Action, string SupportContact);
}


public class EmailNotificationService : INotificationService
{


    //emailMassage method
    public string SuccessEmailMessage(Base Object, string ObjectType, string Action, string SupportContact)
    {
        string message = $"New {ObjectType} Named '{Object.Name}' has been successfully {Action} to the Library.\n\n If you have any queries or feedback, please contact our support team at {SupportContact}.\n";
        return message;
    }


    public string FailureEmailMessage(Base Object, string ObjectType, string Action, string SupportContact)
    {
        string message = $"We encountered an issue {Action} {ObjectType} Named '{Object.Name}'. Please review the input data. For more help, please contact {SupportContact}.\n";
        return message;
    }


    public void SendNotificationOnSuccess(Base Object, string ObjectType, string Action, string SupportContact)
    {
        Console.WriteLine($"{SuccessEmailMessage(Object, ObjectType, Action, SupportContact)}");
    }


    public void SendNotificationOnFailure(Base Object, string ObjectType, string Action, string SupportContact)
    {
        Console.WriteLine($"{FailureEmailMessage(Object, ObjectType, Action, SupportContact)}");
    }


}

public class SMSNotificationService : INotificationService
{

    private string SuccessSMSMessage(Base Object, string ObjectType, string Action, string SupportContact)
    {
        string message = $"The {ObjectType} '{Object.Name}' {Action} successfully. Thank you!\n";
        return message;
    }


    private string FailureSMSMessage(Base Object, string ObjectType, string Action, string SupportContact)
    {
        string message = $"Error {Action} {ObjectType} '{Object.Name}'. please contact {SupportContact}.\n";
        return message;
    }


    public void SendNotificationOnSuccess(Base Object, string ObjectType, string Action, string SupportContact)
    {
        Console.WriteLine(SuccessSMSMessage(Object, ObjectType, Action, SupportContact));
    }

    public void SendNotificationOnFailure(Base Object, string ObjectType, string Action, string SupportContact)
    {
        Console.WriteLine(FailureSMSMessage(Object, ObjectType, Action, SupportContact));
    }


}


public abstract class Base
{

    private Guid id;
    private DateTime date;
    private string name;

    public Guid ID { get { return id; } set { id = value; } }
    public DateTime Date { get { return date; } set { date = value; } }

    public string Name { get { return name; } set { name = value; } }

    public Base(string name, DateTime? createdDate = null)
    {

        ID = Guid.NewGuid();
        Date = (DateTime)(createdDate == null ? DateTime.Now : createdDate);
        Name = name;

    }

    public virtual void DisplayInfo()
    {

        Console.Write($"ID: {ID}, Date of created: {Date.ToShortDateString()} ");

    }

}

public class Book : Base
{

    public Book(string title, DateTime? createdDate = null) : base(title, createdDate) { }


    public override void DisplayInfo()
    {

        base.DisplayInfo();
        Console.WriteLine($"Title: {Name}");

    }

}

public class User : Base
{

    public User(string name, DateTime? createdDate = null) : base(name, createdDate) { }

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
    private INotificationService _notificationService;


    public Library(INotificationService notificationService)
    {
        _notificationService = notificationService; 
    }


    //Add / Delete book
    public void AddBook(Book newBook)
    {


        //check if book already exist
        if (books.Any(x => x == newBook))
        {
            Console.WriteLine($"the book '{newBook.Name}' already exists.");//todo throw
            _notificationService.SendNotificationOnFailure(newBook, "Book", "Added", "Support@sup.com");
            return;
        }

        books.Add(newBook);
        Console.WriteLine($"Book '{newBook.Name}' added to the library.");
        _notificationService.SendNotificationOnSuccess(newBook, "Book", "Added", "Support@sup.com");

    }

    public void RemoveBook(Guid bookToDeleteID)
    {

        if (books.Any(book => book.ID == bookToDeleteID))
        {
            var bookToDelete = books.FirstOrDefault(book => book.ID == bookToDeleteID);
            books.Remove(bookToDelete);
            Console.WriteLine($"book '{bookToDelete.Name}' has deleted from the library successfully.");
            _notificationService.SendNotificationOnSuccess(bookToDelete, "Book", "Delete", "Support@sup.com");
        }
        else
        {
            Console.WriteLine($"book with that ID not in the library");//todo throw
        }

    }


    //Add /Delete user
    public void AddUser(User newMember)
    {
        //Check if the User already exist.
        if (users.Any(x => x == newMember))
        {
            Console.WriteLine($"The user '{newMember.Name}' already exists.");//todo throw
            _notificationService.SendNotificationOnFailure(newMember, "User", "Add", "Support@sup.com");
            return;
        }
        users.Add(newMember);
        Console.WriteLine($"User {newMember.Name} Add successfully.");
        _notificationService.SendNotificationOnSuccess(newMember, "User", "Added", "Support@sup.com");

    }


    public void RemoveUser(Guid memberToDeleteID)
    {

        if (users.Any(user => user.ID == memberToDeleteID))
        {

            var memberToDelete = users.FirstOrDefault(user => user.ID == memberToDeleteID);
            users.Remove(memberToDelete);
            Console.WriteLine($"User '{memberToDelete.Name}' Deleted successfully.");
            _notificationService.SendNotificationOnSuccess(memberToDelete, "Book", "Delete", "Support@sup.com");

        }
        else
        {
            Console.WriteLine($"User with that ID not it the list");//todo throw
        }

    }


    // Get all book in the library
    public IEnumerable<Book> getAllBooks(int limitPerPage, int pageNumber = 1)
    {

        return books.OrderBy(book => book.Date).Skip((pageNumber - 1) * limitPerPage).Take(limitPerPage);

    }


    //Get all Users
    public IEnumerable<User> getAllUser(int limitPerPage, int pageNumber = 1)
    {

        return users.OrderBy(user => user.Date).Skip((pageNumber - 1) * limitPerPage).Take(limitPerPage);

    }


    // Find book by title
    public List<Book> FindBook(string title)
    {

        return books.FindAll(books => books.Name.Contains(title, StringComparison.OrdinalIgnoreCase));

    }


    //Find user By name
    public List<User> FindUser(string name)
    {

        return users.FindAll(users => users.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

    }


    //print any list
    public void DisplayListInfo(IEnumerable<Base> collection)
    {

        foreach (var item in collection)
        {
            item.DisplayInfo();
        }
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
        var book15 = new Book("The Divine Comedy", new DateTime(2024, 3, 1));
        var book16 = new Book("Crime and Punishment", new DateTime(2024, 4, 1));
        var book17 = new Book("The Brothers Karamazov", new DateTime(2024, 5, 1));
        var book18 = new Book("Don Quixote", new DateTime(2024, 6, 1));
        var book19 = new Book("The Iliad");
        var book20 = new Book("Anna Karenina");


        SMSNotificationService SMS = new SMSNotificationService();
        Library library = new Library(SMS);


        //Add books and user to the library
        library.AddUser(user1);
        library.AddUser(user2);
        library.AddUser(user3);
        library.AddUser(user9);
        library.AddUser(user9);
        library.AddUser(user10);

        Console.WriteLine();

        library.AddBook(book1);
        library.AddBook(book2);
        library.AddBook(book3);
        library.AddBook(book4);
        library.AddBook(book19);
        library.AddBook(book19);
        library.AddBook(book20);

        Console.WriteLine();


        //Find books or user by Name/Title
        Console.WriteLine($"Find book/User by name:");
        library.DisplayListInfo(library.FindBook("1984"));
        library.DisplayListInfo(library.FindBook("The"));
        library.DisplayListInfo(library.FindUser("Ian"));


        Console.WriteLine();

        //Delete the user/book
        Console.WriteLine($"Remove book/user");
        library.RemoveBook(book3.ID);
        library.RemoveBook(book6.ID); //will return not in list

        Console.WriteLine();

        library.RemoveUser(user3.ID);
        Console.WriteLine();

        //get all book/user
        Console.WriteLine($"Get All books");
        library.DisplayListInfo(library.getAllBooks(10, 1));
        Console.WriteLine();


        Console.WriteLine($"Get All users");
        library.DisplayListInfo(library.getAllUser(10, 1));
        Console.WriteLine();



    }
}