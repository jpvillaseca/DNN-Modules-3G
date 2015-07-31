<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="Christoc.Modules.LandingSubscription.Settings" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx"%>


<!-- uncomment the code below to start using the DNN Form pattern to create and update settings -->
  

<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>

<h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead">
    <a href="" class="dnnSectionExpanded"><%=LocalizeString("BasicSettings")%></a>
</h2>
<fieldset>
    <div class="dnnFormItem">
        <dnn:Label ID="lblJumbotronTitle" runat="server" /> 
        <asp:TextBox ID="txtJumbotronTitle" runat="server" />
    </div>
    <div class="dnnFormItem">
        <dnn:label ID="lblCallToActionPhone" runat="server" />
        <asp:TextBox ID="txtCallToActionPhone" runat="server" />
    </div>
    <div class="dnnFormItem">
        <dnn:label ID="lblJumbotronContent" runat="server" />
        <dnn:TextEditor ID="htmlEditor" runat="server" Height="400" Width="500"
            HtmlEncode="false" ChooseRender="false" DefaultMode="RICH" />
    </div>
</fieldset>


