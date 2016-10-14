using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MediaEntities;
namespace MediaServices
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        void Initialize();

        [OperationContract]
        Medium GetMediaByTitle(string title);

        [OperationContract]
        void SubmitCategory(Category category);
    }
}
