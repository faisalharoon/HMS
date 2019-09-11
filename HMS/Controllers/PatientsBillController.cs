using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HMS.Models;
using HMS.ViewModels;
using Rotativa;

namespace HMS.Controllers
{
    public class PatientsBillController : Controller
    {
        private HMS_DBEntity db = new HMS_DBEntity();

    // GET: PatientsBill
    public ActionResult Index()
        {
            // return View(db.tblPatients.OrderBy(x => x.Patient_Name).ToList());
            var model = new PatientBillDAL().GetAllRecords().ToList();
             var Appointments = new PatientBillDAL().ListOfRecords().ToList();
             ViewBag.appointment = Appointments;
            ViewData["GetPatients"] = model;
            return View();
        }

        public ActionResult BillListings()
        {
            var model = new PatientBillDAL().GetPatientBills().ToList();
            var Appointments = new PatientBillDAL().ListOfRecords().ToList();
            var PatientsData = db.tblPatients.ToList();
            ViewBag.patient = PatientsData;
            ViewBag.appointment = Appointments;
            return View(model);
        }
        // GET: PatientsBill/Create
        public ActionResult Create(int? Appointment_id)
        {
            CreateBillViewModel model = new CreateBillViewModel();
            //ViewBag.result = Appointment_id;
            Session["QueryVal"] = Appointment_id;
            return View(model);
        }

        // POST: PatientsBill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string nameValueInsert, string nameValueSubmit, CreateBillViewModel model)
        {
            var button = nameValueInsert ?? nameValueSubmit;
            if (button == "Insert")
            {
                if (Session["templist"] == null)
                {
                    List<PatientViewModel> lst = new List<PatientViewModel>();
                    lst.Add(new PatientViewModel()
                    {
                        BillNo = Request.Form["BillNo"],
                        Amount = double.Parse(Request.Form["Amount"]),
                        Description = Request.Form["Description"]
                    });
                    Session["templist"] = lst;
                }
                else
                {
                    List<PatientViewModel> lst = (List<PatientViewModel>)Session["templist"];

                    lst.Add(new PatientViewModel()
                    {
                        BillNo = Request.Form["BillNo"],
                        Amount = double.Parse(Request.Form["Amount"]),
                        Description = Request.Form["Description"]
                    });

                    Session["templist"] = lst;
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    string username = "";
                    HttpCookie cookie = HttpContext.Request.Cookies["AdminCookies"];
                    if (cookie != null)
                    {
                        username = Convert.ToString(cookie.Values["UserName"]);
                    }
                    tblPatientBill patientbill = new tblPatientBill();
                    patientbill.PatientAppointmentID = model.PatientAppointmentID;
                    patientbill.BillNo = model.BillNo;
                    patientbill.Amount = model.AmountTotal;
                    patientbill.Description = model.Note;
                    patientbill.Discount = model.Discount;
                    patientbill.CreatedAt = model.CreatedAt;
                    patientbill.CreatedBy = username;
                    patientbill.is_active = true;
                    db.tblPatientBills.Add(patientbill);
                    db.SaveChanges();

                    List<PatientViewModel> lst = (List<PatientViewModel>)Session["templist"];
                    tblPatientBillDetail billdetail = new tblPatientBillDetail();
                    int PatientBill_ID = Convert.ToInt32(patientbill.ID);
                    foreach (var o in lst)
                    {
                        billdetail.PatientBillID = PatientBill_ID;
                        billdetail.Amount = model.Amount;
                        billdetail.CreatedAt = model.CreatedAt;
                        billdetail.CreatedBy = username;
                        billdetail.Description = model.Description;
                        billdetail.is_active = true;

                        db.tblPatientBillDetails.Add(billdetail);
                        db.SaveChanges();
                       Session.Clear();
                    }

                    return RedirectToAction("BillListings");
                }
            }
            return View(model);
        }

        public ActionResult Delete(int? id, DeleteViewModel model)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            DeleteViewModel deleteViewModel = new DeleteViewModel();
            deleteViewModel.billmodel = new PatientsBillViewModel();
            deleteViewModel.billdetailmodel = new PatientsBillDetailViewModel();

            tblPatientBill bill = db.tblPatientBills.Find(id);
            tblPatientBillDetail billdetail = db.tblPatientBillDetails.Find(id);

            if (bill != null)
            {
                deleteViewModel.billmodel.ID = bill.ID;
                deleteViewModel.billmodel.PatientAppointmentID = bill.PatientAppointmentID;
                deleteViewModel.billmodel.BillNo = bill.BillNo;
                deleteViewModel.billmodel.Amount = bill.Amount;
                deleteViewModel.billmodel.Discount = bill.Discount;
                deleteViewModel.billmodel.CreatedAt = bill.CreatedAt;
                deleteViewModel.billmodel.CreatedBy = bill.CreatedBy;
                deleteViewModel.billmodel.Description = bill.Description;
            }
            if (billdetail != null)
            {
                deleteViewModel.billdetailmodel.ID = billdetail.ID;
                deleteViewModel.billdetailmodel.PatientBillID = billdetail.PatientBillID;
                deleteViewModel.billdetailmodel.Amount = billdetail.Amount;
                deleteViewModel.billdetailmodel.CreatedAt = billdetail.CreatedAt;
                deleteViewModel.billdetailmodel.CreatedBy = billdetail.CreatedBy;
                deleteViewModel.billdetailmodel.Description = billdetail.Description;
            }
            return View(deleteViewModel);
        }

        // POST: PatientsBill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(PatientBillViewModel model)
        {
            db.Database.ExecuteSqlCommand("sp_DeletePatientBill @ID", new SqlParameter("@ID", model.ID)); //trigger it

            var tblPatientBill2 = (from o in db.tblPatientBillDetails
                          join p in db.tblPatientBills on o.PatientBillID equals p.ID 
                          where o.ID == model.ID && p.is_active == true
                          select p).FirstOrDefault();

            var tblPatientBill = (from o in db.tblPatientBillDetails
                                   join p in db.tblPatientBills on o.PatientBillID equals p.ID
                                   where o.ID == model.ID && p.is_active == true
                                  select p)
                                   .ToList();


            //  var tblPatientBill2 = db.tblPatientBills.FirstOrDefault(x => x.ID == model.PatientBillID && x.is_active == true);
            //  var tblPatientBill = db.tblPatientBills.Where(x => x.ID == model.PatientBillID && x.is_active == true).ToList();

            if (tblPatientBill.Count != 0)
            {
                var lst = db.tblPatientBillDetails
                       .Where(x => x.PatientBillID == tblPatientBill2.ID && x.is_active == true)
                      .ToList();

                foreach (var patientBill in tblPatientBill)
                {
                    patientBill.Amount = lst.Where(b => b.PatientBillID == tblPatientBill2.ID && b.is_active == true).Select(c => c.Amount).Sum();
                    if (patientBill.Amount == 0)
                    {
                        patientBill.is_active = false;
                    }
                }

                db.SaveChanges();
            }
            return RedirectToAction("BillListings");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = db.tblPatientBillDetails.Where(x => x.PatientBillID == id && x.is_active == true).ToList();
            if (model == null)
            {
                return HttpNotFound();
            }
            //ViewData["EditBillRecordID"] = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PatientBillViewModel model)
        {
            string username = "";
            HttpCookie cookie = HttpContext.Request.Cookies["AdminCookies"];
            if (cookie != null)
            {
                username = Convert.ToString(cookie.Values["UserName"]);
            }
                if (model.ID > 0) {
                    //update
                    tblPatientBillDetail emp = db.tblPatientBillDetails.SingleOrDefault(x => x.ID == model.ID && x.is_active == true);
                    emp.PatientBillID = model.PatientBillID;
                    emp.Amount = model.Amount;
                    emp.CreatedAt = DateTime.UtcNow;
                    emp.CreatedBy = username;
                    emp.Description = model.Description;
                    db.SaveChanges();

                   // List<tblPatientBill> tblPatientBill = new List<tblPatientBill>();

                    var tblPatientBill2 = db.tblPatientBills.FirstOrDefault(x => x.ID == emp.PatientBillID && x.is_active == true);
                   var  tblPatientBill = db.tblPatientBills.Where(x => x.ID == emp.PatientBillID && x.is_active == true).ToList();

                    if (tblPatientBill.Count != 0)
                    {
                        var lst = db.tblPatientBillDetails
                               .Where(x => x.PatientBillID == tblPatientBill2.ID && x.is_active == true)
                              .ToList();

                        foreach (var patientBill in tblPatientBill)
                        {
                            patientBill.Amount = lst.Where(b => b.PatientBillID == tblPatientBill2.ID).Select(c => c.Amount).Sum();
                        }
                        db.SaveChanges();
                    }
                return Redirect("/EditBill?id=" + emp.PatientBillID);
            }
            else
            {
                return View();
            }
        }
        public ActionResult EditPatient(int id)
        {
            HMS_DBEntity db = new HMS_DBEntity();
            List<tblPatientBillDetail> list = db.tblPatientBillDetails.ToList();

            PatientBillViewModel model = new PatientBillViewModel();
            if (id > 0)
            {
                tblPatientBillDetail detail = db.tblPatientBillDetails.SingleOrDefault(x => x.ID == id && x.is_active == true);
                model.ID = detail.ID;
                model.PatientBillID = detail.PatientBillID;
                model.Amount = detail.Amount;
                model.Description = detail.Description;
            }
            //else
            //{
            //    tblPatientBillDetail billdetail = new tblPatientBillDetail();
            //    billdetail.PatientBillID = model.PatientBillID;
            //    billdetail.Amount = model.Amount;
            //    billdetail.Description = model.Description;
            //}
            return View(model);
        }

        // GET: PatientsBill/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPatient tblPatient = db.tblPatients.Find(id);
            if (tblPatient == null)
            {
                return HttpNotFound();
            }
            return View(tblPatient);
        }

        public ActionResult PrintPartialViewToPdf(int id)
        {
            var result = (from p in db.tblPatientBills where p.ID == id
                          join o in db.tblPatientAppointments on p.PatientAppointmentID equals o.ID
                          join c in db.tblPatients on o.patient_id equals c.Patient_id
                          select new Patient
                          {
                              Patient_Name = c.Patient_Name,
                              Patient_address = c.Patient_address,
                              Contact_no = c.Contact_no,
                              Age = c.Age,
                              Gender = c.Gender,
                              Date_of_Birth = c.Date_of_Birth,
                              AppointmentDate = o.AppointmentDate,

                              TotalAmount = p.Amount,
                              Discount = p.Discount
                          }
                          ).ToList();
            var detail = (from p in db.tblPatientBills where p.ID == id
                          join o in db.tblPatientBillDetails on p.ID equals o.PatientBillID
                          select new PatientBillDetail
                          {
                              PatientBillID = o.PatientBillID,
                              BillNo = p.BillNo,
                              Amount = o.Amount,
                              CreatedAt = o.CreatedAt,
                              CreatedBy = o.CreatedBy,
                              Description = o.Description
                          }
                          ).ToList();
            ViewBag.BillDetails = detail;

            var report = new PartialViewAsPdf("~/Views/Shared/PatientBillToPDF.cshtml", result);
            return report;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
