<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SkillDesk.Web.Models.SkillProfileViewModelBase>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SkillDesk: Skill Profile
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <ul>
    <% foreach (var item in Model.SkillProfile.UserSkills) { %>
    
    <li>
        <%: item.Skill.Name + " (" + item.ExperienceType +")" %>
        <% using (Html.BeginForm("RemoveSkill", "SkillProfile")) { %>
           <%: Html.Hidden("skillId", item.Skill.Id) %>
           <%: Html.Hidden("returnUrl", Model.ReturnUrl) %>
           <input type="submit" value="Remove" />
        <% } %>
    </li>

    <% } %>
    </ul>

    <p align="center" class="actionButtons">
        <a href="<%: Model.ReturnUrl  %>">Back to all skills</a>
        <%: Html.ActionLink("Save profile", "SaveProfile") %>
    </p>

</asp:Content>
