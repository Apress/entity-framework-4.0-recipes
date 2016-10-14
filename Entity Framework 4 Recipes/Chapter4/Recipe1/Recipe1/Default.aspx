<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Recipe1.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Entity Framework Recipes - Recipe 1</title>
</head>
<body>
 <form id="form1" runat="server">
   <div>
     <table>
       <tr>
         <td>Name</td>
         <td><asp:TextBox ID="Name" runat="server" /></td>
       </tr>
       <tr>
         <td>City</td>
         <td><asp:TextBox ID="City" runat="server" /></td>
       </tr>
       <tr>
         <td>State</td>
         <td><asp:TextBox ID="State" runat="server" /></td>
       </tr>
       <tr>
         <td colspan="2">
           <asp:Button ID="SearchCustomer" Text="Search" runat="server" />
         </td>
       </tr>
     </table>

     <asp:EntityDataSource ID="CustomerList" runat="server" 
       ConnectionString="name=EFRecipesEntities" 
       DefaultContainerName="EFRecipesEntities" 
       Where="(@State is null || it.State = @State) && 
              (@City is null || it.City = @City) && 
              (@Name is null || it.Name LIKE '%' + @Name + '%')"
       EntitySetName="Customers">
       <WhereParameters>
         <asp:ControlParameter Name="Name" ControlID="Name" Type="String" />
         <asp:ControlParameter Name="City" ControlID="City" Type="String" />
         <asp:ControlParameter Name="State" ControlID="State" Type="String" />
       </WhereParameters>            
     </asp:EntityDataSource>

     <asp:ListView ID="CustomerListView" runat="server" 
       DataSourceID="CustomerList">
       <ItemTemplate>
         <tr>
           <td><%# Eval("Name") %></td>
           <td><%# Eval("City") %></td>
           <td><%# Eval("State") %></td>
         </tr>
       </ItemTemplate>
       <LayoutTemplate>
         <table>
           <tr>
             <th>Name</th>
             <th>City</th>
             <th>State</th>
           </tr>
           <tr id="ItemPlaceHolder" runat="server" />
         </table>
       </LayoutTemplate>
     </asp:ListView>
    </div>
  </form>
</body>
</html>
