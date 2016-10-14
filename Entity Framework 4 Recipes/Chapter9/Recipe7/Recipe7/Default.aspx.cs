using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Recipe7
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                using (var context = new EFRecipesEntities())
                {
                    // delete previous test data
                    context.ExecuteStoreCommand("delete from chapter9.phone");
                    context.ExecuteStoreCommand("delete from chapter9.customer");
                }
            }
        }

        private void ShowCustomer()
        {
            if (this.Session["Customer"] != null)
            {
                Customer customer = (Customer)this.Session["Customer"];
                this.CustomerName.Text = customer.Name;
                if (customer.Phones.Count > 0)
                {
                    this.PhoneNumber.Text = customer.Phones[0].Number;
                }
            }
        }
        protected void CreateCustomer(object sender, EventArgs e)
        {
            var customer = new Customer { Name = "Phillip Marlowe", Company = "Chandler Enterprises" };
            customer.Phones.Add(new Phone { Number = "212 555-5555", PhoneType = "Office" });
            using (var repository = new CustomerRepository())
            {
                repository.SubmitCustomerWithPhones(customer);
            }
        }

        protected void ReadCustomer(object sender, EventArgs e)
        {
            using (var repository = new CustomerRepository())
            {
                this.Session["Customer"] = repository.GetCustomer("Phillip Marlowe");
            }
            ShowCustomer();
        }

        protected void UpdateCustomer(object sender, EventArgs e)
        {
            Customer customer = (Customer)this.Session["Customer"];
            var number = customer.Phones.FirstOrDefault(p => p.PhoneType == "Office");
            if (number != null)
                number.MarkAsDeleted();
            customer.Phones.Add(new Phone { Number = "817 867-5309", PhoneType = "Cell" });
            using (var repository = new CustomerRepository())
            {
                var cust = repository.SubmitCustomerWithPhones(customer);
                cust.StartTracking();
                this.Session["Customer"] = cust;
            }
            ShowCustomer();
        }
    }
}