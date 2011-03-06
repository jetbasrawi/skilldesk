<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SkillDesk.Domain.Entities.SkillProfile>" %>

<div id="skill-profile-summary">
    <span class="caption">
        <b>Your profile:</b>
            <%: Model.Count %> skills            
    </span>
        <a href="/SkillProfile/" >My Profile</a>
        <%: Html.ActionLink("Add Skill","AddSkill", "SkillProfile", null, new { name = "modal", @class = "modal"}) %>
</div>
