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
    
    public partial class tblHospitalRoom
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblHospitalRoom()
        {
            this.tblPatientAdmissions = new HashSet<tblPatientAdmission>();
        }
    
        public int ID { get; set; }
        public Nullable<int> RoomTypeID { get; set; }
        public string RoomName { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string Description { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<int> HospitalID { get; set; }
    
        public virtual tblHospital tblHospital { get; set; }
        public virtual tblRoomType tblRoomType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPatientAdmission> tblPatientAdmissions { get; set; }
    }
}
