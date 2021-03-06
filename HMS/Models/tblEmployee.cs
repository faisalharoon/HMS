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
    
    public partial class tblEmployee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone_no { get; set; }
        public string Mobile_no { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Gender { get; set; }
        public bool Is_active { get; set; }
        public Nullable<int> EmployeeTypeID { get; set; }
        public Nullable<int> status_id { get; set; }
        public Nullable<int> designation_id { get; set; }
        public Nullable<int> Qualification_id { get; set; }
        public Nullable<int> hospital_id { get; set; }
    
        public virtual tblEmpDesignation tblEmpDesignation { get; set; }
        public virtual tblEmployeeQualification tblEmployeeQualification { get; set; }
        public virtual tblEmployeeSatu tblEmployeeSatu { get; set; }
        public virtual tblEmployeeType tblEmployeeType { get; set; }
        public virtual tblHospital tblHospital { get; set; }
    }
}
