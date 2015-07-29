<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="Christoc.Modules.SubscriptionValidation.Settings" %>



<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>

<h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead"><a href="" class="dnnSectionExpanded"><%=LocalizeString("BasicSettings")%></a></h2>
<fieldset>
    <div class="dnnFormItem">
        <dnn:Label ID="lblRedirectAddress" runat="server" /> 
 
        <asp:TextBox ID="txtRedirectAddress" runat="server" />
    </div>
    <div class="dnnFormItem">
        <dnn:label ID="lblSubscriptionLists" runat="server" />
        <asp:TextBox ID="txtSubscriptionLists" runat="server" />
    </div>

</fieldset>


