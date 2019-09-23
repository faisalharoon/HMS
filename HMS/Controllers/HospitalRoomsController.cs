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
    public class HospitalRoomsController : Controller
    {
        private HMS_DBEntity db = new HMS_DBEntity();

        // GET: HospitalRooms
        public ActionResult Index()
        {
        var tblHospitalRooms = from p in db.tblHospitalRooms.Include(t => t.tblHospital).Include(t => t.tblRoomType).Where(p=>p.isActive == true)
                               select p;
            return View(tblHospitalRooms.ToList());
        }

        // GET: HospitalRooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHospitalRoom tblHospitalRoom = db.tblHospitalRooms.Find(id);
            if (tblHospitalRoom == null)
            {
                return HttpNotFound();
            }
            return View(tblHospitalRoom);
        }

        // GET: HospitalRooms/Create
        public ActionResult Create()
        {
            ViewBag.HospitalID = new SelectList(db.tblHospitals, "ID", "HospitalName");
            ViewBag.RoomTypeID = new SelectList(db.tblRoomTypes, "ID", "RoomTypeName");
            return View();
        }

        // POST: HospitalRooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RoomTypeID,RoomName,CreatedAt,CreatedBy,Description,isActive,HospitalID")] tblHospitalRoom tblHospitalRoom)
        {
            string username = "";
            HttpCookie cookie = HttpContext.Request.Cookies["AdminCookies"];
            if (cookie != null)
            {
                username = Convert.ToString(cookie.Values["UserName"]);
            }
            if (ModelState.IsValid)
            {
                tblHospitalRoom.CreatedBy = username;
                db.tblHospitalRooms.Add(tblHospitalRoom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HospitalID = new SelectList(db.tblHospitals, "ID", "HospitalName", tblHospitalRoom.HospitalID);
            ViewBag.RoomTypeID = new SelectList(db.tblRoomTypes, "ID", "RoomTypeName", tblHospitalRoom.RoomTypeID);
            return View(tblHospitalRoom);
        }

        // GET: HospitalRooms/Edit/5
        public ActionResult Edit(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            tblHospitalRoom tblHospitalRoom = db.tblHospitalRooms.Find(id);
            if (tblHospitalRoom == null)
            {
                return HttpNotFound();
            }
            ViewBag.HospitalID = new SelectList(db.tblHospitals, "ID", "HospitalName", tblHospitalRoom.HospitalID);
            ViewBag.RoomTypeID = new SelectList(db.tblRoomTypes, "ID", "RoomTypeName", tblHospitalRoom.RoomTypeID);
            return View(tblHospitalRoom);
        }

        // POST: HospitalRooms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RoomTypeID,RoomName,CreatedAt,CreatedBy,Description,isActive,HospitalID")] tblHospitalRoom tblHospitalRoom)
        {
            string username = "";
            HttpCookie cookie = HttpContext.Request.Cookies["AdminCookies"];
            if (cookie != null)
            {
                username = Convert.ToString(cookie.Values["UserName"]);
            }
            if (ModelState.IsValid)
            {
                tblHospitalRoom.CreatedBy = username;
                db.Entry(tblHospitalRoom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HospitalID = new SelectList(db.tblHospitals, "ID", "HospitalName", tblHospitalRoom.HospitalID);
            ViewBag.RoomTypeID = new SelectList(db.tblRoomTypes, "ID", "RoomTypeName", tblHospitalRoom.RoomTypeID);
            return View(tblHospitalRoom);
        }

        // GET: HospitalRooms/Delete/5
        public ActionResult Delete(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            tblHospitalRoom tblHospitalRoom = db.tblHospitalRooms.Find(id);
            if (tblHospitalRoom == null)
            {
                return HttpNotFound();
            }
            return View(tblHospitalRoom);
        }

        // POST: HospitalRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblHospitalRoom tblHospitalRoom = db.tblHospitalRooms.Find(id);
            db.tblHospitalRooms.Remove(tblHospitalRoom);
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
