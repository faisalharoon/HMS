using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;


namespace HMS.Models
{
    public class GernalFunction
    {

        public void SetCookie(User user)
        {
            HttpCookie AdminCookie = new HttpCookie("AdminCookies");
            AdminCookie.Values["UserName"] = user.UserName.ToString();
            AdminCookie.Values["Password"] = user.UserPassword.ToString();
            AdminCookie.Values["ID"] = user.ID.ToString();
            AdminCookie.Values["hospital_id"] = user.Hospital_ID.ToString();

            HttpContext.Current.Response.SetCookie(AdminCookie);
        }
        public void setSession(User user)
        {
            HttpContext.Current.Session["User"] = user;
           
        }
        public void deleteSession()
        {
            HttpContext.Current.Session["User"] = null;

        }
        public void CheckAdminLogin()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["AdminCookies"];
            if (cookie == null || String.IsNullOrEmpty(Convert.ToString(cookie)))
            {
                HttpContext.Current.Response.Redirect("/Login");
            }
            else if (cookie.Values["Password"] == "-")
            {
                string url = "/User/LockScreen?ID=" + cookie.Values["ID"].ToString();
                HttpContext.Current.Response.Redirect(url);

            }

      
        }
        public  string ImageCroping(string crop, string Filepath)
        {
            if (!String.IsNullOrEmpty(crop))
            {
                var fileName = "";

                var imageStr64 = crop;
                var imgCode = imageStr64.Split(',');

                var bytes = Convert.FromBase64String(imgCode[1]);

                using (var stream = new MemoryStream(bytes))
                {
                    using (var img = System.Drawing.Image.FromStream(stream))
                    {
                        var filename = "";
                        var fileext = "";

                        if (imgCode.Contains("png"))
                        {
                            filename = "img";
                            fileext = ".png";


                        }
                        else
                        {
                            filename = "img";
                            fileext = ".jpg";
                        }

                        string fileNameNew = filename;
                        fileNameNew = RemoveSpecialCharacters(fileNameNew);


                        var path = HttpContext.Current.Server.MapPath(Filepath);

                        var ext = Path.GetExtension(filename);

                        var i = 0;
                        while (File.Exists(path + fileNameNew + fileext))
                        {
                            i++;
                            fileNameNew = Path.GetFileNameWithoutExtension(filename);
                            fileNameNew += i;
                        }


                        fileName = fileNameNew + fileext;
                        var filePath = HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + Filepath + fileName);
                        File.WriteAllBytes(filePath, bytes);
                    }

                }

                return fileName;
            }
            else
            {
                return null;
            }
        }
        public static string RemoveSpecialCharacters(string input)
        {
            Regex r = new Regex("(?:[^a-zA-Z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(input, String.Empty);
        }
        public string Resize_MedImage(string filePath, string srcImgName, string newFilePath)

        {  System.Drawing.Image img;
            img = System.Drawing.Image.FromFile(filePath + srcImgName);


            int maxWidth = img.Width;
            int maxHeight = img.Height;
            Double xRatio = (double)img.Width / maxWidth;
            Double yRatio = (double)img.Height / maxHeight;
            Double ratio = Math.Max(xRatio, yRatio);
            int nnx = (int)Math.Floor(img.Width / ratio);
            int nny = (int)Math.Floor(img.Height / ratio);
           
            Double nPercentW = (Convert.ToDouble(nnx) / Convert.ToDouble(img.Width));
            Double nPercentH = (Convert.ToDouble(nny) / Convert.ToDouble(img.Height));
            Double nPercent;
            int destY = 0, destX = 0;
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentW;
                destY = Convert.ToInt32((nny - (img.Height * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentH;
                destX = Convert.ToInt32((nnx - (img.Width * nPercent)) / 2);
            }
            Bitmap cpy = new Bitmap(nnx, nny, PixelFormat.Format32bppArgb);
            Graphics gr = Graphics.FromImage(cpy);
            gr.Clear(Color.Transparent);
            gr.InterpolationMode = InterpolationMode.HighQualityBicubic; // This is said to give best quality when resizing images
            gr.DrawImage(img,
                new Rectangle(0, 0, nnx, nny),
                new Rectangle(0, 0, img.Width, img.Height),
                GraphicsUnit.Pixel);
            gr.Dispose();
            img.Dispose();

            cpy.Save(newFilePath  + srcImgName);
            return srcImgName;
        }

    }
}