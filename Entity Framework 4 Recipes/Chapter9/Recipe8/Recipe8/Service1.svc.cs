using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;

namespace Recipe8
{
    public class Service1 : IService1
    {
        [ApplyProxyDataContractResolver]
        public Client GetClient()
        {
            using (var context = new EFRecipesEntities())
            {
                context.ContextOptions.LazyLoadingEnabled = false;
                return context.Clients.Single();
            }
        }

        public void Update(Client client)
        {
            using (var context = new EFRecipesEntities())
            {
                context.Clients.Attach(client);
                context.ObjectStateManager.ChangeObjectState(client, EntityState.Modified);
                context.SaveChanges();
            }
        }

        public void InsertTestRecord()
        {
            using (var context = new EFRecipesEntities())
            {
                // delete previous test data
                context.ExecuteStoreCommand("delete from chapter9.client");

                // insert new test data
                context.ExecuteStoreCommand(@"insert into chapter9.client(Name, Email) values ('Jerry Jones','jjones@gmail.com')");
            }
        }
    }
}
