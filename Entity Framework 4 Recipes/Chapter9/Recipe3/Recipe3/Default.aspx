<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Recipe3.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Entity Framework Recipes - Recipe 3</title>
</head>
<body>
  <form id="form1" runat="server">
    <div>
      <h2>Azle City Project</h2>
      <asp:DetailsView ID="DetailsView1" runat="server" DataKeyNames="ProjectId, TimeStamp" 
        AutoGenerateRows="false" DataSourceID="projectSource">
        <Fields>
          <asp:BoundField DataField="ProjectId" HeaderText="ProjectId" ReadOnly="true" />
          <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="true" />
          <asp:BoundField DataField="AmountAllocated" HeaderText="Amount Allocated" />
          <asp:BoundField DataField="PercentComplete" HeaderText="Percent Complete" />
          <asp:CommandField ShowEditButton="true" />
        </Fields>
      </asp:DetailsView>  

      <asp:ObjectDataSource ID="projectSource" runat="server" UpdateMethod="UpdateProject" 
        SelectMethod="RetrieveAll" TypeName="Recipe3.ProjectRepository" ConflictDetection="CompareAllValues" 
        DataObjectTypeName="Recipe3.Project" OldValuesParameterFormatString="original{0}" />
    </div>
    </form>
</body>
</html>
