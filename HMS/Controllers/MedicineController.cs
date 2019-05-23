using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class MedicineController : Controller
    {
        HMS_DBEntity db = new HMS_DBEntity();

      public ActionResult MedicineCategory()
        {
            var model = new MedicineDAL().GetCategories().ToList();
              ViewData["GetMedicineCatory"] = model;
            return View();
           
        }
        public ActionResult AddMedicineCat(int? cat_id)
        {
            var model = new tblMedicineCategory();
            if (cat_id != null)
            {

                model = new MedicineDAL().SingleRecord(Convert.ToInt32(cat_id));
            }
            return View(model);
        }

        // GET: Medicine/Details/5


        // POST: Medicine/Create
        [HttpPost]
        public ActionResult AddMedicineCat(int? cat_id,tblMedicineCategory medicineCategory)
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
                    medicineCategory.ID = Convert.ToInt32(cat_id);
                    new MedicineDAL().UpdateRecord(medicineCategory);
                    TempData["AlertTask"] = "medicine updated successfully";

                }
                else
                {
                    medicineCategory.CreatedBy = username;
                    medicineCategory.isActive = true;
                    medicineCategory.CreatedAt = DateTime.UtcNow;
                   cat_id= new MedicineDAL().InsertRecord(medicineCategory);
                    TempData["AlertTask"] = "medicine added successfully";
                }

                return Redirect("/medicine-categories");
            }
            catch
            {
                return View();
            }
        }

        // GET: Medicine/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Medicine/Edit/5
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

        // GET: Medicine/Delete/5
        public ActionResult Delete(int id)
        {
            new MedicineDAL().DeleteCategory(id);
            TempData["AlertTask"] = "Category deleted successfully";
            return Redirect("/medicine-categories");

        }

        // POST: Medicine/Delete/5
  
    }
}
