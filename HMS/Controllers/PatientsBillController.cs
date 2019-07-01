using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
        private HMS_DBEntity db = new HMS_DBEntity();

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
           // ViewBag.result = Appointment_id;

            var button = nameValueInsert ?? nameValueSubmit;
            if (button == "Insert")
            {
                if (Session["Key"] != null)
                {
                    model.PatientsBill = Session["Key"] as List<PatientViewModel>;
                }
                model.PatientsBill.Add(new PatientViewModel()
                {
                    //PatientAppointmentID = model.PatientAppointmentID,
                    BillNo = model.BillNo,
                    Amount = model.Amount,
                    Description = model.Description
                    //Discount = model.Discount
                    //CreatedAt = model.CreatedAt,
                    //CreatedBy = model.CreatedBy
                });

                Session["Key"] = model.PatientsBill;
           
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


                    db.tblPatientBills.Add(new tblPatientBill
                        {
                           // PatientAppointmentID = @Appointment_id,
                        PatientAppointmentID =    model.PatientAppointmentID,
                            BillNo = model.BillNo,
                            Amount = model.AmountTotal,
                            Description = model.Note,
                            Discount = model.Discount,
                            CreatedAt = DateTime.UtcNow,
                            CreatedBy = username

                    });
                        db.SaveChanges();
                    Session.Abandon();
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
            tblPatientBill tblPatient = db.tblPatientBills.Find(id);
            db.tblPatientBills.Remove(tblPatient);
            db.SaveChanges();
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
