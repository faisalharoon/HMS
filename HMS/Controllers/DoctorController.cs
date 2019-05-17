using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class DoctorController : Controller
    {
        HMS_DBEntity db = new HMS_DBEntity();
        // GET: Doctor
        public ActionResult Index()
        {
            return View();
        }

        // GET: Doctor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Doctor/Create
        public ActionResult AddDoctor(int? Doctor_id)
        {
            var lstQualification = db.tblEmployeeQualifications.Where(x=>x.employee_type==1).ToList();
            ViewBag.lstQualification = lstQualification;

            var status = db.tblEmployeeSatus.ToList();
            ViewBag.status = status;
            var designation = db.tblEmpDesignations.ToList();
            var model = new tblEmployee();
            if (Doctor_id != null)
            {
                model = new DoctorsDAL().SingleRecord(Convert.ToInt32(Doctor_id));
                designation = designation.Where(x => x.Qualification_id == model.Qualification_id).ToList();
                if(model.Image!=null && model.Image!="")
                {
                    ViewBag.image = model.Image;
                }
            }
            ViewBag.designation = designation;

            return View(model);
        }

        // POST: Doctor/Create
        [HttpPost]
        public ActionResult AddDoctor(tblEmployee employee, HttpPostedFileBase pic, string hdnImages, int? Doctor_id)
        {
            try
            {
                string file_name = "";
                if (Request.Files.Count > 0)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                
                        //Upload file on server
                        if (file != null)
                        {
                            file_name = file.FileName;
                            String path = Server.MapPath("/assets/images/Doctor_Images/");
                            file.SaveAs(path + file_name);

                   
                        }
                    }
                }
                else if(ViewBag.image!=null && ViewBag.image!="")
                {
                    file_name = "/assets/images/Doctor_Images/" + ViewBag.image;
                }
           

                employee.EmployeeTypeID = 1;
                employee.Image = file_name;
                employee.Is_active = true;
                if(employee.Gender=="1")
                {
                    employee.Gender = "Male";
                }
                if (employee.Gender == "2")
                {
                    employee.Gender = "Female";
                }
                if (Doctor_id!=null &&Doctor_id != 0)
                {
                    employee.ID = Convert.ToInt32(Doctor_id);
                    new DoctorsDAL().UpdateRecord(employee);
                    TempData["AlertTask"] = "Doctor Updated successfully";

                }
                else
                {
                    new DoctorsDAL().InsertRecord(employee);
                    TempData["AlertTask"] = "Doctor added successfully";
                }

                    // TODO: Add insert logic here

                    return Redirect("/doctors");
            }
            catch
            {
                TempData["AlertTask"] = "Error Occured.";
                return View();
            }
        }
        public ActionResult DoctorList()
        {
            var list = new DoctorsDAL().ListOfRecords();
            ViewData["listOfData"] = list;
            return View();
        
        }
        public ActionResult Delete(int Doctor_id)
        {
            new DoctorsDAL().Delete(Doctor_id);
            TempData["AlertTask"] = "Doctor deleted successfully";
            return Redirect("/Doctors");

        }
        // GET: Doctor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Doctor/Edit/5
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

        // GET: Doctor/Delete/5
       
        // POST: Doctor/Delete/5
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

     [HttpPost]
        public ActionResult getDesignationByQulif_id(string qualification_id)

        {
            var lstDesignation = new DoctorsDAL().DesignationbyQualification(Convert.ToInt32(qualification_id)).ToList();

            var jsonResult = Json((new
            {
                Success = "true",
                Data = new
                {
                    lstDesignation = lstDesignation
                 
                }
            }), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }        
    }
}
