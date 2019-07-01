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

        // GET: PatientsBill/Create
        public ActionResult Create(int Appointment_id)
        {
            CreateBillViewModel model = new CreateBillViewModel();
            ViewBag.result = Appointment_id;
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
                if (Session["Key"] != null)
                {
                    model.PatientsBill = Session["Key"] as List<PatientViewModel>;
                }
                model.PatientsBill.Add(new PatientViewModel()
                {
                   // PatientAppointmentID = @Appointment_id,
                    BillNo = model.BillNo,
                    Amount = model.Amount,
                    Description = model.Description,
                    Discount = model.Discount,
                    CreatedAt = model.CreatedAt,
                    CreatedBy = model.CreatedBy
                });

                Session["Key"] = model.PatientsBill;
              
            }
            else
            {
                if (ModelState.IsValid)
                {
                   
                    //foreach (var b in model.PatientsBill)
                    //{
                        db.tblPatientBills.Add(new tblPatientBill
                        {
                           // PatientAppointmentID = @Appointment_id,
                            BillNo = model.BillNo,
                            Amount = model.Amount,
                            Description = model.Description,
                            Discount = model.Discount,
                            CreatedAt = model.CreatedAt,
                            CreatedBy = model.CreatedBy

                        });
                        db.SaveChanges();
                        return RedirectToAction("Index");

                    //}


                  


                }
                //    //Insert Data into Database Table
                //    foreach (var b in model.PatientsBill)
                //{
                //    tblPatientBill bill = new tblPatientBill();
                //    //bill.PatientAppointmentID = PatientAppointID;
                //    bill.BillNo = b.BillNo;
                //    bill.Amount = b.Amount;
                //    bill.Description = b.Description;
                //    bill.CreatedAt = b.CreatedAt;
                //    bill.CreatedBy = b.CreatedBy;
                //    bill.Discount = b.Discount;

                //    db.tblPatientBills.Add(bill);
                //    db.SaveChanges();
                //}
               
                //    var data = new tblPatientBill();
                //data = new PatientBillDAL().AddPatientBill(Obj);
            }


            //  Session.Clear();
            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]

      
















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





        // GET: PatientsBill/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: PatientsBill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Patient_id,Patient_Name,Patient_address,Contact_no,Age,Gender,Note,Date_of_Birth")] tblPatient tblPatient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblPatient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblPatient);
        }

        // GET: PatientsBill/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: PatientsBill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblPatient tblPatient = db.tblPatients.Find(id);
            db.tblPatients.Remove(tblPatient);
            db.SaveChanges();
            return RedirectToAction("Index");
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
