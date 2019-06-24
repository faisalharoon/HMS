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
        public int PatientAppointmentID { set; get; }
        public string BillNo { set; get; }
        public Nullable<double> Amount { get; set; }
        public string Description { get; set; }

        public Nullable<double> Discount { get; set; }

        public DateTime? CreatedAt { get; internal set; }
        public string CreatedBy { get; internal set; }

        public List<PatientViewModel> PatientsBill { set; get; }
    }

    public class PatientViewModel
    {
        
        public int PatientAppointmentID { set; get; }
        public string BillNo { set; get; }
        public Nullable<double> Amount { get; set; }
        public string Description { set; get; }
        public Nullable<double> Discount { get; set; }
        public DateTime? CreatedAt { get; internal set; }
        public string CreatedBy { get; internal set; }
    }
}