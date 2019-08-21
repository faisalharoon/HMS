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
    public class SearchController : Controller
    {
        private HMS_DBEntity db = new HMS_DBEntity();

        // GET: Search
        public ActionResult Index(string PatientID, string PatientName,string PatientAddress,string ContactNo)
        {
            if (!string.IsNullOrEmpty(PatientID) ||
                !string.IsNullOrEmpty(PatientName) ||
                !string.IsNullOrEmpty(PatientAddress) ||
                !string.IsNullOrEmpty(ContactNo))
            {
                IQueryable<tblPatient> result = db.tblPatients.AsQueryable();
                if (!string.IsNullOrEmpty(PatientID))
                {
                    int gr = Convert.ToInt32(PatientID);
                    result = result.Where(x => x.Patient_id == gr);
                }
                if (!string.IsNullOrEmpty(PatientName))
                    result = result.Where(x => x.Patient_Name.Contains(PatientName));
                if (!string.IsNullOrEmpty(PatientAddress))
                    result = result.Where(x => x.Patient_address.Contains(PatientAddress));
                if (!string.IsNullOrEmpty(ContactNo))
                    result = result.Where(x => x.Contact_no.Contains(ContactNo));
                ViewData["GetSearchList"] = result;
            }
            else
            {
                ViewData["GetSearchList"] = null;
            }
            return View();
        }
        // GET: Search/Details/5
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

        // GET: Search/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Search/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Patient_id,Patient_Name,Patient_address,Contact_no,Age,Gender,Note,Date_of_Birth")] tblPatient tblPatient)
        {
            if (ModelState.IsValid)
            {
                db.tblPatients.Add(tblPatient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblPatient);
        }

        // GET: Search/Edit/5
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

        // POST: Search/Edit/5
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

        // GET: Search/Delete/5
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

        // POST: Search/Delete/5
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
