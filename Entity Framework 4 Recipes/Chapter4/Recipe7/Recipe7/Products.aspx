<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="Recipe7.Products" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Entity Framework Recipes - Recipe 7</title>
</head>
<body>
  <form id="form1" runat="server">
    <div>
      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataSourceID="itemSource">
        <Columns>
          <asp:BoundField DataField="Name" HeaderText="Product" />
          <asp:TemplateField HeaderText="Category">
            <ItemTemplate><%# Eval("ItemCategory.Name") %></ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>

      <asp:EntityDataSource ID="itemSource" runat="server" EntitySetName="Items" Include="ItemCategory" ConnectionString="name=EFRecipesEntities" DefaultContainerName="EFRecipesEntities" />
      <asp:QueryExtender ID="search" TargetControlID="itemSource" runat="server">
        <asp:PropertyExpression>
          <asp:RouteParameter Name="ItemCategory.Name" RouteKey="category" />
        </asp:PropertyExpression>
      </asp:QueryExtender>    
    </div>
  </form>
</body>
</html>
