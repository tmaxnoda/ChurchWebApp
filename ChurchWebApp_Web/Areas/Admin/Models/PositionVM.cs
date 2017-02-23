using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChurchWebApp_Web.Areas.Admin.Models
{
    public class PositionVM
    {
        //[Key]
        public int id { get; set; }
        [Required]
        public string position_name { get; set; }
    }
}