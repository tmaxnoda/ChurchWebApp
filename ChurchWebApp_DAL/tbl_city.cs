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
    
    public partial class tbl_city
    {
        public string id { get; set; }
        public string city_name { get; set; }
        public int state_id { get; set; }
    
        public virtual tbl_state tbl_state { get; set; }
    }
}
