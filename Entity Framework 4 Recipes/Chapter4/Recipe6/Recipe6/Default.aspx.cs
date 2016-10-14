using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Recipe6
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var context = new EFRecipesEntities())
            {
                // delete the previous test data
                context.ExecuteStoreCommand("delete from chapter4.contact");

                // insert some new test data
                context.Contacts.AddObject(new Customer { Name = "Joan Ryan", Email = "joanr@gmail.com" });
                context.Contacts.AddObject(new Customer { Name = "Robert Kelly", Email = "rkelly@gmail.com" });
                context.Contacts.AddObject(new Employee { Name = "Karen Stanford", HireDate = DateTime.Parse("1/21/2010")});
                context.Contacts.AddObject(new Employee { Name = "Phil Marlowe", HireDate = DateTime.Parse("2/12/2009") });
                context.SaveChanges();
            }
        }
    }
}