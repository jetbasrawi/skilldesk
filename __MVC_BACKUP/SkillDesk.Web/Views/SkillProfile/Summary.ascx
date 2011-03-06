<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SkillDesk.Domain.Entities.SkillProfile>" %>

<div id="skill-profile-summary">
    <span class="caption">
        <b>Your profile:</b>
            <%: Model.Skills.Count %> skills            
    </span>
        <%: Html.ActionLink("View Profile", "ViewProfile", "SkillProfile", new { returnUrl = Request.Url.PathAndQuery }, null)%>
        <%: Html.ActionLink("Add Skill","AddSkill", "SkillProfile", new { name = "apmodal", @class = "modal"}) %>
</div>
