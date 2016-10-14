using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnrollmentEntities;
using EnrollmentClient.ServiceReference1;
namespace EnrollmentClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new Service1Client())
            {
                // insert test data
                client.InsertTestRecord();

                // create some entities with known
                // a known student id and course id
                var student1 = new Student { StudentId = 1 };
                var course1 = new Course { CourseId = 1 };
                student1.MarkAsUnchanged();
                course1.MarkAsUnchanged();

                // enroll the student in two courses, using both
                // an entity and the foreign key
                student1.Enrollments.Add(new Enrollment { Course = course1, Paid = true });
                student1.Enrollments.Add(new Enrollment { CourseId = 2, Paid = false });

                // save the enrollments
                student1 = client.SubmitStudentEnrollment(student1);

                // now drop courses the student has not paid for
                foreach (var enrollment in student1.Enrollments.Where(e => !e.Paid).ToArray())
                {
                    enrollment.MarkAsDeleted();
                }

                // student got married!, change name
                student1.Name += "Robin Rosen-Parker";
                client.SubmitStudentEnrollment(student1);

                // retrieve the courses and enrollments
                foreach (var course in client.GetCourseDetail())
                {
                    Console.WriteLine("Course: {0}", course.Title);
                    foreach (var en in course.Enrollments)
                    {
                        Console.WriteLine("\tStudent: {0} {1} paid", en.Student.Name, en.Paid ? "has" : "has not");
                    }
                }

                Console.WriteLine("Press <enter> to continue...");
                Console.ReadLine();
            }
        }
    }
}
