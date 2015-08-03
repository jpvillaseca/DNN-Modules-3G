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
using DotNetNuke.Entities.Modules;
using System.Collections;
using Christoc.Modules.LandingSubscription.Components;

namespace Christoc.Modules.LandingSubscription
{
    public class LandingSubscriptionModuleBase : PortalModuleBase
    {
        public string GetQueryStringParameter(string name)
        {
            var qs = Request.QueryString[name];
            return qs ?? string.Empty;
        }

        public string Referal
        {
            get
            {
                return Response.Cookies[SettingNames.ViralReferalCookie].Value ?? string.Empty;
            }
            set
            {
                Response.Cookies.Add(new System.Web.HttpCookie(SettingNames.ViralReferalCookie, value) { Expires = DateTime.Now.AddDays(15) });
            }
        }

        public string ViralToken
        {
            get
            {
                return Response.Cookies[SettingNames.ViralTokenCookie].Value ?? string.Empty;
            }
            set
            {
                Response.Cookies.Add(new System.Web.HttpCookie(SettingNames.ViralTokenCookie, value) { Expires = DateTime.Now.AddDays(15) });
            }
        }

        public Hashtable TabModuleSettings { get { return base.ModuleConfiguration.TabModuleSettings; } }
    }
}