using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class UserController : Controller
    {


        HMS_DBEntity db = new HMS_DBEntity();// GET: User
        public ActionResult UserList()
        {
            var currentUser=Session["User"] as User;

            var list = new UserDAL().GetAllUser().Where(x=>x.Hospital_ID==currentUser.Hospital_ID).ToList();
            ViewData["listOfData"] = list;
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult AddUser()
        {
            var listHospital = db.tblHospitals.ToList();
            ViewBag.ExemploList = listHospital;
            var Roles = db.UserRoles.ToList();

            ViewBag.ListRole = Roles;
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult AddUser(User objuser)
        {
            try
            {

                    objuser.IsSuperUser = false;
                objuser.IsActive = true;
                objuser.IsClosed = false;
                objuser.LoginFailedAttempts = 0;
                objuser.LastLoginDate = DateTime.UtcNow;
                string strHostName = Dns.GetHostName();

                IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);

                IPAddress[] addr = ipEntry.AddressList;

                objuser.LastLoginIP = addr[addr.Length - 1].ToString();
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
                userRole.ExpiryDate = DateTime.UtcNow.AddMonths(6);
               
                db.UserInRoles.Add(userRole);
                db.SaveChanges();
                //get pages according to role
                List<tblUserRolePage> LstPages = db.tblUserRolePages.Where(x => x.RoleId == userRole.UserRoleID).ToList();
                foreach (var item in LstPages)
                {
                    //ADD Pages according to RoleID and UserID

                    tblUserPage objUserPage = new tblUserPage();
                    objUserPage.page_id = item.PageId;
                    objUserPage.Role_id = userRole.UserRoleID;
                    objUserPage.show_in_menu = true;
                    objUserPage.user_id = Convert.ToInt32(userID);
                    db.tblUserPages.Add(objUserPage);
                    db.SaveChanges();
                }

                return Redirect("/user-list");
            }
            catch (Exception ex)
            {
                string a = ex.ToString();
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
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

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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
