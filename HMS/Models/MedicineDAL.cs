using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class MedicineDAL
    {
        HMS_DBEntity db = new HMS_DBEntity();
        public int InsertRecord(tblMedicineCategory obj)
        {
            //obj.ID = 18;

            db.tblMedicineCategories.Add(obj);
            db.SaveChanges();
            return obj.ID;
        }
        public tblMedicineCategory SingleRecord(int cat_id)
        {
            return db.tblMedicineCategories.FirstOrDefault(x => x.ID == cat_id);
        }

        public void UpdateRecord(tblMedicineCategory obj)
        {

            db.tblMedicineCategories.Attach(obj);
            var update = db.Entry(obj);
            update.Property(x => x.Description).IsModified = true;

            update.Property(x => x.isActive).IsModified = true;
            update.Property(x => x.MedicineCategoryName).IsModified = true;
            db.SaveChanges();
        }
    }
}