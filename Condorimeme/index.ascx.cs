using System;
using DotNetNuke.Entities.Modules;
using System.IO;
using System.Linq;
using System.Web.UI.HtmlControls;

namespace Digevo.Digevo.Condorimeme
{
	public partial class index : PortalModuleBase
	{
		protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request["id"];
            bool hasId = (String.IsNullOrWhiteSpace(id)) ? false : true;
			
			string img64 = Request.Form["img64"];
			bool hasImg64 = (String.IsNullOrWhiteSpace(img64)) ? false : true;
			
			string showFullscreen = Request["showFullscreen"];
			bool isFullscreen = (showFullscreen == null) ? false : true;

			Session["hasId"] = hasId;
			Session["memeExists"] = false;
			Session["memeId"] = String.Empty;
			Session["memePath"] = String.Empty;
			
			Session["showFullscreen"] = isFullscreen;
			
			HtmlMeta site_name = new HtmlMeta();
			site_name.Attributes.Add("property", "og:site_name");
			site_name.Content = "Condorimemes";
			Page.Header.Controls.Add(site_name);

			HtmlMeta description = new HtmlMeta();
			description.Attributes.Add("property", "og:description");
			description.Content = "¡Crea divertidos memes de Condorito y compártelo con tus amigos!";
			Page.Header.Controls.Add(description);
				
            if(hasId)
            {
                string path = "/DesktopModules/Digevo/Digevo.Condorimeme/img/memes/" + id + ".jpg";
                try
                {
                    bool fileExists = (File.Exists(Server.MapPath(path))) ? true : false;
                    if (fileExists)
                    {
                        Session["memeExists"] = true;
                        Session["memeId"] = id;
                        Session["memePath"] = path;
						
						HtmlMeta title = new HtmlMeta();
						title.Attributes.Add("property", "og:title");
						title.Content = "Condorimeme - " + id;
						Page.Header.Controls.Add(title);
						
						HtmlMeta type = new HtmlMeta();
						type.Attributes.Add("property", "og:type");
						type.Content = "website";
						Page.Header.Controls.Add(type);
						
						HtmlMeta url = new HtmlMeta();
						url.Attributes.Add("property", "og:url");
						url.Content = Request.Url.AbsoluteUri;
						Page.Header.Controls.Add(url);
						
						HtmlMeta image = new HtmlMeta();
						image.Attributes.Add("property", "og:image");
						image.Content = "http://" + Request.Url.Host + path ;
						Page.Header.Controls.Add(image);
                    }
                }
                catch (Exception)
                {
                    throw new NullReferenceException("Directorio memes no válido.");
                }
            }
			
			if(hasImg64)
			{
				if(img64.Length < 40)
				{
					Response.StatusCode = 417;
					Response.End();
				}
				else
				{
					string now = DateTime.Now.ToString("yyyyMMddHHmmss_fffffff");
					string target = Server.MapPath("/DesktopModules/Digevo/Digevo.Condorimeme/img/memes/" + now + ".jpg");
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

					string json = "{\"name\":\"" + now + "\",\"file\":\"" + now + ".jpg\",\"path\":\"/DesktopModules/Digevo/Digevo.Condorimeme/img/memes/" + now + ".jpg\"}";

					Response.Clear();
					Response.ContentType = "application/json; charset=utf-8";
					Response.Write(json);
					Response.End();
				}
			}
            
            try
            {
                string[] images = Directory.GetFiles(Server.MapPath("~/DesktopModules/Digevo/Digevo.Condorimeme/img/src"), "*.jpg").Select(f => Path.GetFileNameWithoutExtension(f)).ToArray();
                Session["images"] = images;
            }
            catch (Exception)
            {
                throw new NullReferenceException("Directorio source no válido.");
            }
            
            try
            {
                string[] history = Directory.GetFiles(Server.MapPath("~/DesktopModules/Digevo/Digevo.Condorimeme/img/memes"), "*.jpg").Select(f => Path.GetFileNameWithoutExtension(f)).OrderByDescending(o => o).Take(20).ToArray();
                Session["history"] = history;
            }
            catch (Exception)
            {
                throw new NullReferenceException("Directorio memes no válido.");
            }
        }
	}
}