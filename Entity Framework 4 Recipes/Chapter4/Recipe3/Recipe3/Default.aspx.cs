using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Recipe3
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        /*
        protected void ordersource_ContextCreating(object sender, EntityDataSourceContextCreatingEventArgs e)
        {
            e.Context = new EFRecipesEntities();
        }
         */
    }

    public partial class EFRecipesEntities
    {
        partial void OnContextCreated()
        {
            this.SavingChanges += (o, e) =>
                {
                    var orders = this.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added).Select(en => en.Entity as PurchaseOrder);
                    foreach (var order in orders)
                    {
                        order.CreateDate = DateTime.Now;
                    }
                };
        }
    }
}