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
    
    public partial class Sp_ActivePatientsList_Result
    {
        public int Patient_id { get; set; }
        public string Patient_Name { get; set; }
        public string Patient_address { get; set; }
        public string Contact_no { get; set; }
        public Nullable<int> Age { get; set; }
        public string Gender { get; set; }
        public string Note { get; set; }
        public Nullable<System.DateTime> Date_of_Birth { get; set; }
        public Nullable<bool> is_active { get; set; }
    }
}