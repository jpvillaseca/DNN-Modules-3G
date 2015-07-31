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
using Christoc.Modules.LandingSubscription.Components;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;

namespace Christoc.Modules.LandingSubscription
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from LandingSubscriptionModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : LandingSubscriptionModuleBase
    {
        public string SelectedCountries { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            { 
                try
                {
                    var moduleController = new ModuleController();

                    lblJumbotronTitle.Text = (TabModuleSettings[SettingNames.JumbotronTitle] ?? LocalizeString("JumbotronDefaultTitle")).ToString();

                    litJumbotronContent.Text = moduleController.ReadLargeTabModuleSetting(TabModuleSettings, TabModuleId, SettingNames.JumbotronContent) ?? string.Empty;

                    btnSubmitPhone.Text = (TabModuleSettings[SettingNames.SubmitPhoneButton] ?? LocalizeString("SubmitForm")).ToString();

                    SelectedCountries = (TabModuleSettings[SettingNames.SelectedCountries] ?? SettingNames.DefaultCountries).ToString();

                }
                catch (Exception exc) //Module failed to load
                {
                    Exceptions.ProcessModuleLoadException(this, exc);
                }
            }
        }

        protected void UpdateForm_Click(object sender, EventArgs e)
        {
            var mobile = Request["mobilenumber"];

            if(!string.IsNullOrWhiteSpace(this.UserInfo.Username))
                this.UserInfo.Profile.SetProfileProperty("Telephone", mobile);

        }

    }
}