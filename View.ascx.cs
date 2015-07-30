﻿/*
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
using Christoc.Modules.SubscriptionValidation.Services;
using System.Threading.Tasks;

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
                //Is administrator, or normal user?
                if (DotNetNuke.Security.Permissions.ModulePermissionController.CanEditModuleContent(this.ModuleConfiguration))
                {
                    dnnView.Visible = true;
                    notSavedWarning.Visible = !TabModuleSettings.ContainsKey(SettingNames.RedirectAddress);
                    lblRedirect.Text = TabModuleSettings.ContainsKey(SettingNames.RedirectAddress) ? TabModuleSettings[SettingNames.RedirectAddress].ToString() + (TabModuleSettings.ContainsKey(SettingNames.SubscriptionLists) ? " (" + TabModuleSettings[SettingNames.SubscriptionLists].ToString() : string.Empty) + ")" : string.Empty;
                }
                else
                {
                    dnnView.Visible = false;

                    if (!IsSubscriptionValidAsync().Result)
                        Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(int.Parse(TabModuleSettings[SettingNames.RedirectAddress].ToString())), true);
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        public async Task<bool> IsSubscriptionValidAsync()
        {
            //Don't check status if the module is not configured properly
            if (!TabModuleSettings.Contains(SettingNames.RedirectAddress) || !TabModuleSettings.Contains(SettingNames.SubscriptionLists))
                return true;

            try
            {
                var subscriptionModel = new SubscriptionModel(this.UserInfo.Profile.Telephone, SubscriptionModel.ParseLists(TabModuleSettings[SettingNames.SubscriptionLists].ToString()));
                
                if (string.IsNullOrWhiteSpace(this.UserInfo.Profile.Telephone))
                    return false;

                return await SubscriptionValidationService.ValidateUserSubscriptionAsync(subscriptionModel);

                    //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "El teléfono no tiene una suscripción válida", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.YellowWarning);


                    //this.UserInfo.Profile.SetProfileProperty("Telephone", "123");
                    //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "El teléfono está suscrito OK!", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.GreenSuccess);


            }catch(Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

            return false;
        }
    }
}