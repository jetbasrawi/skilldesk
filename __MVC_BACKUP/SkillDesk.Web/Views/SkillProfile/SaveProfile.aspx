<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SkillDesk.Domain.Entities.UserProfile>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SaveProfile
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>SaveProfile</h2>
    
    <%: Html.ValidationSummary() %>

    <% using(Html.BeginForm()) { %>

        <h3>Your details</h3>
        <div>Name: <%: Html.EditorFor(x => x.Name) %></div>
        <div>Country: <%: Html.EditorFor(x => x.Country)%></div>
        <div>City: <%: Html.EditorFor(x => x.City) %></div>
    
        <h3>Options</h3>
        <div>Salary: <%: Html.EditorFor(x => x.Salary) %></div>  
        <div>Age: <%: Html.EditorFor(x => x.Age) %></div>
        
        <p align="center"><input type="submit" value="Save" /></p>

    <% } %>

</asp:Content>