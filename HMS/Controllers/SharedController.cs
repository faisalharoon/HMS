using HMS.Models;
using HMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class SharedController : Controller
    {

        private HMS_DBEntity db = new HMS_DBEntity();
        // ChildActionOnly attribute makes sure that
        // the action cannot be called directly from the url
        [ChildActionOnly]
        public ActionResult NavbarPatientInfo()
        {
            // Get the data you need
            DashboardViewModel model = new DashboardViewModel();
            model.Navbars = db.Database.SqlQuery<Navbar>("[Sp_Dashboard2]").ToList();
            return PartialView("_NavbarPatientInfo", model);
        }


        // GET: Shared
        public ActionResult Index()
        {
            return View();
        }

    }
}