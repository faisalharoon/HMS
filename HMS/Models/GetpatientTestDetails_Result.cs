//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HMS.Models
{
    using System;
    
    public partial class GetpatientTestDetails_Result
    {
        public int ID { get; set; }
        public Nullable<int> PatientTestID { get; set; }
        public Nullable<int> TestAttributeID { get; set; }
        public string Result { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<int> patient_id { get; set; }
        public string TestName { get; set; }
        public string AttributeName { get; set; }
        public string NormalRange { get; set; }
        public Nullable<int> PatientAppointmentID { get; set; }
        public string PathalogistRemarks { get; set; }
    }
}
