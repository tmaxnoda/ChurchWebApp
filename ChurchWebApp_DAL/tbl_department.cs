//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChurchWebApp_DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_department
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_department()
        {
            this.tbl_department_member_position = new HashSet<tbl_department_member_position>();
        }
    
        public int id { get; set; }
        public string department_name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_department_member_position> tbl_department_member_position { get; set; }
    }
}
