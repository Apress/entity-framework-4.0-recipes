<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Recipe4.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Entity Framework Recipes - Recipe 4</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:ListView ID="orderslist" runat="server" DataSourceId="orders">
        <LayoutTemplate>
          <table>
            <tr>
              <th>Name</th>
              <th>Amount</th>
              <th>OrderDate</th>
            </tr>
            <tr id="itemPlaceHolder" runat="server" />
          </table>
        </LayoutTemplate>
        <ItemTemplate>
          <tr>
            <td><%# Eval("WebCustomer.Name") %></td>
            <td><%# Eval("Amount") %></td>
            <td><%# Eval("OrderDate") %></td>
          </tr>
        </ItemTemplate>
      </asp:ListView>
      <asp:EntityDataSource ID="orders" runat="server" DefaultContainerName="EFRecipesEntities" Include="WebCustomer" ConnectionString="name=EFRecipesEntities" EntitySetName="Orders" />
    </div>
    </form>
</body>
</html>
