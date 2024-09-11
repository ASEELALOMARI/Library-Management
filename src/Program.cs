using System;

using NotificationService;
using LibraryEntitySystem;
using LibrarySystem;


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


        EmailNotificationService emailService = new EmailNotificationService();
        SMSNotificationService smsService  = new SMSNotificationService();

        Library libraryWithSMS = new Library(smsService );
        Library libraryWithEmail = new Library(emailService);


        //Add books and user to the library
        libraryWithSMS.AddUser(user1);
        libraryWithSMS.AddUser(user2);
        libraryWithEmail.AddUser(user3);
        libraryWithSMS.AddUser(user9);
        libraryWithSMS.AddUser(user9);
        libraryWithSMS.AddUser(user10);

        Console.WriteLine();

        libraryWithSMS.AddBook(book1);
        libraryWithSMS.AddBook(book2);
        libraryWithEmail.AddBook(book3);
        libraryWithEmail.AddBook(book4);
        libraryWithSMS.AddBook(book19);
        libraryWithSMS.AddBook(book19);
        libraryWithSMS.AddBook(book20);

        Console.WriteLine();


        //Find books or user by Name/Title
        Console.WriteLine($"Find book/User by name:");
        libraryWithSMS.DisplayListInfo(libraryWithSMS.FindBook("1984"));
        libraryWithSMS.DisplayListInfo(libraryWithSMS.FindBook("The"));
        libraryWithSMS.DisplayListInfo(libraryWithSMS.FindUser("Ian"));


        Console.WriteLine();

        //Delete the user/book
        Console.WriteLine($"Remove book/user");
        libraryWithSMS.RemoveBook(book2.ID);
        libraryWithSMS.RemoveBook(book6.ID); //will return not in list

        Console.WriteLine();

        libraryWithSMS.RemoveUser(user2.ID);
        Console.WriteLine();

        //get all book/user
        Console.WriteLine($"Get All books");
        libraryWithSMS.DisplayListInfo(libraryWithSMS.getAllBooks(10, 1));
        Console.WriteLine();


        Console.WriteLine($"Get All users");
        libraryWithSMS.DisplayListInfo(libraryWithSMS.getAllUser(10, 1));
        Console.WriteLine();

        Console.WriteLine($"Notification Type:");
        libraryWithEmail.PrintNotificationServiceInfo();
        libraryWithSMS.PrintNotificationServiceInfo();



    }
}