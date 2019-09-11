using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HMS.ViewModels
{
    public class CreateBillViewModel
    {
        public CreateBillViewModel()
        {
            PatientsBill = new List<PatientViewModel>();
        }
        public int? PatientAppointmentID { set; get; }
        public string BillNo { set; get; }
        public Nullable<double> Amount { get; set; }
        public string Description { get; set; }

        public Nullable<double> Discount { get; set; }

        public DateTime CreatedAt { get;  set; }
        public string CreatedBy { get;  set; }
        public Nullable<double> AmountTotal { get; set; }
        public string Note { set; get; }

        public List<PatientViewModel> PatientsBill { set; get; }
    }

    public class PatientViewModel
    {
        public int? PatientAppointmentID { set; get; }
        public string BillNo { set; get; }
        public Nullable<double> Amount { get; set; }
        public string Description { set; get; }
        public Nullable<double> Discount { get; set; }
        public DateTime CreatedAt { get;  set; }
        public string CreatedBy { get;  set; }
        public Nullable<double> AmountTotal { get; set; }
        public string Note { set; get; }
    }

    //public class Product
    //{
    //    public int? Patient_ID { set; get; }
    //    public string Patient_Name { set; get; }
    //    public string Patient_Address { set; get; }
    //    public string Contact_Number { set; get; }
    //    public int Age { set; get; }
    //    public string Gender { set; get; }



    //}
    public class ProductSearchModel
    {
        public int? Patient_ID { set; get; }
        public string Patient_Name { set; get; }
        public string Patient_Address { set; get; }
        public string Contact_Number { set; get; }
    }

    public class DashboardViewModel
    {
        public List<DashboardModel> DashboardsModel { set; get; }
        public List<ActivePatientList> ActivePatientsList { set; get; }
        public List<AdmissionMonthly> AdmissionsMonthly { set; get; }
        public List<PatientCount> PatientsCount { set; get; }
        public List<RoomOccupied> RoomsOccupied { set; get; }
        public List<StaffSpeciality> StaffsSpeciality { set; get; }
        public List<Navbar> Navbars { set; get; }
    }

    public class Navbar
    {
        public int AdmittedPatients { get; set; }
        public int NonAdmittedPatients { get; set; }
        public int TotalAppointments { get; set; }
        public int Patients { get; set; }
    }

    public class StaffSpeciality
    {
        public string Speciality { get; set; }
        public int? TotalCount { get; set; }
    }

    public class RoomOccupied
    {
        public int? TotalRooms { get; set; }
        public int? BookedRooms { get; set; }
        public int? RoomsAvailable { get; set; }
        public int? PrivateRoom { get; set; }
        public int? SemiPrivateRoom { get; set; }
        public int? VIPSuite { get; set; }
        public int? SingleDeluxeRoom { get; set; }
        public int? TwoBeddedRoom { get; set; }
        public int? FourBeddedRoom { get; set; }
        public int? IntensiveCareUnit { get; set; }
        public int? IsolationRoom { get; set; }
        public int? LabourDeliverySuite { get; set; }
        public int? Nursery { get; set; }
        public string TestCategories { get; set; }
    }

    public class PatientCount
    {
        public int ID { get; set; }
        public int? OverallPatientsAdmitted { get; set; }
        public int? PatientsAdmittedCurrentMonth { get; set; }
        public int? PatientsAdmittedToday { get; set; }
    }

    public class AdmissionMonthly
    {
        public int Total_Patients { get; set; }
        public DateTime Admission_Date { get; set; }
    }

    public class ActivePatientList
    {
        public int Patient_id { get; set; }
        public string Patient_Name { get; set; }
        public string Patient_address { get; set; }
        public string Contact_no { get; set; }
        public Nullable<int> Age { get; set; }
        public string Gender { get; set; }
        public string Note { get; set; }
    }

    public class DashboardModel
    {
        public Nullable<double> HospitalEarnings { set; get; }
        public int? ActivePatients { set; get; }
        public int TemporrayPatients { set; get; }
        public int PermanentPatients { set; get; }
        public int TodaysOperations { set; get; }
        public int IsActive { set; get; }
    }

    public class PatientBillViewModel
    {
        public int ID { get; set; }
        public int? PatientBillID { get; set; }
        public Nullable<double> Amount { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string Description { get; set; }
        public Nullable<bool> is_active { get; set; }
    }

    public class DeleteViewModel
    {
        public PatientsBillViewModel billmodel { set; get; }
        public PatientsBillDetailViewModel billdetailmodel { set; get; }

    }
    public class PatientsBillViewModel
    {
        public int ID { get; set; }
        public int? PatientAppointmentID { get; set; }
        public string BillNo { get; set; }
        public double? Amount { get; set; }
        public double? Discount { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string Description { get; set; }
    }

    public class PatientsBillDetailViewModel
    {
        public int ID { get; set; }
        public int? PatientBillID { get; set; }
        public double? Amount { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string Description { get; set; }
    }
    //ViewModels for PrintToPDF
    public class Patient
    {
        public string Patient_Name { get; set; }
        public string Patient_address { get; set; }
        public string Contact_no { get; set; }
        public Nullable<int> Age { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> Date_of_Birth { get; set; }
        public string AppointmentDate { get; set; }
       
        public Nullable<double> TotalAmount { get; set; }
        public Nullable<double> Discount { get; set; }

    }
    public class PatientBillDetail
    {
        public Nullable<int> PatientBillID { get; set; }
        public string BillNo { get; set; }
        public Nullable<double> Amount { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string Description { get; set; }
        
    }
   
}