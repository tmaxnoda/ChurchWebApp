using ChurchWebApp_DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChurchWebApp_Web.Areas.Admin.Models
{
    public class OrganizationRegistrationVM
    {
        //public tbl_organization Organization { get; set; }
        public int id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string organization_name { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string organization_address { get; set; }
        [Required]
        [Display(Name = "State")]
        public string organization_state { get; set; }
        [Required]
        [Display(Name = "City")]
        public string organization_city { get; set; }
        [Required]
        [Display(Name = "Country")]
        public int country_id { get; set; }
        [Required]
        [Display(Name = "Heirarchy")]
        public string orgdiscription { get; set; }

        public virtual tbl_country tbl_country { get; set; }
    }
}