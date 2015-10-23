using System;
using DotNetNuke.Entities.Modules;

namespace Digevo.Digevo.showFullscreen
{
	public partial class index : PortalModuleBase
	{
        protected void Page_Load(object sender, EventArgs e)
        {
          string showFullscreen = Request["showFullscreen"];
          bool isFullscreen = (showFullscreen == null) ? false : true;
          
          Session["showFullscreen"] = isFullscreen;
        }
	}
}


