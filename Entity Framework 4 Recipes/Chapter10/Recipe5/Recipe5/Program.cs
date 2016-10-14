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
                context.ExecuteStoreCommand("delete from chapter10.message");
                context.ExecuteStoreCommand("delete from chapter10.member");
            }
        }

        static void RunExample()
        {
            DateTime today = DateTime.Parse("4/18/2010");
            using (var context = new EFRecipesEntities())
            {
                var mem1 = new Member { Name = "Jill Robertson" };
                var mem2 = new Member { Name = "Steven Rhodes" };
                mem1.Messages.Add(new Message { DateSent = today, MessageBody = "Hello Jim", Subject = "Hello" });
                mem1.Messages.Add(new Message { DateSent = today, MessageBody = "Wonderful weather!", Subject = "Weather" });
                mem1.Messages.Add(new Message { DateSent = today, MessageBody = "Meet me for lunch", Subject = "Lunch plans" });
                mem2.Messages.Add(new Message { DateSent = today, MessageBody = "Going to class today?", Subject = "What's up?" });
                context.Members.AddObject(mem1);
                context.Members.AddObject(mem2);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Members by message count for {0}", today.ToShortDateString());
                var members = context.MembersWithTheMostMessages(today);
                foreach (var member in members)
                {
                    Console.WriteLine("Member: {0}", member.Name);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
