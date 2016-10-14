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
                context.ExecuteStoreCommand("delete from chapter5.sectionstudent");
                context.ExecuteStoreCommand("delete from chapter5.section");
                context.ExecuteStoreCommand("delete from chapter5.student");
                context.ExecuteStoreCommand("delete from chapter5.course");
                context.ExecuteStoreCommand("delete from chapter5.instructor");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var course = new Course { Title = "Biology 101" };
                var fred = new Instructor { Name = "Fred Jones" };
                var julia = new Instructor { Name = "Julia Canfield" };
                var section1 = new Section { Course = course, Instructor = fred };
                var section2 = new Section { Course = course, Instructor = julia };
                var jim = new Student { Name = "Jim Roberts" };
                jim.Sections.Add(section1);
                var jerry = new Student { Name = "Jerry Jones" };
                jerry.Sections.Add(section2);
                var susan = new Student { Name = "Susan O'Reilly" };
                susan.Sections.Add(section1);
                var cathy = new Student { Name = "Cathy Ryan" };
                cathy.Sections.Add(section2);
                context.Courses.AddObject(course);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                var graph = context.Courses.Include("Sections.Instructor").Include("Sections.Students");
                Console.WriteLine("Courses");
                Console.WriteLine("=======");
                foreach (var course in graph)
                {
                    Console.WriteLine("{0}", course.Title);
                    foreach (var section in course.Sections)
                    {
                        Console.WriteLine("\tSection: {0}, Instrutor: {1}", section.SectionId.ToString(), section.Instructor.Name);
                        Console.WriteLine("\tStudents:");
                        foreach (var student in section.Students)
                        {
                            Console.WriteLine("\t\t{0}", student.Name);
                        }
                        Console.WriteLine("\n");
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
