using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe9
{
    class Program
    {
        static void Main(string[] args)
        {
            Cleanup();
            RunExample();
        }

        static void Cleanup()
        {
            using (var context = new EFRecipesEntities())
            {
                context.ExecuteStoreCommand("delete from chapter10.authorbook");
                context.ExecuteStoreCommand("delete from chapter10.author");
                context.ExecuteStoreCommand("delete from chapter10.book");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var auth1 = new Author { Name = "Jane Austin"};
                var book1 = new Book { Title = "Pride and Prejudice", ISBN = "1848373104" };
                var book2 = new Book { Title = "Sense and Sensibility", ISBN = "1440469563" };
                auth1.Books.Add(book1);
                auth1.Books.Add(book2);
                var auth2 = new Author { Name = "Audrey Niffenegger" };
                var book3 = new Book { Title = "The Time Traveler's Wife", ISBN = "015602943X" };
                auth2.Books.Add(book3);
                context.Authors.AddObject(auth1);
                context.Authors.AddObject(auth2);
                context.SaveChanges();
                context.DeleteObject(book1);
                context.SaveChanges();
            }

            Console.WriteLine("No output, check out the SQL Profiler to see what happens");
            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
