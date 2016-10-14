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
            if (!this.IsPostBack)
            {
                using (var context = new EFRecipesEntities())
                {
                    // delete any previous test data
                    context.ExecuteStoreCommand("delete from chapter9.project");

                    // insert some test data
                    context.Projects.AddObject(new Project { Name = "Trim City Park Trees", AmountAllocated = 8200M, PercentComplete = 75 });
                    context.SaveChanges();
                }
            }
        }
    }
}