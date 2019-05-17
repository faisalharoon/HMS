using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class HospitalDAL
    {
        HMS_DBEntity db = new HMS_DBEntity();
        public void InsertRecord(tblHospital obj)
        {
            //obj.ID = 18;

            db.tblHospitals.Add(obj);
            db.SaveChanges();
        }

        public List<tblHospital> ListOfRecords()
        {
            return db.tblHospitals.OrderBy(x => x.HospitalName).ToList();
        }

        public tblHospital SingleRecord(int Hospital_id)
        {
            return db.tblHospitals.FirstOrDefault(x => x.ID == Hospital_id);
        }

        public void UpdateRecord(tblHospital obj)
        {

            db.tblHospitals.Attach(obj);
            var update = db.Entry(obj);
            update.Property(x => x.HospitalName).IsModified = true;
       
            update.Property(x => x.Description).IsModified = true;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var del = db.tblHospitals.FirstOrDefault(x => x.ID == id);
            db.tblHospitals.Remove(del);
            db.SaveChanges();
        }
    }
}