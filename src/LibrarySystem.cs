using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using NotificationService;
using LibraryEntitySystem;


namespace LibrarySystem
{


public class Library
    {
        private List<Book> books = new List<Book>();
        private List<User> users = new List<User>();
        private NotificationService.INotificationService _notificationService;


        public Library(NotificationService.INotificationService notificationService)
        {
            _notificationService = notificationService;
        }


        // Adds a new book to the library.
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


        // Remove an Exiting book from the library
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


        // Adds a new user to the library.
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


        // Remove an Exiting user from the library
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


        // Retrieves all books with pagination.
        public IEnumerable<Book> getAllBooks(int limitPerPage, int pageNumber = 1)
        {

            return books.OrderBy(book => book.Date).Skip((pageNumber - 1) * limitPerPage).Take(limitPerPage);

        }


        // Retrieves all users with pagination.
        public IEnumerable<User> getAllUser(int limitPerPage, int pageNumber = 1)
        {

            return users.OrderBy(user => user.Date).Skip((pageNumber - 1) * limitPerPage).Take(limitPerPage);

        }


        // Find books by title.
        public List<Book> FindBook(string title)
        {

            return books.FindAll(books => books.Name.Contains(title, StringComparison.OrdinalIgnoreCase));

        }


        // Find users By name.
        public List<User> FindUser(string name)
        {

            return users.FindAll(users => users.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

        }


        // Displays information for any list of library entities.
        public void DisplayListInfo(IEnumerable<LibraryEntity> collection)
        {

            foreach (var item in collection)
            {
                item.DisplayInfo();
            }
        }

    }
}