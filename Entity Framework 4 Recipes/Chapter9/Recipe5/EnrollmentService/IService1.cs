using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using EnrollmentEntities;

namespace EnrollmentService
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        void InsertTestRecord();

        [OperationContract]
        Student SubmitStudentEnrollment(Student student);

        [OperationContract]
        List<Course> GetCourseDetail();
    }
}
