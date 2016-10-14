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
                context.ExecuteStoreCommand("delete from chapter12.course");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var course1 = new Course { CourseName = "CS 301" };
                var course2 = new Course { CourseName = "Math 455" };
                var en1 = new Enrollment { Student = "James Folk" };
                var en2 = new Enrollment { Student = "Scott Shores" };
                var en3 = new Enrollment { Student = "Jill Glass" };
                var en4 = new Enrollment { Student = "Robin Rosen" };
                var class1 = new Class { Instructor = "Bill Meyers" };
                var class2 = new Class { Instructor = "Norma Hall" };
                class1.Course = course1;
                class2.Course = course2;
                class1.Enrollments.Add(en1);
                class1.Enrollments.Add(en2);
                class2.Enrollments.Add(en3);
                class2.Enrollments.Add(en4);
                context.Classes.AddObject(class1);
                context.Classes.AddObject(class2);
                context.SaveChanges();
                context.Classes.DeleteObject(class1);
                context.SaveChanges();
            }
            using (var context = new EFRecipesEntities())
            {
                foreach (var course in context.Courses)
                {
                    Console.WriteLine("Course: {0}", course.CourseName);
                    foreach (var c in course.Classes)
                    {
                        Console.WriteLine("\tClass: {0}, Instructor: {1}", c.ClassId.ToString(), c.Instructor);
                        foreach (var en in c.Enrollments)
                        {
                            Console.WriteLine("\t\tStudent: {0}", en.Student);
                        }
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
