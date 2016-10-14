using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                context.ExecuteStoreCommand("delete from chapter6.member");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var teen = new Teen { Name = "Steven Keller", Age = 17, Phone = "817 867-5309" };
                var adult = new Adult { Name = "Margret Jones", Age = 53, Phone = "913 294-6059" };
                var senior = new Senior { Name = "Roland Park", Age = 71, Phone = "816 353-4458" };
                context.Members.AddObject(teen);
                context.Members.AddObject(adult);
                context.Members.AddObject(senior);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Club Members");
                Console.WriteLine("============");
                foreach(var member in context.Members)
                {
                    bool printPhone = true;
                    string str = string.Empty;
                    if (member is Teen)
                    {
                        str = " a Teen";
                        printPhone = false;
                    }
                    else if (member is Adult)
                        str = "an Adult";
                    else if (member is Senior)
                        str = "a Senior";
                    Console.WriteLine("{0} is {1} member, phone: {2}",member.Name, str, printPhone ? member.Phone : "unavailable");
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }

    public partial class EFRecipesEntities
    {
        partial void OnContextCreated()
        {
            this.SavingChanges +=new EventHandler(Validate);
        }

        public void Validate(object sender, EventArgs e)
        {
            var entities = this.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added | System.Data.EntityState.Modified).Select(et => et.Entity as Member);
            foreach (var member in entities)
            {
                if (member is Teen && member.Age > 19)
                {
                    throw new ApplicationException("Entity Valdiation Failed");
                }
                else if (member is Adult && (member.Age < 20))
                {
                    throw new ApplicationException("Entity Valdiation Failed");
                }
                else if (member is Senior && (member.Age < 55))
                {
                    throw new ApplicationException("Entity Valdiation Failed");
                }
            }
        }
    }
}
