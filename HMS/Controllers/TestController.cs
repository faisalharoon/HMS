using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        HMS_DBEntity db = new HMS_DBEntity();

        public ActionResult TestCategory()
        {
            var model = new TestDAL().GetCategories().ToList();
            ViewData["GetTestCatory"] = model;
            return View();

        }
        public ActionResult AddTestCat(int? cat_id)
        {
            var model = new tblTestCategory();
            if (cat_id != null)
            {

                model = new TestDAL().GetTestCategory(Convert.ToInt32(cat_id));
            }
            return View(model);
        }

        
        [HttpPost]
        public ActionResult AddTestCat(int? cat_id, tblTestCategory obj)
        {
            string username = "";
            HttpCookie cookie = HttpContext.Request.Cookies["AdminCookies"];
            if (cookie != null)
            {
                username = Convert.ToString(cookie.Values["UserName"]);
            }

            try
            {
                // TODO: Add insert logic here
                if (cat_id != null)
                {
                    obj.ID = Convert.ToInt32(cat_id);
                    new TestDAL().UpdateTestCategory(obj);
                    TempData["AlertTask"] = "Test updated successfully";

                }
                else
                {
                    cat_id = new TestDAL().AddTestCategory(obj);
                    TempData["AlertTask"] = "Test added successfully";
                }

                return Redirect("/test-categories");
            }
            catch
            {
                return View();
            }
        }



        public ActionResult Delete(int id)
        {
            new TestDAL().DeleteCategory(id);
            TempData["AlertTask"] = "Category deleted successfully";
            return Redirect("/test-categories");

        }

        public ActionResult AddTest(int? test_id)
        {
            var model = new tblTest();
            var cat = new TestDAL().GetCategories().ToList();
            ViewBag.TestCat = cat;
            if (test_id != null)
            {

                model = new TestDAL().GetTest(Convert.ToInt32(test_id));
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AddTest(int? test_id, tblTest Obj)
        {
            string username = "";
            HttpCookie cookie = HttpContext.Request.Cookies["AdminCookies"];
            if (cookie != null)
            {
                username = Convert.ToString(cookie.Values["UserName"]);
            }

            try
            {
                // TODO: Add insert logic here
                if (test_id != null)
                {
                    Obj.ID = Convert.ToInt32(test_id);
                    new TestDAL().UpdateTest(Obj);
                    TempData["AlertTask"] = "Test updated successfully";

                }
                else
                {

                    test_id = new TestDAL().AddTest(Obj);
                    TempData["AlertTask"] = "Test added successfully";
                }

                return Redirect("/tests");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult TestList()
        {
            {
                var model = new TestDAL().GetAllTests().ToList();
                ViewData["GetTests"] = model;
                return View();
            }
        }
        public ActionResult DeleteTest(int id)
        {
            new TestDAL().DeleteTest(id);
            TempData["AlertTask"] = "test deleted successfully";
            return Redirect("/tests");

        }
    }
}
