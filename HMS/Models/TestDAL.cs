using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class TestDAL
    {
        HMS_DBEntity db = new HMS_DBEntity();
        public int AddTestCategory(tblTestCategory obj)
        {
            //obj.ID = 18;

            db.tblTestCategories.Add(obj);
            db.SaveChanges();
            return obj.ID;
        }
        public tblTestCategory GetTestCategory(int cat_id)
        {
            return db.tblTestCategories.FirstOrDefault(x => x.ID == cat_id);
        }

        public void UpdateTestCategory(tblTestCategory obj)
        {

            db.tblTestCategories.Attach(obj);
            var update = db.Entry(obj);
            update.Property(x => x.TestCategory).IsModified = true;

               db.SaveChanges();
        }
        public List<tblTestCategory> GetCategories()
        {
            return db.tblTestCategories.ToList();

        }
        public void DeleteCategory(int id)
        {
            var del = db.tblTestCategories.FirstOrDefault(x => x.ID == id);
            db.tblTestCategories.Remove(del);
            db.SaveChanges();
        }




        public int AddTest(tblTest obj)
        {
            //obj.ID = 18;

            db.tblTests.Add(obj);
            db.SaveChanges();
            return obj.ID;
        }
        public tblTest GetTest(int Test_id)
        {
            return db.tblTests.FirstOrDefault(x => x.ID == Test_id);
        }

        public void UpdateTest(tblTest obj)
        {

            db.tblTests.Attach(obj);
            var update = db.Entry(obj);
            update.Property(x => x.Description).IsModified = true;

            update.Property(x => x.TestCategoryID).IsModified = true;
            update.Property(x => x.TestName).IsModified = true;
             db.SaveChanges();
        }
        public List<tblTest> GetAllTests()
        {
            return db.tblTests.ToList();

        }
        public void DeleteTest(int id)
        {
            var del = db.tblTests.FirstOrDefault(x => x.ID == id);
            db.tblTests.Remove(del);
            db.SaveChanges();
        }
    }
}