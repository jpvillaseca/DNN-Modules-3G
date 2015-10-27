using System;
using DotNetNuke.Entities.Modules;

namespace Digevo.Digevo.Redirect
{
	public partial class index : PortalModuleBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!DotNetNuke.Security.Permissions.ModulePermissionController.CanEditModuleContent(this.ModuleConfiguration))
			{
				string config = "{";
				for(int i = 1; i <= 10; i++)
				{
					string country_code = (string)Settings["country" + i + "_code"];
					string country_url = (string)Settings["country" + i + "_url"];
					
					bool has_previous = (!String.IsNullOrWhiteSpace((string)Settings["country" + (i - 1) + "_code"]) && !String.IsNullOrWhiteSpace((string)Settings["country" + (i - 1) + "_url"])) ? true : false;
					bool has_next = (!String.IsNullOrWhiteSpace((string)Settings["country" + (i + 1) + "_code"]) && !String.IsNullOrWhiteSpace((string)Settings["country" + (i + 1) + "_url"])) ? true : false;
					
					if(!String.IsNullOrWhiteSpace(country_code) && !String.IsNullOrWhiteSpace(country_url)) 
					{ 
						if(!has_previous && i != 1) { config += ", "; }
						config += "\"" + country_code + "\" : " + "\"" + country_url + "\"";
						if(has_next) { config += ", "; }
					}
				}
				config += "}";
				Session["redirect_config"] = config;
			}
		}
		
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			cmdSave.Click += cmdSave_Click;
			cmdCancel.Click += cmdCancel_Click;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			
			if (!Page.IsPostBack)
			{
				 country1_code.Text = (string)Settings["country1_code"];
				 country1_url.Text = (string)Settings["country1_url"];
				 
				 country2_code.Text = (string)Settings["country2_code"];
				 country2_url.Text = (string)Settings["country2_url"];
				 
				 country3_code.Text = (string)Settings["country3_code"];
				 country3_url.Text = (string)Settings["country3_url"];
				 
				 country4_code.Text = (string)Settings["country4_code"];
				 country4_url.Text = (string)Settings["country4_url"];
				 
				 country5_code.Text = (string)Settings["country5_code"];
				 country5_url.Text = (string)Settings["country5_url"];
				 
				 country6_code.Text = (string)Settings["country6_code"];
				 country6_url.Text = (string)Settings["country6_url"];
				 
				 country7_code.Text = (string)Settings["country7_code"];
				 country7_url.Text = (string)Settings["country7_url"];
				 
				 country8_code.Text = (string)Settings["country8_code"];
				 country8_url.Text = (string)Settings["country8_url"];
				 
				 country9_code.Text = (string)Settings["country9_code"];
				 country9_url.Text = (string)Settings["country9_url"];
				 
				 country10_code.Text = (string)Settings["country10_code"];
				 country10_url.Text = (string)Settings["country10_url"];
			}
		}
		
		protected void cmdSave_Click(object sender, EventArgs e)
		{
		
            ModuleController.Instance.UpdateTabModuleSetting(TabModuleId, "country1_code", country1_code.Text);
			ModuleController.Instance.UpdateTabModuleSetting(TabModuleId, "country1_url", country1_url.Text);
			
			ModuleController.Instance.UpdateTabModuleSetting(TabModuleId, "country2_code", country2_code.Text);
			ModuleController.Instance.UpdateTabModuleSetting(TabModuleId, "country2_url", country2_url.Text);
			
			ModuleController.Instance.UpdateTabModuleSetting(TabModuleId, "country3_code", country3_code.Text);
			ModuleController.Instance.UpdateTabModuleSetting(TabModuleId, "country3_url", country3_url.Text);
			
			ModuleController.Instance.UpdateTabModuleSetting(TabModuleId, "country4_code", country4_code.Text);
			ModuleController.Instance.UpdateTabModuleSetting(TabModuleId, "country4_url", country4_url.Text);
			
			ModuleController.Instance.UpdateTabModuleSetting(TabModuleId, "country5_code", country5_code.Text);
			ModuleController.Instance.UpdateTabModuleSetting(TabModuleId, "country5_url", country5_url.Text);
			
			ModuleController.Instance.UpdateTabModuleSetting(TabModuleId, "country6_code", country6_code.Text);
			ModuleController.Instance.UpdateTabModuleSetting(TabModuleId, "country6_url", country6_url.Text);
			
			ModuleController.Instance.UpdateTabModuleSetting(TabModuleId, "country7_code", country7_code.Text);
			ModuleController.Instance.UpdateTabModuleSetting(TabModuleId, "country7_url", country7_url.Text);
			
			ModuleController.Instance.UpdateTabModuleSetting(TabModuleId, "country8_code", country8_code.Text);
			ModuleController.Instance.UpdateTabModuleSetting(TabModuleId, "country8_url", country8_url.Text);
			
			ModuleController.Instance.UpdateTabModuleSetting(TabModuleId, "country9_code", country9_code.Text);
			ModuleController.Instance.UpdateTabModuleSetting(TabModuleId, "country9_url", country9_url.Text);
			
			ModuleController.Instance.UpdateTabModuleSetting(TabModuleId, "country10_code", country10_code.Text);
			ModuleController.Instance.UpdateTabModuleSetting(TabModuleId, "country10_url", country10_url.Text);
			
            DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Actualizado correctamente.", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.GreenSuccess);
		}

		protected void cmdCancel_Click(object sender, EventArgs e)
		{
		}

	}
}

