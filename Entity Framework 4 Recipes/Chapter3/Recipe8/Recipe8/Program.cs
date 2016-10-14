using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe8
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
                context.ExecuteStoreCommand("delete from chapter3.book");
                context.ExecuteStoreCommand("delete from chapter3.category");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var cat1 = new Category { Name = "Programming" };
                var cat2 = new Category { Name = "Databases" };
                var cat3 = new Category {Name = "Operating Systems"};
                context.Books.AddObject(new Book { Title = "F# In Practice", Category = cat1 });
                context.Books.AddObject(new Book { Title = "The Joy of SQL", Category = cat2 });
                context.Books.AddObject(new Book { Title = "Windows 7: The Untold Story", Category = cat3 });
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Books (using LINQ)");
                List<string> cats = new List<string> { "Programming", "Databases"};
                var books = from b in context.Books
                            where cats.Contains(b.Category.Name)
                            select b;
                foreach (var book in books)
                {
                    Console.WriteLine("'{0}' is in category: {1}", book.Title, book.Category.Name);
                }
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Books (using ESQL)");
                var esql = @"select value b from Books as b
                             where b.Category.Name in {'Programming','Databases'}";
                var books = context.CreateQuery<Book>(esql);
                foreach (var book in books)
                {
                    Console.WriteLine("'{0}' is in category: {1}", book.Title, book.Category.Name);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
