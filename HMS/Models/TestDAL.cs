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


        public tblTestAttribute GetTestAttributes(int test_id, int att_id)
        {
            return db.tblTestAttributes.Where(x => x.ID == att_id && x.TestID == test_id).FirstOrDefault();
        }
        public void UpdateTestAttribute(tblTestAttribute obj)
        {

            db.tblTestAttributes.Attach(obj);
            var update = db.Entry(obj);
            update.Property(x => x.AttributeName).IsModified = true;

            update.Property(x => x.NormalRange).IsModified = true;
            update.Property(x => x.TestID).IsModified = true;
            db.SaveChanges();
        }
        public int AddTestAttribute(tblTestAttribute obj)
        {
            //obj.ID = 18;

            db.tblTestAttributes.Add(obj);
            db.SaveChanges();
            return obj.ID;
        }
        public List<tblTestAttribute> GetAllTestAttribute()
        {
            return db.tblTestAttributes.ToList();

        }

        public void DeleteAttribute(int id)
        {
            var del = db.tblTestAttributes.FirstOrDefault(x => x.ID == id);
            db.tblTestAttributes.Remove(del);
            db.SaveChanges();
        }



        public tblTestAttribute GetTestAttributesbyTestId(int test_id)
        {
            return db.tblTestAttributes.Where(x =>x.TestID == test_id).FirstOrDefault();
        }
    }
}