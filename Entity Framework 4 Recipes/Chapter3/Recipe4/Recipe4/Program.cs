using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.EntityClient;
using System.Data;

namespace Recipe4
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
                context.ExecuteStoreCommand("delete from chapter3.teacher");
                context.ExecuteStoreCommand("delete from chapter3.lawyer");
                context.ExecuteStoreCommand("delete from chapter3.person");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                context.People.AddObject(new Teacher { Name = "Janet Dietz", IsProfessor = true });
                context.People.AddObject(new Teacher { Name = "Robert Kline", IsProfessor = false });
                context.People.AddObject(new Lawyer { Name = "Jenny Dunlap", Cases = 3 });
                context.People.AddObject(new Lawyer { Name = "Karen Eads", Cases = 7 });
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                var esql = "select value p from OfType(People,Recipe4.Teacher) as p";
                var teachers = context.CreateQuery<Teacher>(esql);
                Console.WriteLine("Teachers...Using Object Services");
                foreach (var teacher in teachers)
                {
                    Console.WriteLine("{0} is{1} a professor", teacher.Name, teacher.IsProfessor ? "" : " not");
                }
            }

            Console.WriteLine();

            using (var conn = new EntityConnection("name=EFRecipesEntities"))
            {
                conn.Open();
                var esql = "select value p from OfType(EFRecipesEntities.People,EFRecipesModel.Teacher) as p";
                var cmd = conn.CreateCommand();
                cmd.CommandText = esql;
                Console.WriteLine("Teachers...Using EntityClient");
                using (var reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0} is{1} a professor", reader.GetString(1), reader.GetBoolean(2) ? "" : " not");
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
