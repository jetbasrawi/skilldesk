<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<SkillDesk.Domain.Entities.Skill>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>

    <table class="Grid">
        <tr>
            <th></th>
            <th>
                Id
            </th>
            <th>
                Name
            </th>
            
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { id=item.Id }) %> |
                <%: Html.ActionLink("Details", "Details", new { id=item.Id })%> |
                <%: Html.ActionLink("Delete", "Delete", new { id=item.Id })%>
            </td>
            <td>
                <%: item.Id %>
                <img width="32" src="<%: Url.Action("GetImage", "Admin", new { item.Id }) %>" />
            </td>
            <td>
                <%: item.Name %>
                                <%: item.ShortDescription %>
                <%: item.Category %>

            </td>
            <td>
            <% using (Html.BeginForm("Delete", "Admin")) { %>
            <%: Html.Hidden("skillId", item.Id) %>            
            <% } %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

