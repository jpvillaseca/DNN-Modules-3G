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
using System.Web.UI.WebControls;
using Christoc.Modules.SubscriptionValidation.Components;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;

namespace Christoc.Modules.SubscriptionValidation
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from SubscriptionValidationModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : SubscriptionValidationModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (DotNetNuke.Security.Permissions.ModulePermissionController.CanEditModuleContent(this.ModuleConfiguration))
                {
                    dnnView.Visible = true;
                    notSavedWarning.Visible = !Settings.ContainsKey(SettingNames.RedirectAddress);
                    lblRedirect.Text = Settings.ContainsKey(SettingNames.RedirectAddress) ? Settings[SettingNames.RedirectAddress].ToString() + (Settings.ContainsKey(SettingNames.SubscriptionLists) ? " (" + Settings[SettingNames.SubscriptionLists].ToString() : string.Empty) + ")" : string.Empty;
                }
                else if (Settings.ContainsKey(SettingNames.RedirectAddress))
                {
                    dnnView.Visible = false;

                    if (!IsSubscriptionValid())
                        Response.Redirect(Settings[SettingNames.RedirectAddress].ToString(), true);
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        public bool IsSubscriptionValid()
        {
            var subscriptionModel = new SubscriptionModel();
            subscriptionModel.SubscriptionLists = SubscriptionModel.ParseLists(Settings[SettingNames.SubscriptionLists].ToString());

            if (string.IsNullOrWhiteSpace(this.UserInfo.Profile.Telephone))
            {
                DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "El teléfono no es válido", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.YellowWarning);

                this.UserInfo.Profile.SetProfileProperty("Telephone", "123");
            }
            else
                DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "El teléfono está OK!", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.GreenSuccess);


            return true;

            return false;
        }
    }
}