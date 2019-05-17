using System;
using System.Collections.Generic;
using System.Linq;
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
            HttpContext.Current.Response.SetCookie(AdminCookie);
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
    }
}