using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.Objects.DataClasses;

namespace Recipe12
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
                context.ExecuteStoreCommand("delete from chapter11.webproduct");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var w1 = new WebProduct { Name = "Camping Tent", Description = "Family Camping Tent, Color Green" };
                var w2 = new WebProduct { Name = "Chemical Light" };
                var w3 = new WebProduct { Name = "Ground Cover", Description = "Blue ground cover" };
                context.WebProducts.AddObject(w1);
                context.WebProducts.AddObject(w2);
                context.WebProducts.AddObject(w3);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Query using eSql...");
                var esql = @"select value EFRecipesModel.Store.ISNULL(p.Description,p.Name)
                             from EFRecipesEntities.WebProducts as p";
                var prods = context.CreateQuery<string>(esql);
                foreach (var prod in prods)
                {
                    Console.WriteLine("Product Description: {0}", prod);
                }
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine();
                Console.WriteLine("Query using LINQ...");
                var prods = from p in context.WebProducts
                            select BuiltinFunctions.ISNULL(p.Description, p.Name);
                foreach (var prod in prods)
                {
                    Console.WriteLine(prod);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }

    public class BuiltinFunctions
    {
        [EdmFunction("EFRecipesModel.Store", "ISNULL")]
        public static string ISNULL(string check_expression, string replacementvalue)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }
    }
}
