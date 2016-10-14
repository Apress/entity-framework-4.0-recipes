<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Recipe2.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Entity Framework Recipes - Recipe 2</title>
</head>
<body>
  <form id="form1" runat="server">
    <div style="font-size:larger; margin: 10px;">Manage Club Members</div>
    <div>
      <asp:Button ID="Insert" Text="Insert New Member" runat="server" OnClick="InsertMember" />
      <asp:ListView ID="membersList" runat="server" DataSourceID="membersDataSource"
            DataKeyNames="MemberId" OnItemInserted="membersList_ItemInserted">
        <LayoutTemplate>
          <table>
            <tr>
              <th colspan="2">&nbsp;</th>
              <th>Name</th>
              <th>Email</th>
            </tr>
            <tr id="itemPlaceholder" runat="server" />
          </table>
        </LayoutTemplate>
        <ItemTemplate>
          <tr>
            <td><asp:LinkButton Text="Delete" CommandName="Delete" runat="server" /></td>
            <td><asp:LinkButton Text="Edit" CommandName="Edit" runat="server" /></td>
            <td><%# Eval("Name") %></td>
            <td><%# Eval("Email") %></td>
          </tr>
        </ItemTemplate>
        <InsertItemTemplate>
          <tr>
            <td colspan="4">
              <table>
                <tr>
                  <td>Name:</td>
                  <td><asp:TextBox ID="Name" runat="server" Text='<%# Bind("Name") %>' /></td>
                </tr>
                <tr>
                  <td>Email:</td>
                  <td><asp:TextBox ID="Email" runat="server" Text='<%# Bind("Email") %>' /></td>
                </tr>
                <tr>
                  <td colspan="2">
                    <asp:Button Text="Insert" CommandName="Insert" runat="server" />&nbsp;
                    <asp:Button Text="Cancel" CommandName="Cancel" OnClick="CancelClick" runat="server" />
                  </td>
                </tr>
              </table>
            </td>
          </tr>
        </InsertItemTemplate>

        <EditItemTemplate>
          <tr>
            <td colspan="4">
              <table>
                <tr>
                  <td>Name:</td>
                  <td><asp:TextBox ID="Name" runat="server" Text='<%# Bind("Name") %>' /></td>
                </tr>
                <tr>
                  <td>Email:</td>
                  <td><asp:TextBox ID="Email" runat="server" Text='<%# Bind("Email") %>' /></td>
                </tr>
                <tr>
                  <td colspan="2">
                    <asp:Button Text="Update" CommandName="Update" runat="server" /> &nbsp;
                    <asp:Button Text="Cancel" CommandName="Cancel" runat="server" />
                  </td>
                </tr>
              </table>
            </td>
          </tr>
        </EditItemTemplate>
      </asp:ListView>

      <asp:EntityDataSource ID="membersDataSource" runat="server"
            ConnectionString="name=EFRecipesEntities" DefaultContainerName="EFRecipesEntities"
            EnableInsert="true" EnableUpdate="true" EnableDelete="true" EntitySetName="Members" />

      <asp:DataPager ID="Pager" runat="server" PagedControlID="membersList" PageSize="2">
        <Fields>
          <asp:NumericPagerField ButtonCount="10" />
        </Fields>
      </asp:DataPager>
    </div>
  </form>
</body>
</html>
