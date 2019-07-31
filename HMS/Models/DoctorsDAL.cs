using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class DoctorsDAL
    {
        HMS_DBEntities db = new HMS_DBEntities();
        public void InsertRecord(tblEmployee obj)
        {
            //obj.ID = 18;

            db.tblEmployees.Add(obj);
            db.SaveChanges();
        }

        public List<tblEmployee> ListOfRecords()
        {
            return db.tblEmployees.OrderBy(x => x.Name).ToList();
        }
        public List<tblEmpDesignation> DesignationbyQualification(int id)
        {
            return db.tblEmpDesignations.Where(x => x.Qualification_id == id).ToList();
        }

        public tblEmployee SingleRecord(int Doc_id)
        {
            return db.tblEmployees.FirstOrDefault(x => x.ID == Doc_id);
        }

        public void UpdateRecord(tblEmployee obj)
        {

            db.tblEmployees.Attach(obj);
            var update = db.Entry(obj);
            update.Property(x => x.Address).IsModified = true;
            update.Property(x => x.Name).IsModified = true;
            update.Property(x => x.Is_active).IsModified = true;
            update.Property(x => x.Mobile_no).IsModified = true;
            update.Property(x => x.Phone_no).IsModified = true;
            update.Property(x => x.status_id).IsModified = true;
            update.Property(x => x.Image).IsModified = true;
            update.Property(x => x.Email).IsModified = true;
            update.Property(x => x.designation_id).IsModified = true;
       

            update.Property(x => x.Qualification_id).IsModified = true;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var del = db.tblEmployees.FirstOrDefault(x => x.ID == id);
            db.tblEmployees.Remove(del);
            db.SaveChanges();
        }
    }
}