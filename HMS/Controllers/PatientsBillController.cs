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
                    foreach (var o in lst)
                    {
                        int PatientBill_ID = Convert.ToInt32(patientbill.ID);
                        tblPatientBillDetail billdetail = new tblPatientBillDetail();
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
            tblPatientBillDetail model = db.tblPatientBillDetails.Find(id);

            if (model == null)
            {
                if (tblPatient == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View("PatientsBillDelete",tblPatient);
                }
            }
            else
            {
                return View("PatientsBillDetail",model);
            }
        }

        // POST: PatientsBill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(PatientBillViewModel model)
        {
            db.Database.ExecuteSqlCommand("sp_DeletePatientBill @ID", new SqlParameter("@ID", model.ID)); //trigger it

            //Update Table TblPatientBill
            List<tblPatientBill> tblPatientBill = new List<tblPatientBill>();

        var tblPatientBill2 = db.tblPatientBills.FirstOrDefault(x => x.ID == model.PatientBillID && x.is_active == true);
            tblPatientBill = db.tblPatientBills.Where(x => x.ID == model.PatientBillID && x.is_active == true).ToList();

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

                    List<tblPatientBill> tblPatientBill = new List<tblPatientBill>();

                    var tblPatientBill2 = db.tblPatientBills.FirstOrDefault(x => x.ID == emp.PatientBillID && x.is_active == true);
                    tblPatientBill = db.tblPatientBills.Where(x => x.ID == emp.PatientBillID && x.is_active == true).ToList();

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
            HMS_DBEntities db = new HMS_DBEntities();
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
