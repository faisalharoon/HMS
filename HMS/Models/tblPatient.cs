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
    
    public partial class tblPatient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblPatient()
        {
            this.tblPatientAppointments = new HashSet<tblPatientAppointment>();
        }
    
        public int Patient_id { get; set; }
        public string Patient_Name { get; set; }
        public string Patient_address { get; set; }
        public string Contact_no { get; set; }
        public Nullable<int> Age { get; set; }
        public string Gender { get; set; }
        public string Note { get; set; }
        public bool? is_active { get; set; }
        public Nullable<System.DateTime> Date_of_Birth { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPatientAppointment> tblPatientAppointments { get; set; }
    }
}
