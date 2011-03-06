<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SkillDesk.Web.Models.SkillUiItem>" %>
<div class="skill-list-item">
<% if(Model.Skill.ImageData != null) { %>
    <img src="<%: Url.Action("GetImage", "Admin", new { Model.Skill.Id }) %>" />
<% } %>
    <h3><%: Html.RouteLink(Model.Skill.Name, Model.Routes ) %></h3>
    <p><%: Model.Skill.ShortDescription %></p>
    <div class="skill-controls">
    <% using (Html.BeginForm("AddSkill", "SkillProfile")) {%>
        <%: Html.Hidden("id", Model.Skill.Id) %>
        <%: Html.Hidden("name", Model.Skill.Name) %>
        <%: Html.Hidden("returnUrl", Request.Url.PathAndQuery) %>
        <input type="submit" value="+" />
    <% } %>
    </div>
    
         
</div>