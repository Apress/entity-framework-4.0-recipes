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
                context.ExecuteStoreCommand("delete from chapter2.picturecategory");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var louvre = new PictureCategory { Name = "Louvre" };
                var child = new PictureCategory { Name = "Egyptian Antiquites" };
                louvre.Subcategories.Add(child);
                child = new PictureCategory { Name = "Sculptures" };
                louvre.Subcategories.Add(child);
                child = new PictureCategory { Name = "Paintings" };
                louvre.Subcategories.Add(child);
                var paris = new PictureCategory { Name = "Paris" };
                paris.Subcategories.Add(louvre);
                var vacation = new PictureCategory { Name = "Summer Vacation" };
                vacation.Subcategories.Add(paris);
                context.PictureCategories.AddObject(paris);

                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                context.ContextOptions.LazyLoadingEnabled = true;
                PictureCategory root = (from c in context.PictureCategories 
                                        where c.ParentCategory == null 
                                        select c).FirstOrDefault();
                Print(root, 0);
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }

        static void Print(PictureCategory cat, int level)
        {
            StringBuilder sb = new StringBuilder();
            Console.WriteLine("{0}{1}", sb.Append(' ', level).ToString(), cat.Name);
            foreach (PictureCategory child in cat.Subcategories)
            {
                Print(child, level + 1);
            }
        }
    }
}
