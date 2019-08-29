using HMS.Models;
using HMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class HomeController : Controller
    {
        private HMS_DBEntity db = new HMS_DBEntity();
        public ActionResult Index()
        {
            DashboardViewModel model = new DashboardViewModel();
            model.DashboardsModel = db.Database.SqlQuery<DashboardModel>("[Sp_Dashboard]").ToList();
            model.ActivePatientsList = db.Database.SqlQuery<ActivePatientList>("[Sp_ActivePatientsList]").ToList();
            db.Database.ExecuteSqlCommand("PatientAdmission");//Trigger it
            model.PatientsCount = db.Database.SqlQuery<PatientCount>("[Patient_Count]").ToList();
            model.RoomsOccupied = db.Database.SqlQuery<RoomOccupied>("[Sp_RoomsOccupied]").ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Get_Highcharts_Data()
        {
            DateTime dt = DateTime.Now;
            int month = Convert.ToInt32(dt.Month);

            //List<tblPatientAdmission> lst = new List<tblPatientAdmission>();
            //var result = lst.Where(r => Convert.ToDateTime(r.AdmissionDate).Month == month);

            var list = db.tblPatientAdmissions.GroupBy(r => Convert.ToDateTime(r.AdmissionDate).Month == month && r.IsActive == true).Count();
            PatientCount obj = new PatientCount();
            obj.PatientsAdmittedCurrentMonth = list;

            //.Select(u => new PatientCount
            //     {
            //         PatientsAdmittedCurrentMonth = list.Count(ID),

            //   }).ToList();

            //var list = db.tblPatientAdmissions.GroupBy(u => DateTime.ParseExact(u.AdmissionDate).Month)
            //    .Select(u => new PatientCount
            //    {
            //        PatientsAdmittedCurrentMonth = u.Count(),
            //        Month = u.FirstOrDefault().AdmissionDate.Month.ToString()
            //    }).ToList();

            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Menu()
        {
            int ID = 0;
            List<GetMenu_Result> Menu = db.GetMenu().Where(x => x.Active_role == true && x.IsActive == true && x.is_active == true && x.show_in_menu == true).ToList();
              HttpCookie cookie = Request.Cookies["AdminCookies"];
            if (cookie != null)
            {
                ID = Convert.ToInt32(cookie.Values["ID"]);
                Menu = Menu.Where(x => x.ID == ID).ToList();
            }
            else
            {
                Menu = null;
            }
          
            return PartialView("Menu", Menu);
        }
    }
}