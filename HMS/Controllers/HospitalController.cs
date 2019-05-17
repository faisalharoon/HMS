using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class HospitalController : Controller
    {
        // GET: Hospital
        public ActionResult ListOfHospitals()
        {
            var list = new HospitalDAL().ListOfRecords();
            ViewData["listOfData"] = list;
            return View();
        }

        // GET: Hospital/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Hospital/Create
        public ActionResult AddHospital(int? Hospital_id)
        {
            var model = new tblHospital();
            if (Hospital_id != null)
            {

                model = new HospitalDAL().SingleRecord(Convert.ToInt32(Hospital_id));
            }
            return View(model);
        }

        // POST: Hospital/Create
        [HttpPost]
        public ActionResult AddHospital(int? Hospital_id,tblHospital hospital)
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
                if (Hospital_id != null)
                {
                    hospital.ID = Convert.ToInt32(Hospital_id);
                    new HospitalDAL().UpdateRecord(hospital);
                    TempData["AlertTask"] = "Hospital updated successfully";

                }
                else
                {
                    hospital.CreatedBy = username;
                    hospital.CreatedAt = DateTime.UtcNow;
                    new HospitalDAL().InsertRecord(hospital);
                    TempData["AlertTask"] = "Hospital added successfully";
                    }

                return Redirect("/hospitals");
            }
            catch
            {
                return View();
            }
        }

        // GET: Hospital/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Hospital/Edit/5
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

        // GET: Hospital/Delete/5
        public ActionResult Delete(int id)
        {
            new HospitalDAL().Delete(id);
            TempData["AlertTask"] = "Hospital deleted successfully";
            return Redirect("/hospitals");
            
        }
    }
}
