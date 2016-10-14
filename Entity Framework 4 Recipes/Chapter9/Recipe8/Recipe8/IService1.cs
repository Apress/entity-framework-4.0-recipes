using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Recipe8
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        void InsertTestRecord();

        [OperationContract]
        Client GetClient();

        [OperationContract]
        void Update(Client client);
    }
}
