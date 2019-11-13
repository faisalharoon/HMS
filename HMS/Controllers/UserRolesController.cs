using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HMS.Models;

namespace HMS.Controllers
{
    public class UserRolesController : Controller
    {
        private HMS_DBEntity db = new HMS_DBEntity();

        // GET: UserRoles
        public ActionResult Index()
        {
            return View(db.UserRoles.ToList());
        }

        // GET: UserRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = db.UserRoles.Find(id);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // GET: UserRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RoleName")] UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                db.UserRoles.Add(userRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userRole);
        }

        // GET: UserRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = db.UserRoles.Find(id);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // POST: UserRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RoleName")] UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userRole);
        }

        // GET: UserRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = db.UserRoles.Find(id);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // POST: UserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserRole userRole = db.UserRoles.Find(id);
            db.UserRoles.Remove(userRole);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult UserPages(int? ID)
        {
            var pages = db.tblPages.ToList();
            var UserRolePages = new List<tblUserRolePage>();
            ViewBag.pages = pages;
            string role_name = "";
            if(ID!=null && ID!=0)
            {
                var role = db.UserRoles.Where(x => x.ID == ID).FirstOrDefault();
                role_name = role.RoleName;
                UserRolePages = db.tblUserRolePages.Where(x => x.RoleId == ID).ToList();

            }
            ViewBag.role_name = role_name;
            ViewBag.UserRolePages = UserRolePages;
            return View();
        }
       [HttpPost]
        public ActionResult UserPages(int? ID, tblUserRolePage userpage)
        {
            try
            {
                List<tblUserRolePage> lst = db.tblUserRolePages.Where(x => x.RoleId == ID ).ToList();
                if (lst != null && lst.Count > 0)
                {
                    foreach (var item in lst)
                    {
                        new UserDAL().deletepages(item.RoleId);
                    }
                }
                string count = Request.Form["hdncount"];
                List<string> lstpage = new List<string>();
                string[] stringSeparators = new string[] { "," };
                lstpage = count.Split(stringSeparators, StringSplitOptions.None).ToList();
                for(int i=0; i<lstpage.Count;i++)
                {
                    string pageid = Request.Form["txtid_" + lstpage[i]];
                    userpage.PageId = Convert.ToInt32(pageid);
                    userpage.RoleId = Convert.ToInt32(ID);
                   
                    new UserDAL().insertRolePages(userpage);
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Redirect("/UserRolesPage");
        }
    }
}
