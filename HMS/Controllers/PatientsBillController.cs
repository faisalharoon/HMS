using System;
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

namespace HMS.Controllers
{
    public class PatientsBillController : Controller
    {
        private HMS_DBEntities db = new HMS_DBEntities();

        // GET: PatientsBill
        public ActionResult Index()
        {
            // return View(db.tblPatients.OrderBy(x => x.Patient_Name).ToList());
            var model = new PatientBillDAL().GetAllRecords().ToList();
            // var Appointments = new PatientBillDAL().ListOfRecords().ToList();
            // ViewBag.appointment = Appointments;
            ViewData["GetPatients"] = model;
            return View();
        }

        public ActionResult BillListings()
        {
            var model = new PatientBillDAL().GetPatientBills().ToList();
            ViewData["GetBills"] = model;
            return View();
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
                    db.tblPatientBills.Add(patientbill);
                    db.SaveChanges();

                    List<PatientViewModel> lst = (List<PatientViewModel>)Session["templist"];
                    foreach (var o in lst)
                    {
                        int PatientBill_ID = Convert.ToInt32(patientbill.ID);
                        tblPatientBillDetail billdetail = new tblPatientBillDetail();
                        billdetail.PatientBillID = PatientBill_ID;
                        billdetail.Amount = model.Amount;
                        billdetail.CreatedAt = model.CreatedAt;
                        billdetail.CreatedBy = username;
                        billdetail.Description = model.Description;

                        db.tblPatientBillDetails.Add(billdetail);
                        db.SaveChanges();
                        Session.Clear();
                    }

                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        // GET: PatientsBill/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPatientBill tblPatient = db.tblPatientBills.Find(id);
            if (tblPatient == null)
            {
                return HttpNotFound();
            }
            return View(tblPatient);
        }

        // POST: PatientsBill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.Database.ExecuteSqlCommand("sp_DeletePatientBill @ID", new SqlParameter("@ID", id)); //trigger it

            return RedirectToAction("BillListings");
        }
        // GET: PatientsBill/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPatientBill tblPatient = db.tblPatientBills.Find(id);
            if (tblPatient == null)
            {
                return HttpNotFound();
            }
            return View(tblPatient);
        }

        // POST: PatientsBill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PatientAppointmentID,BillNo,Amount,Discount,CreatedAt,CreatedBy,Description")] tblPatientBill tblPatient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblPatient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("BillListings");
            }
            return View(tblPatient);
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
