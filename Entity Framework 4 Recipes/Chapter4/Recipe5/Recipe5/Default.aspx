<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Recipe5.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Entity Framework Recipes - Recipe 5</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table>
        <tr>
          <td>Name or Description</td>
          <td><asp:TextBox ID="ProductName" runat="server" /></td>
        </tr>
        <tr>
          <td>Discontinued</td>
          <td>
            <asp:DropDownList ID="Discontinued" runat="server">
              <asp:ListItem Text="All" Value="" />
              <asp:ListItem Text="Yes" Value="true" />
              <asp:ListItem Text="No" Value="false" />
            </asp:DropDownList>
          </td>
        </tr>
        <tr>
          <td>Category</td>
          <td><asp:TextBox ID="CategoryName" runat="server" /></td>
        </tr>
        <tr>
          <td>Units In Stock</td>
          <td><asp:TextBox ID="UnitsInStock" runat="server" /></td>
        </tr>
        <tr>
          <td>Price From</td>
          <td><asp:TextBox ID="FromPrice" runat="server" /> &nbsp;&nbsp;
              Price To <asp:TextBox ID="ToPrice" runat="server" />
          </td>
        </tr>
        <tr>
          <td>Supplier Country</td>
          <td><asp:TextBox ID="SupplierCountry" runat="server" /></td>
        </tr>
        <tr>
          <td>Total Sales</td>
          <td><asp:TextBox ID="TotalSales" runat="server" /></td>
        </tr>
        <tr>
          <td align="left" colspan="2" > <asp:Button ID="SearchButton" Text="Search" runat="server" /></td>
        </tr>
      </table>

      <asp:GridView ID="GridView1" runat="server" AllowPaging="true" PageSize="50" AutoGenerateColumns="false" DataSourceID="DataSource">
        <Columns>
          <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
          <asp:BoundField DataField="ProductDescription" HeaderText="Product Description" />
          <asp:CheckBoxField DataField="Discontinued" HeaderText="Discontinued" />
          <asp:TemplateField HeaderText="UnitPrice">
            <ItemTemplate><%# Eval("ProductDetail.UnitPrice","{0:C}") %></ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="CategoryName">
            <ItemTemplate><%# Eval("Category.CategoryName") %></ItemTemplate>
          </asp:TemplateField>
          <asp:BoundField DataField="UnitsInStock" HeaderText="Units In Stock" />
          <asp:TemplateField HeaderText="Supplier Country">
            <ItemTemplate><%# Eval("Supplier.Country") %></ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="Total Sales">
            <ItemTemplate><%# Eval("TotalSales","{0:C}") %></ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>

      <asp:EntityDataSource ID="DataSource" runat="server" ConnectionString="name=EFRecipesEntities" 
        Include="Category,ProductDetail,Supplier,OrderDetails" DefaultContainerName="EFRecipesEntities" 
        EnableFlattening="false" EntitySetName="Products" />
    
      <asp:QueryExtender ID="QueryExtender1" runat="server" TargetControlID="DataSource">
        <asp:SearchExpression SearchType="Contains" DataFields="ProductName,ProductDescription">
          <asp:ControlParameter ControlID="ProductName" />
        </asp:SearchExpression>
        <asp:OrderByExpression DataField="UnitsInStock" Direction="Descending">
          <asp:ThenBy DataField="ProductDetail.UnitPrice" Direction="Ascending" />
        </asp:OrderByExpression>
        <asp:PropertyExpression>
          <asp:ControlParameter Name="Discontinued" ControlID="Discontinued" />
          <asp:ControlParameter Name="UnitsInStock" ControlID="UnitsInStock" />
          <asp:ControlParameter Name="Supplier.Country" ControlID="SupplierCountry" />
        </asp:PropertyExpression>
        <asp:RangeExpression DataField="ProductDetail.UnitPrice" MinType="Inclusive" MaxType="Exclusive">
          <asp:ControlParameter ControlID="FromPrice" />
          <asp:ControlParameter ControlID="ToPrice" />
        </asp:RangeExpression>
        <asp:CustomExpression OnQuerying="ProductsWithCategory">
          <asp:ControlParameter Name="CategoryName" ControlID="CategoryName" />
        </asp:CustomExpression>
        <asp:MethodExpression MethodName="ProductWithSalesGreaterThan">
          <asp:ControlParameter Name="TotalSales" Type="Decimal" ControlID="TotalSales" />
        </asp:MethodExpression>         
      </asp:QueryExtender>
    </div>
    </form>
</body>
</html>
