<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Recipe9.Models.Movie>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Create</title>
</head>
<body>
    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.MovieId) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.MovieId) %>
                <%= Html.ValidationMessageFor(model => model.MovieId) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Title) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Title) %>
                <%= Html.ValidationMessageFor(model => model.Title) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Director) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Director) %>
                <%= Html.ValidationMessageFor(model => model.Director) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Budget) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Budget) %>
                <%= Html.ValidationMessageFor(model => model.Budget) %>
            </div>
            
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</body>
</html>

