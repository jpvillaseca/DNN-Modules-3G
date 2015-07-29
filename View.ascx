<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Christoc.Modules.SubscriptionValidation.View" %>
<div class="dnnForm dnnEdit dnnClear" id="dnnView" runat="server">
    <div id="notSavedWarning" class="dnnFormMessage dnnFormWarning" runat="server"><%=LocalizeString("NotSavedWarning")%></div>
    <div id="redirectText" class ="dnnFormMessage dnnFormInformation" runat="server"><%=LocalizeString("RedirectTo")%><asp:Label ID="lblRedirect" runat="server" /></div>
</div>