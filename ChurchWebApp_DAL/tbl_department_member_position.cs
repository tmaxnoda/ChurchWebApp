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
    
    public partial class tbl_department_member_position
    {
        public int id { get; set; }
        public Nullable<int> tbl_position_id { get; set; }
        public Nullable<int> tbl_department_id { get; set; }
        public Nullable<int> tbl_member_biodata_id { get; set; }
    
        public virtual tbl_department tbl_department { get; set; }
        public virtual tbl_member_biodata tbl_member_biodata { get; set; }
        public virtual tbl_position tbl_position { get; set; }
    }
}
