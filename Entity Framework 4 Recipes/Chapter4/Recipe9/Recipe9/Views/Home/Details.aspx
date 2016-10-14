<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Recipe9.Models.Movie>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Details</title>
</head>
<body>
    <fieldset>
        <legend>Fields</legend>
        
        <div class="display-label">MovieId</div>
        <div class="display-field"><%= Html.Encode(Model.MovieId) %></div>
        
        <div class="display-label">Title</div>
        <div class="display-field"><%= Html.Encode(Model.Title) %></div>
        
        <div class="display-label">Director</div>
        <div class="display-field"><%= Html.Encode(Model.Director) %></div>
        
        <div class="display-label">Budget</div>
        <div class="display-field"><%= Html.Encode(String.Format("{0:F}", Model.Budget)) %></div>
        
    </fieldset>
    <p>

        <%=Html.ActionLink("Edit", "Edit", new { id=Model.MovieId }) %> |
        <%=Html.ActionLink("Back to List", "Index") %>
    </p>

</body>
</html>

