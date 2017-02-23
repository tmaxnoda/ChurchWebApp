using ChurchWebApp_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchWebApp_Web.Areas.Admin.Models
{
    public class BioDepPositionVM
    {
        public int id { get; set; }
        public int tbl_position_id { get; set; }
        public int tbl_department_id { get; set; }
        public int tbl_member_biodata_id { get; set; }

        public tbl_department tbl_department { get; set; }
        public tbl_member_biodata tbl_member_biodata { get; set; }
        public tbl_position tbl_position { get; set; }

        public virtual string GetFullName
        {
            get
            {
                return string.Format("{0} {1} {2}", tbl_member_biodata.last_name,tbl_member_biodata.first_name,tbl_member_biodata.middle_name);
            }
        }
    }
}