using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Recipe2
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                using (var context = new EFRecipesEntities())
                {
                    context.ExecuteStoreCommand("delete from chapter4.Member");
                    context.Members.AddObject(new Member { Name = "Robert Dewey", Email = "RobertD@gmail.com" });
                    context.Members.AddObject(new Member { Name = "Nancy Steward", Email = "NSteward@AOL.com" });
                    context.Members.AddObject(new Member { Name = "Robin Rosen", Email = "RRosen@Regenix.com" });
                    context.SaveChanges();
                }
            }
        }

        protected void membersList_ItemInserted(object sender, ListViewInsertedEventArgs e)
        {
            if (e.Exception == null)
            {
                membersList.InsertItemPosition = InsertItemPosition.None;
            }
        }

        protected void CancelClick(object sender, EventArgs e)
        {
            membersList.InsertItemPosition = InsertItemPosition.None;
        }

        protected void InsertMember(object sender, EventArgs e)
        {
            membersList.InsertItemPosition = InsertItemPosition.FirstItem;
        }
    }
}