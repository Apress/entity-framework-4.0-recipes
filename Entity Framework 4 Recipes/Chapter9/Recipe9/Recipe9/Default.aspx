<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Recipe9.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Entity Framework Recipes - Recipe 9</title>
</head>
<body>
  <form id="form1" runat="server">
    <div>
      <table>
        <tr>
          <td>New Job Title:</td>
          <td><asp:TextBox ID="JobTitle" runat="server" /></td>
        </tr>
        <tr>
          <td>New Salary:</td>
          <td><asp:TextBox ID="Salary" runat="server" /></td>
        </tr>
      </table>
      <br />
      <asp:Button ID="create" runat="server" OnClick="Create_Click" Text="Create Job" /> &nbsp;
      <asp:Button ID="update" runat="server" OnClick="Update_Click" Text="Update Job" />

      <table>
        <tr>
          <td>Job Title:</td>
          <td><asp:Label ID="JobTitleLabel" runat="server" /></td>
        </tr>
        <tr>
          <td>Salary:</td>
          <td><asp:Label ID="SalaryLabel" runat="server" /></td>
        </tr>
      </table>

    </div>
  </form>
</body>
</html>
