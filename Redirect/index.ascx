<%@ Control Language="C#" AutoEventWireup="true" Inherits="Digevo.Digevo.Redirect.index" CodeFile="index.ascx.cs" %>
<script type="text/javascript" src="/DesktopModules/Digevo/Digevo.Redirect/js/redirect.js"></script>
<link rel="stylesheet" type="text/css" href="/DesktopModules/Digevo/Digevo.Redirect/css/font-awesome.min.css" />
<link rel="stylesheet" type="text/css" href="/DesktopModules/Digevo/Digevo.Redirect/css/style.css" />
<% if (!DotNetNuke.Security.Permissions.ModulePermissionController.CanEditModuleContent(this.ModuleConfiguration)) { %>
		<% var redirect_config = (string)Session["redirect_config"]; %>
		<script type="text/javascript">
			init(<%=redirect_config%>);
		</script>
<% } %>

<% if (DotNetNuke.Security.Permissions.ModulePermissionController.CanEditModuleContent(this.ModuleConfiguration)) { %>
<div class="dnnForm dnnEdit dnnClear" id="dnnEdit" style="text-align: center">
    <fieldset>
        <div class="dnnFormItem" style="text-align: center">
            <asp:textbox id="country1_code" runat="server" maxlength="2" size="2" placeholder="COD" style="width: auto" />
            <asp:textbox id="country1_url" runat="server" maxlength="999" placeholder="URL de destino" />
        </div>		
		<div class="dnnFormItem" style="text-align: center">
            <asp:textbox id="country2_code" runat="server" maxlength="2" size="2" placeholder="COD" style="width: auto" />
            <asp:textbox id="country2_url" runat="server" maxlength="999" placeholder="URL de destino" />
        </div>		
		<div class="dnnFormItem" style="text-align: center">
            <asp:textbox id="country3_code" runat="server" maxlength="2" size="2" placeholder="COD" style="width: auto" />
            <asp:textbox id="country3_url" runat="server" maxlength="999" placeholder="URL de destino" />
        </div>
		<div class="rest_wrapper">
			<div class="dnnFormItem" style="text-align: center">
				<asp:textbox id="country4_code" runat="server" maxlength="2" size="2" placeholder="COD" style="width: auto" />
				<asp:textbox id="country4_url" runat="server" maxlength="999" placeholder="URL de destino" />
			</div>
			<div class="dnnFormItem" style="text-align: center">
				<asp:textbox id="country5_code" runat="server" maxlength="2" size="2" placeholder="COD" style="width: auto" />
				<asp:textbox id="country5_url" runat="server" maxlength="999" placeholder="URL de destino" />
			</div>
			<div class="dnnFormItem" style="text-align: center">
				<asp:textbox id="country6_code" runat="server" maxlength="2" size="2" placeholder="COD" style="width: auto" />
				<asp:textbox id="country6_url" runat="server" maxlength="999" placeholder="URL de destino" />
			</div>
			<div class="dnnFormItem" style="text-align: center">
				<asp:textbox id="country7_code" runat="server" maxlength="2" size="2" placeholder="COD" style="width: auto" />
				<asp:textbox id="country7_url" runat="server" maxlength="999" placeholder="URL de destino" />
			</div>
			<div class="dnnFormItem" style="text-align: center">
				<asp:textbox id="country8_code" runat="server" maxlength="2" size="2" placeholder="COD" style="width: auto" />
				<asp:textbox id="country8_url" runat="server" maxlength="999" placeholder="URL de destino" />
			</div>
			<div class="dnnFormItem" style="text-align: center">
				<asp:textbox id="country9_code" runat="server" maxlength="2" size="2" placeholder="COD" style="width: auto" />
				<asp:textbox id="country9_url" runat="server" maxlength="999" placeholder="URL de destino" />
			</div>
			<div class="dnnFormItem" style="text-align: center">
				<asp:textbox id="country10_code" runat="server" maxlength="2" size="2" placeholder="COD" style="width: auto" />
				<asp:textbox id="country10_url" runat="server" maxlength="999" placeholder="URL de destino" />
			</div>
		</div>
		<div class="rest_switch">
			<span class="fa fa-angle-double-down"></span>
		</div>
   </fieldset>
    <ul class="dnnActions dnnClear">
        <li><asp:linkbutton id="cmdSave" text="GUARDAR" runat="server" cssclass="dnnPrimaryAction" /></li>
        <!--<li><asp:linkbutton id="cmdCancel" text="CANCELAR" runat="server" cssclass="dnnSecondaryAction" /></li>-->
    </ul>
</div>
<% } %>
<!-- Redirecting -->
<div class="redirect_wrapper">
	<div class="redirect_container">
		<div class="redirect_section">
			<p>Hemos detectado que vienes desde <span id="redirect_country">######</span>. 
			Te llevaremos al portal de tu país en unos segundos o <a id="redirect_url" href="#">haz clic aquí</a>.</p>
		</div>
	</div>
</div>
