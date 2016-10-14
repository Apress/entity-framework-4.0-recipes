using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe11
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
                context.ExecuteStoreCommand("delete from chapter13.member");
            }
        }
        
        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                context.ExecuteStoreCommand(@"insert into chapter13.member(name,salary) 
                   values ('Steven Jones',45000)");
                context.ExecuteStoreCommand(@"insert into chapter13.member(name,salary)
                   values ('Kathy Kurtz', 85000)");
                context.ExecuteStoreCommand(@"insert into chapter13.member(name,salary)
                   values ('Aaron McCabe', 82000)");
            }

            using (var context = new EFRecipesEntities())
            {
                var upperclass = context.Members.OfType<UpperClass>();
                foreach (var member in upperclass)
                {
                    Console.WriteLine("{0}", member.Name);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
