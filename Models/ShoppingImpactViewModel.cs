using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FIT5120Project.Models
{
    public class ShoppingImpactViewModel
    {
        // User Inputs
        public int ShopQuantityMonth { get; set; }
        public int OnlineQuantity { get; set; }
        public double ReturnPercOnline { get; set; }
        public int InstoreQuantity { get; set; }
        public double ReturnPercInstore { get; set; }
        public double LandfillPerc { get; set; }
        public double DonatePerc { get; set; }

        // Outputs
        public double Co2Total { get; set; }
        public int DrivingQuantity { get; set; }
        public int TimesToMoon { get; set; }
      
    }
}