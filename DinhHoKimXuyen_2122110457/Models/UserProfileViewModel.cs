﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DinhHoKimXuyen_2122110457.Models
{
    public class UserProfileViewModel
    {
        public user User { get; set; }
        public List<OrderWithDetailsModel> Orders { get; set; }
    }

    public class OrderWithDetailsModel
    {
        public order Order { get; set; }
        public List<orderdetail> OrderDetails { get; set; }
    }

}