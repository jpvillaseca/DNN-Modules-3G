using System;
using System.IO;
using DotNetNuke.Entities.Modules;

namespace Digevo.Digevo.Condorimeme
{
	public partial class upload : PortalModuleBase
	{
		protected void Page_Load(object sender, EventArgs e)
        {
            string img64 = Request.Form["img64"];
            if (String.IsNullOrWhiteSpace(img64))
            {
                Response.StatusCode = 417;
                Response.End();
            }
            else
            {
                string now = DateTime.Now.ToString("yyyyMMddHHmmss_fffffff");
                string target = Server.MapPath("/img/memes/" + now + ".jpg");
                using (FileStream fs = new FileStream(target, FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        try
                        {
                            byte[] data = Convert.FromBase64String(img64);
                            bw.Write(data);
                            bw.Close();
                        }
                        catch (Exception)
                        {
                            Response.StatusCode = 406;
                            Response.End();
                        }
                    }
                }

                string json = "{\"name\":\"" + now + "\",\"file\":\"" + now + ".jpg\",\"path\":\"/img/memes/" + now + ".jpg\"}";

                Response.Clear();
                Response.ContentType = "application/json; charset=utf-8";
                Response.Write(json);
                Response.End();
            }
		}
	}
}

