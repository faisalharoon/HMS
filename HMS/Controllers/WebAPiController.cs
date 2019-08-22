using HMS.Models;
using HMS.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class WebAPiController : ApiController
    {
        private HMS_DBEntity db = new HMS_DBEntity();
        [System.Web.Http.HttpGet]
        [System.Web.Mvc.Route("WebApi/AdmissionsProgress")]
        public IEnumerable<AdmissionMonthly> AdmissionsMonthly()
        {
            List<AdmissionMonthly> acb = new List<AdmissionMonthly>();
            acb = db.Database.SqlQuery<AdmissionMonthly>("[Sp_PatientAdmissions]").ToList();
            return acb;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("WebApi/PatientDetails")]
        public IEnumerable<DashboardModel> DashboardModel()
        {
            List<DashboardModel> acb = new List<DashboardModel>();
            acb = db.Database.SqlQuery<DashboardModel>("[Sp_Dashboard]").ToList();
            return acb;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("WebApi/RoomOccupied")]
        public IEnumerable<RoomOccupied> RoomOccupied()
        {
            List<RoomOccupied> acb = new List<RoomOccupied>();
            acb = db.Database.SqlQuery<RoomOccupied>("[Sp_RoomsOccupied]").ToList();
            return acb;
        }

        [System.Web.Http.HttpGet]

        [System.Web.Http.Route("WebApi/StaffSpeciality")]
        public IEnumerable<StaffSpeciality> StaffSpeciality()
        {
            List<StaffSpeciality> acb = new List<StaffSpeciality>();
            acb = db.Database.SqlQuery<StaffSpeciality>("[Sp_StaffSpeciality]").ToList();
            return acb;
        }



        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}