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
        public ActionResult AddPatient(tblPatient obj, int? patient_id)
        {
            obj.is_active = true;
            if (obj.Gender == "1")
            {
                //obj.Gender = "Male";
            }
            if (obj.Gender == "2")
            {
                //obj.Gender = "Female";
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

                return Redirect("/patients");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        public ActionResult PatientList()
        {
            var model = new PatientDAL().ListOfRecords().Where(x=>x.is_active==true).ToList();
            var lstDoctors = new DoctorsDAL().ListOfRecords().Where(x => x.EmployeeTypeID == 1).ToList();
            ViewBag.doctors = lstDoctors;
            ViewData["GetPatientList"] = model;
            return View();
        }
        public ActionResult PatientAdmission(int? Patient_id, int? appointment_id, int? addmission_id)
        {
            int hospital_id = 0;
            HttpCookie cookie = HttpContext.Request.Cookies["AdminCookies"];
            if (cookie != null)
            {
                hospital_id = Convert.ToInt32(cookie.Values["hospital_id"]);
            }
            List<tblHospitalRoom> lstRoom = db.tblHospitalRooms.Where(x => x.isActive == true&&x.HospitalID==hospital_id).ToList();
            ViewBag.ListRoom = lstRoom;
            List<tblAdmissionType> listAdmitType = db.tblAdmissionTypes.ToList();
            ViewBag.listAdmitType = listAdmitType;
            var model = new tblPatientAdmission();
            model.PatientAppointmentID = appointment_id;
            if (addmission_id != null)
            {

                model = new PatientDAL().GetPatientAdmission(Convert.ToInt32(addmission_id), Convert.ToInt32(appointment_id));
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult PatientAdmission(tblPatientAdmission obj, int? Patient_id, int? appointment_id, int? addmission_id)
        {

           
            string AppointmentId = Request.Form["hdnAppointmentId"];
            if (AppointmentId == "") {
                tblPatientAppointment app = new PatientDAL().getPatientAppointments(Convert.ToInt32(Patient_id)).Where(x => x.isActive == true).FirstOrDefault();
                if (app != null && app.ID > 0)
                {
                    AppointmentId = app.ID.ToString();
                } }
            string AdmissionDate = Request.Form["A_Date"];
            string DischargeDate = Request.Form["DischargeDate"];
            string[] s = DischargeDate.Split('-');
            DischargeDate = s[0];
            string time = Convert.ToDateTime(s[1]).ToLongTimeString();
            s = time.Split(' ');
            obj.DisChargeDate = DischargeDate + time;
            s = AdmissionDate.Split('-');

            AdmissionDate = s[0];
            time = Convert.ToDateTime(s[1]).ToLongTimeString();
            s = time.Split(' ');
            obj.AdmissionDate = AdmissionDate + time;


            try
            {
                obj.CreatedAt = DateTime.UtcNow;
                string username = "";
                HttpCookie cookie = HttpContext.Request.Cookies["AdminCookies"];
                if (cookie != null)
                {
                    username = Convert.ToString(cookie.Values["UserName"]);
                } obj.CreatedBy = username;
                obj.IsActive = true;
                if (AppointmentId != "")
                {
                    obj.PatientAppointmentID = Convert.ToInt32(AppointmentId);
                }

                // TODO: Add insert logic here
                if (addmission_id != null && addmission_id != 0)
                {
                    obj.ID = Convert.ToInt32(addmission_id);
                    new PatientDAL().UpdatePatientAdmission(obj);

                    TempData["AlertTask"] = "Patient Admit updated successfully";
                }
                else
                {
                    new PatientDAL().SavePatientAdmission(obj);
                    addmission_id = obj.ID;
                    TempData["AlertTask"] = "Patient Admit added successfully";


                }

                return Redirect("/patient-admissions?patient_id="+obj.patient_id);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return View();
            }

        }


        public ActionResult PatientAppointment(int? patient_id, int? appointment_id)
        {
            ViewBag.ListDoctor = new DoctorsDAL().ListOfRecords().Where(x => x.EmployeeTypeID == 1).ToList();
            var model = new tblPatientAppointment();
            model.patient_id = Convert.ToInt32(patient_id);
            if (patient_id != null && appointment_id != null)
            {

                model = new PatientDAL().GetPatientAppointment(Convert.ToInt32(patient_id), Convert.ToInt32(appointment_id));


            }
            return View(model);
        }
        [HttpPost]
        public ActionResult PatientAppointment(tblPatientAppointment obj, int? appointment_id)
        {

            string p_id = Request.Form["hdnPatientId"];
            string hdndate = Request.Form["hdndate"];
            string[] date = hdndate.Split('-');
            hdndate = date[0];
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
                obj.patient_id = Convert.ToInt32(p_id);
                string time = Convert.ToDateTime(date[1]).ToLongTimeString();
                date = time.Split(' ');
                obj.AppointmentDate = hdndate + time;
                // TODO: Add insert logic here
                if (appointment_id != null && appointment_id != 0)
                {
                    obj.ID = Convert.ToInt32(appointment_id);
                    new PatientDAL().UpdatePatientAppointment(obj);
                    appointment_id = obj.ID;
                    TempData["AlertTask"] = "Patient updated successfully";

                }
                else
                {
                    new PatientDAL().SavePatientAppointment(obj);
                    appointment_id = obj.ID;
                    TempData["AlertTask"] = "Patient Admit added successfully";
                }

                return Redirect("/patient-appointments?patient_id="+obj.patient_id);
                // return Redirect("patient-admission?appointment_id=" + appointment_id+ "&patient_id=" + p_id);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return View();
            }

        }


        // GET: Patient/Delete/5
        public ActionResult Delete(int id)
        {
               new PatientDAL().DeletePatient(Convert.ToInt32(id));
            TempData["AlertTask"] = "Patient deleted successfully";
            return Redirect("/patients");
        }

        // POST: Patient/Delete/5
        public ActionResult AddPatientTest(int? patient_id, int? Test_id)
        {

            List<tblPatientAppointment> lstAppointment = new PatientDAL().getPatientAppointments(Convert.ToInt32(patient_id)).ToList();
            if (lstAppointment.Count > 0)
            {
                ViewBag.lstAppointment = lstAppointment;
            }
            List<tblTest> lstTest = new TestDAL().GetAllTests();
            ViewBag.lstTest = lstTest;
            var model = new tblPatientTest();

            if (patient_id != null)
            {

                model = new PatientDAL().getPatientTestsbypatient_id(Convert.ToInt32(patient_id));
                var patientTestDetails = new PatientDAL().GetPatientTestDetail(Convert.ToInt32(patient_id)).ToList();
                if (patientTestDetails != null)
                {
                    ViewBag.patientTestDetails = patientTestDetails;
                }

            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AddPatientTest(tblPatientTest obj, int? patient_id, int? PatientTestID)
        {
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
                obj.patient_id = Convert.ToInt32(patient_id);

                // TODO: Add insert logic here
                if (PatientTestID != null && PatientTestID != 0)
                {
                    obj.ID = Convert.ToInt32(PatientTestID);
                    new PatientDAL().UpdatePatientTest(obj);
                    PatientTestID = obj.ID;
                    TempData["AlertTask"] = "Patient Test updated successfully";

                }
                else
                {
                    new PatientDAL().SavePatientTest(obj);
                    PatientTestID = obj.ID;
                    TempData["AlertTask"] = "Patient Test added successfully";
                }

                string testcount = Request.Form["testcount"];
                int count = Convert.ToInt32(testcount);
                for (int i = 0; i < count; i++)
                {


                    tblPatientTestDetail test_details = new tblPatientTestDetail();
                    string result = Request.Form["Result_" + i];
                    string attributeId = Request.Form["attributeId_" + i];
                    test_details.Result = result;
                    test_details.PatientTestID = PatientTestID;
                    test_details.TestAttributeID = Convert.ToInt32(attributeId);
                    test_details.CreatedAt = DateTime.UtcNow;
                    test_details.CreatedBy = username;
                    new PatientDAL().SavePatientTestDetails(test_details);
                }

                return Redirect("/patient-tests?patient_id="+patient_id);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return View();
            }

        }


        public ActionResult GetTestDetails(string test_id)
        {
            var testDetails = new TestDAL().GetAllTestAttribute().Where(x => x.TestID == (Convert.ToInt32(test_id))).ToList();

            var test = testDetails.Select(S => new {
                TestID = S.TestID,
                NormalRange = S.NormalRange,
                ID = S.ID,
                AttributeName = S.AttributeName
            });
            var jsonResult = Json((new
            {
                Success = "true",
                Data = new
                {
                    test = test

                }
            }), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }




        public ActionResult AddPatientMedicine(int? patient_id, int? PatientMedID)
        {
            List<tblPatientAppointment> lstAppointment = new PatientDAL().getPatientAppointments(Convert.ToInt32(patient_id)).ToList();
            if (lstAppointment.Count > 0)
            {
                ViewBag.lstAppointment = lstAppointment;
            }
            Session["lstAppointment"] = lstAppointment;
            List<tblMedicine> lstMed = new MedicineDAL().GetAllMedicine().ToList();
            ViewBag.lstMed = lstMed;
            Session["lstMed"] = lstMed;
            List<tblMedicineTiming> lstTiming = new MedicineDAL().GetMedicineTiming().ToList();
            Session["lstTiming"] = lstTiming;
            ViewBag.lstTiming = lstTiming;

            List<tblMedicineOccurance> lstOccurance = new MedicineDAL().GetMedicineOccurance().ToList();
            Session["lstOccurance"] = lstOccurance;
            ViewBag.lstOccurance = lstOccurance;
            if (Session["Medicine"] != null)
            {
                ViewBag.med = Session["Medicine"];

            }
            return View();
        }
        [HttpPost]
        public ActionResult AddPatientMedicine(string add, string Save, int? patient_id, int? PatientMedID, tblPatientMedicine obj)

        {
            string redirect_url = "";
            List<tblPatientMedicine> lst = new List<tblPatientMedicine>();
            var button = add ?? Save;
            if (button == "Add")
            {
                string username = "";
                HttpCookie cookie = HttpContext.Request.Cookies["AdminCookies"];
                if (cookie != null)
                {
                    username = Convert.ToString(cookie.Values["UserName"]);
                }
                obj.CreatedAt = DateTime.UtcNow;
                obj.patient_id = Convert.ToInt32(patient_id);
                obj.CreatedBy = username;

                obj.tblMedicine = new MedicineDAL().GetMedicine(Convert.ToInt32(obj.MedicineID));
                obj.tblPatientAppointment = new PatientDAL().GetPatientAppointment(Convert.ToInt32(patient_id), Convert.ToInt32(obj.PatientAppointmentID));
                tblMedicineTiming time = new MedicineDAL().GetMedicineTiming().Where(x => x.ID == Convert.ToInt32(obj.Timing)).FirstOrDefault();
                obj.Timing = time.TimingName;

                tblMedicineOccurance occurance = new MedicineDAL().GetMedicineOccurance().Where(x => x.ID == Convert.ToInt32(obj.Occurance)).FirstOrDefault();
                obj.Occurance = occurance.OccuranceName;
                obj.isActive = true;

                if (Session["Medicine"] != null)
                {
                    lst = Session["Medicine"] as List<tblPatientMedicine>;
                }
                lst.Add(obj);

                Session["Medicine"] = lst;
                redirect_url = "/add-patient-medicine?Patient_id=" + patient_id;
            }
            else
            {
                if (ModelState.IsValid)
                {
                    lst = Session["Medicine"] as List<tblPatientMedicine>;
                    foreach (var med in lst)
                    {
                        tblPatientMedicine newobj = new tblPatientMedicine();
                        newobj.CreatedAt = med.CreatedAt;
                        newobj.CreatedBy = med.CreatedBy;
                        newobj.isActive = med.isActive;
                        newobj.MedicineID = med.MedicineID;
                        newobj.NoofDays = med.NoofDays;
                        newobj.Occurance = med.Occurance;
                        newobj.PatientAppointmentID = med.PatientAppointmentID;
                        newobj.patient_id = med.patient_id;
                        newobj.Quantity = med.Quantity;
                        newobj.Timing = med.Timing;
                        new PatientDAL().SavePatientMed(newobj);

                    }

                    Session["Medicine"] = null;
                    redirect_url = "/Patient-medicine?Patient_id=" + patient_id;



                }

            }


            ViewBag.med = Session["Medicine"];

            return Redirect(redirect_url);
        }

        public ActionResult PatientMedList(int patient_id)
        {
            var lstPatientMedicine = new PatientDAL().GetPatientMedicineList(patient_id).ToList();
            if (lstPatientMedicine.Count > 0)
                ViewBag.lstPatientMedicine = lstPatientMedicine;

            var lstPatientMed = new PatientDAL().GetPatientMedList(patient_id).ToList();
            if (lstPatientMed.Count > 0)
                ViewBag.lstPatientMed = lstPatientMed;
            return View();

        }


        public ActionResult PatientTestList(int? patient_id)
        {
            var patientTestDetails = new PatientDAL().GetPatientTestDetail(Convert.ToInt32(patient_id)).ToList();
            if (patientTestDetails != null)
            {
                ViewBag.patientTestDetails = patientTestDetails;
            }
            return View();
        }


        public ActionResult PatientappointmentList(int? patient_id)
        {
            var model = new PatientDAL().GetPatientAllAppointments().ToList();

            if (patient_id != null && patient_id!=0)
            {
                model = model.Where(x => x.patient_id == patient_id).ToList();
            }
                return View(model);
            
           
        }

        public ActionResult PatientAdmissionList(int patient_id)
        {
            var model = new PatientDAL().GetPatientAdmits(Convert.ToInt32(patient_id));


            return View(model);


        }
    } 
    
}
