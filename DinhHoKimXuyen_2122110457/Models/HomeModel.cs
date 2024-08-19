using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DinhHoKimXuyen_2122110457.Models
{
    public class HomeModel
    {
        public List<product> ListProduct { get; set; }
        public List<category> ListCategory { get; set; }
        public List<brand> ListBrand { get; set; }

        //public List<Banner> ListBanner { get; set; }
    }
}