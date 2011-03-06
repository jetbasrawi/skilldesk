<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<SkillDesk.Domain.Entities.Skill>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <h1>Edit <%: Model.Name %></h1>
    <% Html.EnableClientValidation(); %>
    
    <% using(Html.BeginForm("Edit", "Admin", FormMethod.Post, new { enctype = "multipart/form-data" })) { %>
    
        <%: Html.Hidden("skillId", Model.Id) %>
        <%: Html.EditorForModel() %>
    
        <div class="editor-label">Image</div>
        <div class="editor-field">
    
        <% if (Model.ImageData == null) { %>
            None
        <% } else { %>
            <img width="100" src="<%: Url.Action("GetImage", "Admin", new { Model.Id }) %>" />
        <% } %>
        
        <div>Upload new image: <input type="file" name="Image" /></div>
        </div>
        <input type="submit" value="Save" />
        <%: Html.ActionLink("Cancel and return to List", "Index") %>
    
    <% } %>

    <script src="<%: Url.Content("~/Scripts/MicrosoftAjax.js")%>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/MicrosoftMvcValidation.js")%>" type="text/javascript"></script>

</asp:Content>

