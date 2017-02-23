using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChurchWebApp_Web.Areas.Admin.Models
{
    public class Member_BiodataVM
    {
        public int id { get; set; }
        [Display(Name="Image")]
        public byte[] photo { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string first_name { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string last_name { get; set; }
        [Required]
        [Display(Name = "Middle Name")]
        public string middle_name { get; set; }
        [Required]
      
        public DateTime dob { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string phone_number { get; set; }
        [Required]
        [Display(Name = "House Address")]
        public string house_address { get; set; }
        [Required]
        [Display(Name = "House Fellowship")]
        public string marital_status { get; set; }
        [Required]
        [Display(Name = "House Fellowship")]
        public int house_fellowship_id { get; set; }
        [Required]
        public DateTime date_of_initial_attendant { get; set; }
        [Required]
        public string gender { get; set; }

        public virtual string GetFullName
        {
            get
            {
                return string.Format("{0} {1} {2}", last_name, middle_name, first_name);
            }
        }
    }
}