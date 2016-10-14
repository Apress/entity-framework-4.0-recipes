<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AltProducts.aspx.cs" Inherits="Recipe7.AltProducts" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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

      <asp:EntityDataSource ID="itemSource" runat="server" EntitySetName="Items" Include="ItemCategory" ConnectionString="name=EFRecipesEntities" DefaultContainerName="EFRecipesEntities" OnQueryCreated="ProdFilter" />
    </div>
  </form>
</body>
</html>
