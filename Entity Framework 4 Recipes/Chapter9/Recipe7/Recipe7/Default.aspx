<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Recipe7.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Entity Framework Recipes - Recipe 7</title>
</head>
<body>
  <form id="form1" runat="server">
    <div>
      <asp:Button ID="button1" Text="Create Customer" OnClick="CreateCustomer" runat="server" />
      <br />
      <asp:Button ID="button2" Text="Read Customer" OnClick="ReadCustomer" runat="server" />
      <br />
      <asp:Button ID="button3" Text="Update Customer" OnClick="UpdateCustomer" runat="server" />
      <br />
      <h2>Customer Details</h2>
      <table>
        <tr>
          <td>Customer Name</td>
          <td><asp:Label ID="CustomerName" runat="server" /></td>
        </tr>
        <tr>
          <td>Phone Number</td>
          <td><asp:Label ID="PhoneNumber" runat="server" /></td>
        </tr>
      </table>
    </div>
  </form>
</body>
</html>
