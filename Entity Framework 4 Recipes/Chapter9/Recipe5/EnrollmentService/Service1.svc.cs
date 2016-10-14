using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using EnrollmentData;
using EnrollmentEntities;
namespace EnrollmentService
{
    public class Service1 : IService1
    {
        public void InsertTestRecord()
        {
            using (var context = new EFRecipesEntities())
            {
                // remove previous test data
                context.ExecuteStoreCommand("delete from chapter9.enrollment");
                context.ExecuteStoreCommand("delete from chapter9.course");
                context.ExecuteStoreCommand("delete from chapter9.student");

                // insert new test data
                var student = new Student { Name = "Robin Rosen", StudentId = 1 };
                var course1 = new Course { Title = "Mathematical Logic 101", CourseId = 1 };
                var course2 = new Course { Title = "Organic Chemistry 211", CourseId = 2 };
                context.Students.AddObject(student);
                context.Courses.AddObject(course1);
                context.Courses.AddObject(course2);
                context.SaveChanges();
            }
        }

        public Student SubmitStudentEnrollment(Student student)
        {
            using (var context = new EFRecipesEntities())
            {
                context.Students.ApplyChanges(student);
                context.SaveChanges();
                student.AcceptChanges();
                foreach (var enrollment in student.Enrollments)
                {
                    enrollment.AcceptChanges();
                }
                return student;
            }
        }

        public List<Course> GetCourseDetail()
        {
            using (var context = new EFRecipesEntities())
            {
                return context.Courses.Include("Enrollments.Student").ToList();
            }
        }
    }
}
