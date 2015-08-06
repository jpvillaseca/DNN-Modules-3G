/*
' Copyright (c) 2015  Christoc.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using Christoc.Modules.ViralEngineAdapter.Components;

namespace Christoc.Modules.ViralEngineAdapter
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from ViralEngineAdapterModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : ViralEngineAdapterModuleBase
    {
        private static string viralEngineServiceDefault = "";
        private static string viralCampaignDefault = "1";
        private static bool viralCoreEnabledDefault = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (string.IsNullOrWhiteSpace((string)TabModuleSettings[SettingNames.ViralEngineEnabled]))
                    {
                        //Not configured, show warning
                        txtCampaign.Text = viralCampaignDefault;
                        txtViralCoreAddress.Text = viralEngineServiceDefault;
                        chkViralCoreEnabled.Checked = viralCoreEnabledDefault;
                        notSavedWarning.Visible = true;
                    }
                    else
                    {
                        txtCampaign.Text = (string)TabModuleSettings[SettingNames.ViralEngineCampaign];
                        txtViralCoreAddress.Text = (string)TabModuleSettings[SettingNames.ViralEngineAddress];
                        chkViralCoreEnabled.Checked = bool.Parse((string)TabModuleSettings[SettingNames.ViralEngineEnabled]);
                        notSavedWarning.Visible = false;

                        //Append js configuration
                        string script = string.Format("viralConfig('{0}', '{1}', '{2}', {3}); ", DotNetNuke.Entities.Users.UserController.Instance.GetCurrentUserInfo().Username, (string)TabModuleSettings[SettingNames.ViralEngineAddress], (string)TabModuleSettings[SettingNames.ViralEngineCampaign], ((string)TabModuleSettings[SettingNames.ViralEngineEnabled]).ToLower());
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "initViralEngine", script, true);
                    }

                    //Hide for non admin users
                    if (DotNetNuke.Security.Permissions.ModulePermissionController.CanEditModuleContent(this.ModuleConfiguration))
                        dnnEdit.Visible = true;
                    else
                        dnnEdit.Visible = false;
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            ModuleController.Instance.UpdateTabModuleSetting(TabModuleId, SettingNames.ViralEngineCampaign, txtCampaign.Text);
            ModuleController.Instance.UpdateTabModuleSetting(TabModuleId, SettingNames.ViralEngineAddress, txtViralCoreAddress.Text);
            ModuleController.Instance.UpdateTabModuleSetting(TabModuleId, SettingNames.ViralEngineEnabled, chkViralCoreEnabled.Checked.ToString());
            DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Update Successful", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.GreenSuccess);
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
        }
    }
}