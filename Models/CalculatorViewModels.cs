using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FIT5120Project.Models
{
    public class CalculatorViewModels
    {
        public int ShopQuantityMonth { get; set; }
        public int OnlineQuantity { get; set; }
        public int ReturnPercOnline { get; set; }
        public int InstoreQuantity { get; set; }
        public int ReturnPercInstore { get; set; }
        public int LandfillPercentage { get; set; }
        public int DonatePercentage { get; set; }
        public double Co2Total { get; set; }
    }
}