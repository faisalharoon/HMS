using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class PatientBillDAL
    {
        HMS_DBEntity db = new HMS_DBEntity();
        public List<tblPatientBill> GetAllRecords()
        {
            return db.tblPatientBills.ToList();

        }


    }
}