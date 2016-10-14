using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe2
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
                context.ExecuteStoreCommand("delete from chapter12.[user]");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var user1 = new User { FullName = "Robert Meyers", UserName = "RM" };
                var user2 = new User { FullName = "Karen Kelley", UserName = "KKelley" };
                context.Users.AddObject(user1);
                context.Users.AddObject(user2);
                context.SaveChanges();
                Console.WriteLine("Users saved to database");
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine();
                Console.WriteLine("Reading users from database");
                foreach (var user in context.Users)
                {
                    Console.WriteLine("{0} is {1}, UserName is {2}", user.FullName, user.IsActive ? "Active" : "Inactive", user.UserName);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }

    public partial class User
    {
        partial void OnUserNameChanging(string value)
        {
            if (value.Length > 5)
                Console.WriteLine("{0}'s UserName changing to {1}, OK!", this.FullName, value);
            else
                Console.WriteLine("{0}'s UserName changing to {1}, Too Short!", this.FullName, value);
        }

        partial void OnUserNameChanged()
        {
            this.IsActive = (this.UserName.Length > 5);
        }
    }
}
