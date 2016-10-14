<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Recipe8.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Entity Framework Recipes - Recipe 8</title>
</head>
<body>
  <form id="form1" runat="server">
    <div>
      <asp:ListView ID="reservationList" runat="server" DataSourceId="reservationSource" DataKeyNames="ReservationId,TimeStamp" InsertItemPosition="LastItem">
        <EditItemTemplate>
          <tr>
            <td>
              <asp:Button runat="server" CommandName="Update" Text="Update" />
              <asp:Button runat="server" CommandName="Cancel" Text="Cancel" />
            </td>
            <td>
              <asp:TextBox ID="nameTextBox" runat="server" Text='<%# Bind("Name") %>' />
            </td>
            <td>
              <asp:DropDownList ID="hotel" runat="server" AppendDataBoundItems="true" SelectedValue = '<%# Bind("HotelId") %>' DataSourceID="HotelSource" DataTextField="Name" DataValueField="HotelId">
                <asp:ListItem Text="Select" Value="" />
              </asp:DropDownList>
              <asp:ObjectDataSource ID="hotelSource" runat="server" TypeName="Recipe8.HotelRepository" SelectMethod="GetHotels" />
            </td>
            <td>
              <asp:TextBox ID="ResDateTextBox" runat="server" Text='<%# Bind("ReservationDate") %>' />
            </td>
            <td>
              <asp:TextBox ID="RateTextBox" runat="server" Text='<%# Bind("Rate") %>' />
            </td>
          </tr>
        </EditItemTemplate>
        <InsertItemTemplate>
          <tr>
            <td>
              <asp:Button runat="server" CommandName="Insert" Text="Insert" />
              <asp:Button runat="server" CommandName="Cancel" Text="Cancel" />
            </td>
            <td>
              <asp:TextBox ID="nameTextBox" runat="server" Text='<%# Bind("Name") %>' />
            </td>
            <td>
              <asp:DropDownList ID="hotel" runat="server" AppendDataBoundItems="true" SelectedValue='<%# Bind("HotelId") %>' DataSourceID="hotelSource" DataTextField="Name" DataValueField="HotelId">
                <asp:ListItem Text="Select" Value="" />
              </asp:DropDownList>
              <asp:ObjectDataSource ID="hotelSource" runat="server" TypeName="Recipe8.HotelRepository" SelectMethod="GetHotels" />
            </td>
            <td>
              <asp:TextBox ID="ResDateTextBox" runat="server" Text='<%# Bind("ReservationDate") %>' />
            </td>
            <td>
              <asp:TextBox ID="RateTextBox" runat="server" Text='<%# Bind("Rate") %>' />
            </td>
          </tr>
        </InsertItemTemplate>
        <ItemTemplate>
          <tr>
            <td>
              <asp:Button runat="server" CommandName="Delete" Text="Delete" />
              <asp:Button runat="server" CommandName="Edit" Text="Edit" />
            </td>
            <td><%# Eval("Name") %></td>
            <td><%# Eval("Hotel.Name") %></td>
            <td><%# Eval("ReservationDate") %></td>
            <td><%# Eval("Rate") %></td>
          </tr>        
        </ItemTemplate>
        <LayoutTemplate>
          <table>
            <tr>
              <th></th>
              <th>
                <asp:LinkButton runat="server" CommandName="Sort" CommandArgument="Name" Text="Name" />
              </th>
              <th>
                <asp:LinkButton runat="server" CommandName="Sort" CommandArgument="Hotel.Name" Text="Hotel" />
              </th>
              <th>
                <asp:LinkButton runat="server" CommandName="Sort" CommandArgument="ReservationDate" Text="Reservation Date" />
              </th>
              <th>
                <asp:LinkButton runat="server" CommandName="Sort" CommandArgument="Rate" Text="Daily Rate" />
              </th>
            </tr>
            <tr ID="itemPlaceholder" runat="server" />
          </table>
        </LayoutTemplate>
      </asp:ListView>
      <asp:DataPager ID="pager" runat="server" PagedControlID="reservationList" PageSize="2">
        <Fields>
          <asp:NumericPagerField />
        </Fields>
      </asp:DataPager>
      <asp:ObjectDataSource ID="reservationSource" runat="server" DataObjectTypeName="Recipe8.Reservation" 
          DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetReservations" UpdateMethod="Update" 
          EnablePaging="true" SortParameterName="sort" SelectCountMethod="ReservationCount" TypeName="Recipe8.ReservationRepository" />
    </div>
  </form>
</body>
</html>
