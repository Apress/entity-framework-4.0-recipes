using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recipe7
{
    public class CustomerRepository : IDisposable 
    {
        private EFRecipesEntities context;

        public CustomerRepository()
        {
            this.context = new EFRecipesEntities();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        public Customer GetCustomer(string name)
        {
            var customer = this.context.Customers.Include("Phones").FirstOrDefault(c => c.Name == name);
            if (customer != null)
                this.context.StartSelfTracking();
            return customer;
        }

        public Customer SubmitCustomerWithPhones(Customer customer)
        {
            this.context.Customers.ApplyChanges(customer);
            this.context.SaveChanges();
            return customer;
        }
    }
}