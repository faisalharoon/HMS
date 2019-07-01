using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class PatientDAL

    {
        HMS_DBEntity db = new HMS_DBEntity();
        public int InsertRecord(tblPatient obj)
        {
            //obj.ID = 18;

            db.tblPatients.Add(obj);
            db.SaveChanges();
            return obj.Patient_id;
        }
        public void SavePatientAdmission(tblPatientAdmission obj)
        {
            //obj.ID = 18;

            db.tblPatientAdmissions.Add(obj);
            db.SaveChanges();
        }
        public void SavePatientAppointment(tblPatientAppointment obj)
        {
            //obj.ID = 18;
            try
            {
                db.tblPatientAppointments.Add(obj);
                db.SaveChanges();

            }
            catch(Exception ex)
            {
               string error= ex.ToString();
            }
            }
        public List<tblPatient> ListOfRecords()
        {
            return db.tblPatients.OrderBy(x => x.Patient_Name).ToList();
        }
        public List<GetPatientList_Result> GetAllPatients()
        {
            return db.GetPatientList().ToList();
        }
        public tblPatient SingleRecord(int Patient_id)
        {
            return db.tblPatients.FirstOrDefault(x => x.Patient_id == Patient_id);
        }
        public tblPatientAdmission GetPatientAdmission(int admit_id, int appoint_id)
        {
            return db.tblPatientAdmissions.FirstOrDefault(x => x.ID == admit_id & x.PatientAppointmentID==appoint_id);
        }
        public tblPatientAppointment GetPatientAppointment(int Patient_id, int appoint_id)
        {
            return db.tblPatientAppointments.FirstOrDefault(x => x.PatientID == Patient_id & x.ID == appoint_id);
        }

       
        public void UpdateRecord(tblPatient obj)
        {

            db.tblPatients.Attach(obj);
            var update = db.Entry(obj);
            update.Property(x => x.Age).IsModified = true;
            update.Property(x => x.Contact_no).IsModified = true;
            update.Property(x => x.Gender).IsModified = true;
            update.Property(x => x.Note).IsModified = true;
            update.Property(x => x.Patient_address).IsModified = true;


            update.Property(x => x.Patient_Name).IsModified = true;
            db.SaveChanges();
        }
        public void UpdatePatientAdmission(tblPatientAdmission obj)
        {

            db.tblPatientAdmissions.Attach(obj);
            var update = db.Entry(obj);
            update.Property(x => x.Description).IsModified = true;
            update.Property(x => x.DisChargeDate).IsModified = true;
            update.Property(x => x.AdmissionDate).IsModified = true;
            update.Property(x => x.HospitalRoomID).IsModified = true;
            update.Property(x => x.IsActive).IsModified = true;
            db.SaveChanges();
        }
        public void UpdatePatientAppointment(tblPatientAppointment obj)
        {

            db.tblPatientAppointments.Attach(obj);
            var update = db.Entry(obj);
            update.Property(x => x.Description).IsModified = true;
            update.Property(x => x.AppointmentDate).IsModified = true;
            update.Property(x => x.DoctorID).IsModified = true;
            update.Property(x => x.isActive).IsModified = true;

            db.SaveChanges();
        }
        public void Delete(int id)
        {
            var del = db.tblPatients.FirstOrDefault(x => x.Patient_id == id);
            db.tblPatients.Remove(del);
            db.SaveChanges();
        }
        public List<tblPatientAppointment> getPatientAppointments(int patient_id)

        {
            return db.tblPatientAppointments.Where(x => x.PatientID == patient_id && x.isActive == true).ToList();
        }



        //PAtientTest
        public void SavePatientTest(tblPatientTest obj)
        {
            //obj.ID = 18;

            db.tblPatientTests.Add(obj);
            db.SaveChanges();
        }
        public void SavePatientTestDetails(tblPatientTestDetail obj)
        {
            //obj.ID = 18;

            db.tblPatientTestDetails.Add(obj);
            db.SaveChanges();
        }
        public void UpdatePatientTest(tblPatientTest obj)
        {

            db.tblPatientTests.Attach(obj);
            var update = db.Entry(obj);
            update.Property(x => x.PathalogistRemarks).IsModified = true;
            update.Property(x => x.PatientAppointmentID).IsModified = true;
            update.Property(x => x.TestID).IsModified = true;
                db.SaveChanges();
        }
        public tblPatientTest getPatientTestsbypatient_id(int patient_id)

        {
            return db.tblPatientTests.Where(x => x.patient_id == patient_id).FirstOrDefault();
        }
        public List<GetpatientTestDetails_Result> GetPatientTestDetail(int patient_id)
        {
            return db.GetpatientTestDetails().Where(x=>x.patient_id==patient_id).ToList();

        }


        public void SavePatientMed(tblPatientMedicine obj)
        {
            //obj.ID = 18;

            db.tblPatientMedicines.Add(obj);
            
            db.SaveChanges();
        }


       public List<GetPatientMedicineList_Result>GetPatientMedicineList(int patient_id)
        {
            return db.GetPatientMedicineList().Where(x => x.patient_id == patient_id).ToList();
        }
        public List<GetPatientMedList_Result> GetPatientMedList(int patient_id)
        {
            return db.GetPatientMedList().Where(x => x.patient_id == patient_id).ToList();
        }

    }
}