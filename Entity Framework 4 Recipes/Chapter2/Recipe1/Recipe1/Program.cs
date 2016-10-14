using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe1
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
                context.ExecuteStoreCommand("delete from chapter2.People");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var person = new Person() { FirstName = "Robert", MiddleName="Allen", LastName = "Doe", PhoneNumber = "867-5309" };
                context.People.AddObject(person);
                person = new Person() { FirstName = "John", MiddleName="K.", LastName = "Smith", PhoneNumber = "824-3031" };
                context.People.AddObject(person);
                person = new Person() { FirstName = "Billy", MiddleName="Albert", LastName = "Minor", PhoneNumber = "907-2212" };
                context.People.AddObject(person);
                person = new Person() { FirstName = "Kathy", MiddleName="Anne", LastName = "Ryan", PhoneNumber = "722-0038" };
                context.People.AddObject(person);

                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                foreach (var person in context.People)
                {
                    System.Console.WriteLine("{0} {1} {2}, Phone: {3}", person.FirstName, person.MiddleName, person.LastName, person.PhoneNumber);
                }
            }

            System.Console.WriteLine("Press <enter> to continue...");
            System.Console.ReadLine();
        }
    }
}
