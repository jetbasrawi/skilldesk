<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SkillDesk.Web.Models.SkillUiItem>" %>
Hello
<div class="item">
<% if(Model.Skill.ImageData != null) { %>
    <div style="float:left; margin-right:20px">
        <img width="32" src="<%: Url.Action("GetImage", "Admin", new { Model.Skill.Id }) %>" />
    </div>
<% } %>
    <%: Html.RouteLink(Model.Skill.Name, Model.Routes ) %>
    <%: Model.Skill.ShortDescription %>  

    <% using (Html.BeginForm("AddSkill", "SkillProfile")) {%>
        <%: Html.Hidden("skillId", Model.Skill.Id) %>
        <%: Html.Hidden("returnUrl", Request.Url.PathAndQuery) %>
        <input type="submit" value="+ add to profile" />
    <% } %>
</div>