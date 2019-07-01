using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class PatientBillDAL
    {
        HMS_DBEntity db = new HMS_DBEntity();
        public List<tblPatient> GetAllRecords()
        {
            return db.tblPatients.ToList();

        }

        public List<tblPatientBill> GetPatientBills()
        {
            return db.tblPatientBills.ToList();

        }

































        public List<tblPatientAppointment> GetAppointments()
        {
            return db.tblPatientAppointments.ToList();

        }

        public tblPatientBill GetBill(int Bill_id)
        {
            return db.tblPatientBills.FirstOrDefault(x => x.ID == Bill_id);
        }



    


    }
}