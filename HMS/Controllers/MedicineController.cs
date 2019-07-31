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
        HMS_DBEntities db = new HMS_DBEntities();

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
        public ActionResult AddMedicineCat(int? cat_id, tblMedicineCategory medicineCategory)
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
                    cat_id = new MedicineDAL().InsertRecord(medicineCategory);
                    TempData["AlertTask"] = "medicine added successfully";
                }

                return Redirect("/medicine-categories");
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

        public ActionResult AddMedicine(int? medicine_id)
        {
            var model = new tblMedicine();
            var cat = new MedicineDAL().GetCategories().ToList();
            ViewBag.MedicineCat = cat;
            if (medicine_id != null)
            {

                model = new MedicineDAL().GetMedicine(Convert.ToInt32(medicine_id));
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AddMedicine(int? medicine_id, tblMedicine Obj)
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
                if (medicine_id != null)
                {
                    Obj.ID = Convert.ToInt32(medicine_id);
                    new MedicineDAL().UpdateMedicine(Obj);
                    TempData["AlertTask"] = "medicine updated successfully";

                }
                else
                {
                    Obj.CreatedBy = username;
                    Obj.isActive = true;
                    Obj.CreatedAt = DateTime.UtcNow;
                    medicine_id = new MedicineDAL().AddMedicine(Obj);
                    TempData["AlertTask"] = "medicine added successfully";
                }

                return Redirect("/medicine");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult MedicineList()
        {
            {
                var model = new MedicineDAL().GetAllMedicine().ToList();
                ViewData["GetMedicine"] = model;
                return View();
            }
        }
        public ActionResult DeleteMedicine(int id)
        {
            new MedicineDAL().DeleteMedicine(id);
            TempData["AlertTask"] = "Medicine deleted successfully";
            return Redirect("/medicine");

        }
    }
}
