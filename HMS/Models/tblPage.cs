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
    
    public partial class tblPage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblPage()
        {
            this.tblUserRolePages = new HashSet<tblUserRolePage>();
        }
    
        public int page_id { get; set; }
        public string page_name { get; set; }
        public string page_url { get; set; }
        public Nullable<bool> is_active { get; set; }
        public Nullable<bool> is_parent { get; set; }
        public Nullable<int> parent_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblUserRolePage> tblUserRolePages { get; set; }
    }
}
