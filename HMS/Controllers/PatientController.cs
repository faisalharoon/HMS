using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        HMS_DBEntity db = new HMS_DBEntity();
        public ActionResult Index()
        {
            return View();
        }

        // GET: Patient/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Patient/Create
        public ActionResult AddPatient(int? patient_id)
        {
            var model = new tblPatient();
            if (patient_id != null)
            {
                model = new PatientDAL().SingleRecord(Convert.ToInt32(patient_id));
            }
            return View(model);
        }

        // POST: Patient/Create
        [HttpPost]
        public ActionResult AddPatient(tblPatient obj)
        {
            int patient_id = 0;
            if (obj.Gender == "1")
            {
                obj.Gender = "Male";
            }
            if (obj.Gender == "2")
            {
                obj.Gender = "Female";
            }
            try
            {
                // TODO: Add insert logic here
                if (obj.Patient_id == 0)
                {
                   
                    patient_id = new PatientDAL().InsertRecord(obj);

                    TempData["AlertTask"] = "Patient added successfully";
                }
                else
                {

                    new PatientDAL().UpdateRecord(obj);
                    patient_id = obj.Patient_id;
                    TempData["AlertTask"] = "Patient updated successfully";
                }

                return Redirect("patient-appointment?patient_id=" + patient_id);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult PatientList()
        {
            var model = new PatientDAL().GetAllPatients().ToList();
            var lstDoctors = new DoctorsDAL().ListOfRecords().Where(x => x.EmployeeTypeID == 1).ToList();
            ViewBag.doctors = lstDoctors;
            ViewData["GetPatientList"] = model;
            return View();
        }
        public ActionResult PatientAdmission(int? patient_id, int? appointment_id,int? addmission_id)
        {
          List<tblHospitalRoom> lstRoom = db.tblHospitalRooms.Where(x => x.isActive == true).ToList();
            ViewBag.ListRoom = lstRoom;
            List<tblAdmissionType> listAdmitType = db.tblAdmissionTypes.ToList();
            ViewBag.listAdmitType = listAdmitType;
            var model = new tblPatientAdmission();
            model.PatientAppointmentID = appointment_id;
            if (addmission_id != null)
            {

                model = new PatientDAL().GetPatientAdmission(Convert.ToInt32(patient_id),Convert.ToInt32(appointment_id));
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult PatientAdmission(tblPatientAdmission obj, int? patient_id, int? appointment_id, int? addmission_id)
        {
            string AppointmentId = Request.Form["hdnAppointmentId"];
            string AppointmentDate = Request.Form["A_Date"];
            string DischargeDate = Request.Form["DischargeDate"];
            string[] s = DischargeDate.Split('-');
            DischargeDate = s[0];
            string dischageTime = s[1];
           s = AppointmentDate.Split('-');

            AppointmentDate = s[0];
            string AppointmentTime = s[1];


            try
            {
                obj.CreatedAt = DateTime.UtcNow;
                string username = "";
                HttpCookie cookie = HttpContext.Request.Cookies["AdminCookies"];
                if (cookie != null)
                {
                    username = Convert.ToString(cookie.Values["UserName"]);
                }obj.CreatedBy = username;
                obj.IsActive = true;
                obj.PatientAppointmentID = Convert.ToInt32(AppointmentId);
                //string time = DateTime.UtcNow.ToLongTimeString();
                //string Date = Convert.ToDateTime(obj.AdmissionDate).ToString("dd-MMM-yyyy");
                //string DischargeDate = Convert.ToDateTime(obj.DisChargeDate).ToString("dd-MMM-yyyy");
                DateTime c_date = DateTime.Parse(AppointmentDate + " " + AppointmentTime);
                DateTime d_date = DateTime.Parse(DischargeDate + " " + dischageTime);
                obj.AdmissionDate = Convert.ToDateTime(c_date);
                obj.DisChargeDate = Convert.ToDateTime(d_date);
               
                // TODO: Add insert logic here
                if (obj.ID == 0)
                {

                    new PatientDAL().SavePatientAdmission(obj);
                    addmission_id = obj.ID;
                    TempData["AlertTask"] = "Patient Admit added successfully";
                }
                else
                {

                    new PatientDAL().UpdatePatientAdmission(obj);
                    addmission_id = obj.ID;
                    TempData["AlertTask"] = "Patient Admit updated successfully";
                }

                return Redirect("patients");
            }
            catch(Exception ex)
            {
                string error = ex.ToString();
                return View();
            }

        }


        public ActionResult PatientAppointment(int? patient_id, int? appointment_id)
        {
            ViewBag.ListDoctor = new DoctorsDAL().ListOfRecords().Where(x => x.EmployeeTypeID == 1).ToList();
            var model = new  tblPatientAppointment();
            model.PatientID = Convert.ToInt32(patient_id);
            if (patient_id != null && appointment_id!=null)
            {

                model = new PatientDAL().GetPatientAppointment(Convert.ToInt32(patient_id), Convert.ToInt32(appointment_id));
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult PatientAppointment(tblPatientAppointment obj, int? appointment_id)
        {

            string p_id = Request.Form["hdnPatientId"];
            try
            {
                string username = "";
                HttpCookie cookie = HttpContext.Request.Cookies["AdminCookies"];
                if (cookie != null)
                {
                    username = Convert.ToString(cookie.Values["UserName"]);
                }
                obj.CreatedAt = DateTime.UtcNow;
                obj.CreatedBy = username;
                obj.isActive = true;
                obj.PatientID = Convert.ToInt32(p_id);
                string time = DateTime.UtcNow.ToLongTimeString();
                string Date = Convert.ToDateTime(obj.AppointmentDate).ToString("dd-MMM-yyyy");
                DateTime c_date = DateTime.Parse(Date + " " + time);
                obj.AppointmentDate = c_date;
                // TODO: Add insert logic here
                if (obj.ID == 0)
                {
                    new PatientDAL().SavePatientAppointment(obj);
                    appointment_id = obj.ID;
                    TempData["AlertTask"] = "Patient Admit added successfully";
                }
                else
                {
                    obj.ID = Convert.ToInt32(appointment_id);
                    new PatientDAL().UpdatePatientAppointment(obj);
                    appointment_id = obj.ID;
                    TempData["AlertTask"] = "Patient updated successfully";
                }

                return Redirect("patient-admission?appointment_id=" + appointment_id+ "&patient_id=" + p_id);
            }
            catch
            {
                return View();
            }

        }
        // GET: Patient/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Patient/Edit/5
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

        // GET: Patient/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Patient/Delete/5
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
