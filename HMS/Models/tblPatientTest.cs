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
    using System.Collections.Generic;
    
    public partial class tblPatientTest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblPatientTest()
        {
            this.tblPatientTestDetails = new HashSet<tblPatientTestDetail>();
        }
    
        public int ID { get; set; }
        public Nullable<int> PatientAppointmentID { get; set; }
        public Nullable<int> TestID { get; set; }
        public string PathalogistRemarks { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<int> patient_id { get; set; }
    
        public virtual tblPatientAppointment tblPatientAppointment { get; set; }
        public virtual tblTest tblTest { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPatientTestDetail> tblPatientTestDetails { get; set; }
    }
}
