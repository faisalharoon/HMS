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
}