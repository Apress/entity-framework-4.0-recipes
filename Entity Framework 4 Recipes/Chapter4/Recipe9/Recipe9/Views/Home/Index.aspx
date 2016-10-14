<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Recipe9.Models.Movie>>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Index</title>
</head>
<body>
    <table>
        <tr>
            <th></th>
            <th></th>
            <th>
                MovieId
            </th>
            <th>
                Title
            </th>
            <th>
                Director
            </th>
            <th>
                Budget
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.MovieId }) %> |
                <%= Html.ActionLink("Details", "Details", new { id=item.MovieId })%>
            </td>
            <td>
                <% using (Html.BeginForm("Delete", "Home", new { id = item.MovieId }))
                   { %> <input type="submit" value="Delete" /> <% } %>
            </td>
            <td>
                <%= Html.Encode(item.MovieId) %>
            </td>
            <td>
                <%= Html.Encode(item.Title) %>
            </td>
            <td>
                <%= Html.Encode(item.Director) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.Budget)) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>

</body>
</html>

