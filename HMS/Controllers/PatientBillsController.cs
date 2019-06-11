using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HMS.Models;

namespace HMS.Controllers
{
    public class PatientBillsController : Controller
    {
        private HMS_DBEntity db = new HMS_DBEntity();

        // GET: PatientBills
        public ActionResult Index()
        {
            var model = new PatientBillDAL().GetAllRecords().ToList();
            ViewData["GetRecords"] = model;
            return View();

            //var tblPatientBills = db.tblPatientBills.Include(t => t.tblPatientAppointment);
            //return View(tblPatientBills.ToList());
        }

        // GET: PatientBills/Create
        public ActionResult Create()
        {
            //ViewBag.PatientAppointmentID = new SelectList(db.tblPatientAppointments, "ID", "Description");
            return View();
        }
























        // GET: PatientBills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPatientBill tblPatientBill = db.tblPatientBills.Find(id);
            if (tblPatientBill == null)
            {
                return HttpNotFound();
            }
            return View(tblPatientBill);
        }

      

        // POST: PatientBills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PatientAppointmentID,BillNo,Amount,Discount,CreatedAt,CreatedBy,Description")] tblPatientBill tblPatientBill)
        {
            if (ModelState.IsValid)
            {
                db.tblPatientBills.Add(tblPatientBill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PatientAppointmentID = new SelectList(db.tblPatientAppointments, "ID", "Description", tblPatientBill.PatientAppointmentID);
            return View(tblPatientBill);
        }

        // GET: PatientBills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPatientBill tblPatientBill = db.tblPatientBills.Find(id);
            if (tblPatientBill == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatientAppointmentID = new SelectList(db.tblPatientAppointments, "ID", "Description", tblPatientBill.PatientAppointmentID);
            return View(tblPatientBill);
        }

        // POST: PatientBills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PatientAppointmentID,BillNo,Amount,Discount,CreatedAt,CreatedBy,Description")] tblPatientBill tblPatientBill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblPatientBill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PatientAppointmentID = new SelectList(db.tblPatientAppointments, "ID", "Description", tblPatientBill.PatientAppointmentID);
            return View(tblPatientBill);
        }

        // GET: PatientBills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPatientBill tblPatientBill = db.tblPatientBills.Find(id);
            if (tblPatientBill == null)
            {
                return HttpNotFound();
            }
            return View(tblPatientBill);
        }

        // POST: PatientBills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblPatientBill tblPatientBill = db.tblPatientBills.Find(id);
            db.tblPatientBills.Remove(tblPatientBill);
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
