using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe5
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
                context.ExecuteStoreCommand("delete from chapter6.category");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var book = new Category { Name = "Books" };
                var fiction = new Category { Name = "Fiction", ParentCategory = book };
                var nonfiction = new Category { Name = "Non-Fiction", ParentCategory = book };
                var novel = new Category { Name = "Novel", ParentCategory = fiction };
                var history = new Category { Name = "History", ParentCategory = nonfiction };
                context.Categories.AddObject(book);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                var root = context.Categories.Where(o => o.Name == "Books").First();
                Console.WriteLine("Parent category is {0}, subcategories are:", root.Name);
                foreach (var sub in context.GetSubCategories(root.CategoryId))
                {
                    Console.WriteLine("\t{0}", sub.Name);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
