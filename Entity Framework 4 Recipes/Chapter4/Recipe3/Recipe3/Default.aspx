<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Recipe3.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Entity Framework Recipes - Recipe 3</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:DetailsView ID="detailsView" runat="server" AutoGenerateRows="false" DataSourceID="orderSource" DefaultMode="Insert">
        <Fields>
          <asp:BoundField DataField="Company" HeaderText="Company" />
          <asp:BoundField DataField="Amount" HeaderText="Amount" />
          <asp:CommandField ShowInsertButton="true" />
        </Fields>
      </asp:DetailsView>

      <asp:EntityDataSource ID="orderSource" runat="server" ConnectionString="name=EFRecipesEntities" 
           DefaultContainerName="EFRecipesEntities" 
           EnableInsert="true" EntitySetName="PurchaseOrders" ContextTypeName="Recipe3.EFRecipesEntities" />
    </div>
    </form>
</body>
</html>
