using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HMS.Models;

namespace HMS.Controllers
{
    public class LoginController : Controller
    {
        HMS_DBEntities db = new HMS_DBEntities();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User objuser)
        {
            string redirect = "Login";
           
            User user = db.Users.Where(x => x.UserName == objuser.UserName && x.UserPassword == objuser.UserPassword).FirstOrDefault();
            if (user != null)
            {
                new GernalFunction().SetCookie(user);
                TempData["Msg"] = "Login Successfully.";
                redirect = "/";
                UserLoginHistory loginHistory = new UserLoginHistory();
                HttpRequest req = System.Web.HttpContext.Current.Request;
string browserName = req.Browser.Browser;
                loginHistory.BrowserType = browserName;
                loginHistory.CurrentUser = user.UserName;
                loginHistory.HostName= Dns.GetHostName();
                loginHistory.IsFailedAttempt = false;
                loginHistory.LoginDate = DateTime.UtcNow;

                IPHostEntry ipEntry = Dns.GetHostEntry(loginHistory.HostName);

                IPAddress[] addr = ipEntry.AddressList;

                loginHistory.LoginIP = addr[addr.Length - 1].ToString();
                loginHistory.PasswordUsed = objuser.UserPassword;
                loginHistory.PlateForm = req.Browser.Platform;
                loginHistory.UserId = user.ID;
                db.UserLoginHistories.Add(loginHistory);
                db.SaveChanges();
               
            }
            else
            {
                TempData["Msg"] = "User or Password incorrect.";
                redirect = "/Login";
                UserLoginHistory loginHistory = new UserLoginHistory();
                HttpRequest req = System.Web.HttpContext.Current.Request;
                string browserName = req.Browser.Browser;
                loginHistory.BrowserType = browserName;
                loginHistory.CurrentUser = objuser.UserName;
                loginHistory.HostName = Dns.GetHostName();
                loginHistory.IsFailedAttempt = true;
                loginHistory.LoginDate = DateTime.UtcNow;

                IPHostEntry ipEntry = Dns.GetHostEntry(loginHistory.HostName);

                IPAddress[] addr = ipEntry.AddressList;

                loginHistory.LoginIP = addr[addr.Length - 1].ToString();
                loginHistory.PasswordUsed = objuser.UserPassword;
                loginHistory.PlateForm = req.Browser.Platform;
                loginHistory.UserId = 1;
                db.UserLoginHistories.Add(loginHistory);
                db.SaveChanges();
            }
            return Redirect(redirect);

        }
        public ActionResult logout()
        {

            if (Request.Cookies["AdminCookies"] != null)
            {
                var c = new HttpCookie("AdminCookies");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            return RedirectToAction("Login");
        }
        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Register()
        {
            var listHospital = db.tblHospitals.ToList();
            ViewBag.ExemploList = listHospital;
            var Roles = db.UserRoles.ToList();

            ViewBag.ListRole = Roles;
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Register(User objuser)
        {
            try
            {
            

                string chkSupper = Convert.ToString(Request.Form["chkSupper"]);
                if (chkSupper == "on")
                    objuser.IsSuperUser = true;
                else
                    objuser.IsSuperUser = false;
                objuser.IsActive = true;
                objuser.IsClosed = false;
                objuser.LoginFailedAttempts =0;
                objuser.LastLoginDate = DateTime.UtcNow;
               string strHostName = Dns.GetHostName();

                IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);

                IPAddress[] addr = ipEntry.AddressList;

                objuser.LastLoginIP= addr[addr.Length - 1].ToString();
                objuser.TotalLogins = 0;
                HttpRequest req = System.Web.HttpContext.Current.Request;
                objuser.PlateForm = req.Browser.Platform;

                // TODO: Add insert logic here

                db.Users.Add(objuser);
                db.SaveChanges();
                long userID = objuser.ID;
                UserInRole userRole = new UserInRole();
                userRole.IsActive = true;
                userRole.UserID = userID;
                userRole.UserRoleID = Convert.ToInt32(Request.Form["UserInRoles"]);
                userRole.EffectiveFromDate = DateTime.UtcNow;
                db.UserInRoles.Add(userRole);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            catch(Exception ex)
            {
                string a = ex.ToString();
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
       
        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
