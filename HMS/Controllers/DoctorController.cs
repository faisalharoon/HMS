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
                ViewBag.gender = model.Gender;
                if(model.Image!=null && model.Image!="")
                {
                    ViewData["image"] =model.Image;
                }
            }
            ViewBag.designation = designation;

            return View(model);
        }

        // POST: Doctor/Create
        [HttpPost]
        public ActionResult AddDoctor(tblEmployee employee, int? Doctor_id)
        {
            try
            {
                string file_name = "";
                string hdnImages = Request.Form["hdnImages"];
                string hdnimageName = Request.Form["hdnimageName"];

                employee.EmployeeTypeID = 1;

                employee.Is_active = true;
                if (employee.Gender == "1")
                {
                    // employee.Gender = "Male";
                }
                if (employee.Gender == "2")
                {
                    //  employee.Gender = "Female";
                }
                if (hdnImages != "")
                {

                    String path = Server.MapPath("/assets/images/Doctor_Images/");

                    List<string> Imglst = new List<string>();
                    string[] stringSeparators = new string[] { "#####" };
                    Imglst = hdnImages.Split(stringSeparators, StringSplitOptions.None).ToList();
                    int a = Imglst.Count();
                    Imglst.RemoveAt(a - 1);
                    string Images = null;
                    foreach (var item in Imglst)
                    {


                        if (!item.Contains("/images"))
                        {
                            Images += new GernalFunction().ImageCroping(item, "/assets/images/Doctor_Images/");
                            Images += ",";
                        }
                        else
                        {
                            Images += System.IO.Path.GetFileName(item);
                            Images += ",";
                        }
                    }

                    List<string> splitimgs = new List<string>();
                    splitimgs = Images.Split(',').ToList();
                    int b = splitimgs.Count();
                    splitimgs.RemoveAt(b - 1);
                    foreach (var item in splitimgs)
                    {
                        file_name = new GernalFunction().Resize_MedImage(path, item, path);//Medium images

                    }
                }




            


                else if (hdnimageName != null && hdnimageName != "")
                {
                    file_name = hdnimageName;
                }
           

                employee.Image = file_name;
               
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
            catch(Exception ex)
            {
                string err = ex.ToString();
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
