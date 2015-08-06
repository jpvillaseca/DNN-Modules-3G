<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Christoc.Modules.ViralEngineAdapter.View" %>


<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<script>
    var viralUser = ''; 
    var viralCoreEngineService = ''; 
    var viralCampaign = ''; 
    var viralEngineEnabled = false;

    function viralConfig(user, service, campaign, enabled) {
        viralUser = user;
        viralCoreEngineService = service;
        viralCampaign = campaign;
        viralEngineEnabled = enabled;
    }

    function sharePopup(url) {
        var winWidth = 520; var winHeight = 350;
        var winTop = (screen.height / 2) - (winHeight / 2);
        var winLeft = (screen.width / 2) - (winWidth / 2);
        window.open(url, 'sharer', 'top=' + winTop + ',left=' + winLeft + ',toolbar=0,status=0,width=' + winWidth + ',height=' + winHeight);
    }

    function getInviteText() {
        return encodeURIComponent(window.document.title);
    }

    function getInviteUrl() {
        return encodeURIComponent(window.location.href);
    }

    function getDescription() {
        return encodeURIComponent("Compartir");
    }

    function getFacebookViralUrl() {
        return getViralUrl("Facebook", "https://www.facebook.com/sharer/sharer.php?u={shareurl}", getInviteUrl());
    }

    function getTwitterViralUrl() {
        return getViralUrl("Twitter", "https://twitter.com/intent/tweet?text=" + getInviteText() + "&url={shareurl}", getInviteUrl());
    }

    function getWhatsAppViralUrl() {
        return getViralUrl("WhatsApp", "whatsapp://send?text=" + getInviteText() + " " + "{shareurl}", getInviteUrl());
    }

    function getViralUrl(medium, url, shareUrl) {
        if (viralEngineEnabled)
            return encodeURIComponent(viralCoreEngineService.replace('{webpage}', encodeURIComponent(url)).replace('{user}', viralUser).replace('{medium}', medium).replace('{campaign}', viralCampaign).replace('{shareurl}', shareUrl));
        else
            return url.replace('{shareurl}', shareUrl);
    }

    $(document).ready(function () {
        $(".share.facebook").attr("href", "javascript:sharePopup('" + getFacebookViralUrl() + "')");

        $(".share.twitter").attr("href", "javascript:sharePopup('" + getTwitterViralUrl() + "')");

        $(".share.whatsapp").attr("href", "javascript:sharePopup('" + getWhatsAppViralUrl() + "')").attr("data-action", "share/whatsapp/share");
    });

</script>
<style>.share{background-size:60px;display:inline-block;min-width:32px;min-height:32px;text-align:center;vertical-align:middle;padding:4px}.share>i, .share>em{color:#fff}.facebook{background-color:#305891}.facebook>i, .facebook>em{padding-left:10px}.twitter{background-color:#2ca8d2}.whatsapp{background-color:#29a628}</style>
  
<div class="dnnForm dnnEdit dnnClear" id="dnnEdit" runat="server">
    <div id="notSavedWarning" class="dnnFormMessage dnnFormWarning" runat="server">Debes guardar la configuraci&oacute;n del m&oacute;dulo antes de poder usarlo.</div>

    <fieldset>
        <div class="dnnFormItem">
            <dnn:label id="Campaign" runat="server" text="Field" helptext="Enter a value" controlname="txtCampaign" />
            <asp:textbox id="txtCampaign" runat="server" maxlength="255" />
        </div>
      
        <div class="dnnFormItem">
            <dnn:label id="ViralCoreAddress" runat="server" text="Field" helptext="Enter a value" controlname="txtViralCoreAddress" />
            <asp:textbox id="txtViralCoreAddress" runat="server" maxlength="255" />
        </div>
      
        <div class="dnnFormItem">
            <dnn:label id="ViralCoreEnabled" runat="server" text="Field" helptext="Enter a value" controlname="chkViralCoreEnabled" />
            <asp:CheckBox runat="server" ID="chkViralCoreEnabled" />
        </div>

   </fieldset>

    <ul class="dnnActions dnnClear">

        <li><asp:linkbutton id="cmdSave" text="Save" runat="server" cssclass="dnnPrimaryAction" OnClick="cmdSave_Click" /></li>

        <!--<li><asp:linkbutton id="cmdCancel" text="Cancel" runat="server" cssclass="dnnSecondaryAction"  /></li>-->

    </ul>

</div>