<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Recipe6.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Entity Framework Recipes - Recipe 6</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <h2>Employees</h2>
      <asp:GridView ID="GridView1" runat="server" DataSourceID="EmployeesSource" AutoGenerateColumns="true" />
      <asp:EntityDataSource ID="EmployeesSource" runat="server" ConnectionString="name=EFRecipesEntities" DefaultContainerName="EFRecipesEntities" EnableFlattening="false" EntitySetName="Contacts" EntityTypeFilter="Employee" />

      <h2>Customers</h2>
      <asp:GridView ID="GridView2" runat="server" DataSourceID="CustomersSource" AutoGenerateColumns="true" />
      <asp:EntityDataSource ID="CustomersSource" runat="server" ConnectionString="name=EFRecipesEntities" DefaultContainerName="EFRecipesEntities" EnableFlattening="false" EntitySetName="Contacts" EntityTypeFilter="Customer" />

    </div>
    </form>
</body>
</html>
