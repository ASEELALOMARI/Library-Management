using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace LibraryEntitySystem
{


    public abstract class LibraryEntity
    {

        private Guid id;
        private DateTime date;
        private string name;

        public Guid ID { get { return id; } set { id = value; } }
        public DateTime Date { get { return date; } set { date = value; } }

        public string Name { get { return name; } set { name = value; } }

        public LibraryEntity(string name, DateTime? createdDate = null)
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


    public class Book : LibraryEntity
    {

        public Book(string title, DateTime? createdDate = null) : base(title, createdDate) { }


        public override void DisplayInfo()
        {

            base.DisplayInfo();
            Console.WriteLine($"Title: {Name}");

        }

    }


    public class User : LibraryEntity
    {

        public User(string name, DateTime? createdDate = null) : base(name, createdDate) { }


        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Name: {Name}");
        }

    }

}
