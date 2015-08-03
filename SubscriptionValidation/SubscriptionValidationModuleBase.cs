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

namespace Christoc.Modules.SubscriptionValidation
{
    public class SubscriptionValidationModuleBase : PortalModuleBase
    {
        public Hashtable TabModuleSettings { get { return base.ModuleConfiguration.TabModuleSettings; } }

        public string Referal
        {
            get
            {
                var qs = Request.QueryString["referal"];
                if (qs != null)
                    return qs;
                return string.Empty;
            }
        }

        public int ItemId
        {
            get
            {
                var qs = Request.QueryString["tid"];
                if (qs != null)
                    return Convert.ToInt32(qs);
                return -1;
            }

        }
    }
}