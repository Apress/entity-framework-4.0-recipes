using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using BookingEntities;
namespace BookingService
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        TravelAgent GetAgentWithBookings();

        [OperationContract]
        void SubmitAgentBookings(TravelAgent agent);
    }
}
