using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;

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
                context.ExecuteStoreCommand("delete from chapter3.student");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                context.Students.AddObject(new Student { FirstName = "Robert", LastName = "Smith", Degree = "Masters" });
                context.Students.AddObject(new Student { FirstName = "Julia", LastName = "Kerns", Degree = "Masters" });
                context.Students.AddObject(new Student { FirstName = "Nancy", LastName = "Stiles", Degree = "Doctorate" });
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                string sql = "select * from Chapter3.Student where Degree = @Major";
                var args = new DbParameter[] {
                    new SqlParameter {ParameterName = "Major", Value = "Masters"}};
                var students = context.ExecuteStoreQuery<Student>(sql, args);
                Console.WriteLine("Students...");
                foreach (var student in students)
                {
                    Console.WriteLine("{0} {1} is working on a {2} degree", student.FirstName, student.LastName, student.Degree);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
