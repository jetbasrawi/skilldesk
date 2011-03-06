<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SkillDesk.Web.Models.LogOnViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	LogOn
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Log In</h1>
    <p>Please log in to access the administrative area:</p>
        <% Html.EnableClientValidation(); %>
        <% using(Html.BeginForm()) { %>
        <%: Html.ValidationSummary(true) %>
        <%: Html.EditorForModel() %>
        <p><input type="submit" value="Log in" /></p>
    <% } %>

</asp:Content>
