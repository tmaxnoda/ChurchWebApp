using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchWebApp_Web.Areas.Admin.Models
{
    public class HeaderImage
    {
        public int Id { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageName { get; set; }
        public bool IsActive { get; set; }
    }
}