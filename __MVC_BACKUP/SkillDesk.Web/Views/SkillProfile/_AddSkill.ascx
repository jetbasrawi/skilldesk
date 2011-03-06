<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SkillDesk.Domain.Entities.Skill>" %>
    <% 
        Html.EnableClientValidation(); 
        using(Html.BeginForm("AddSkill", "SkillProfile", FormMethod.Post, new { id="addSkill", name="addSkill" })) { 
    %>
    
        <%: Html.Hidden("skillId", Model.Id) %>
        
       <%-- <%: Html.EditorForModel() %>--%>

        <%: Html.EditorFor(x=>x.Name) %>
        <%: Html.ValidationMessageFor(x=>x.Name) %>
        <br />

        <%: Html.EditorFor(x=>x.ShortDescription) %>    
        <br />

        <input type="submit" value="Save" />
    
    <% } %>    

    
    
    
          