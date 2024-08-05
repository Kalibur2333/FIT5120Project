using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FIT5120Project.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        public string LocationName { get; set; }
        public string LocationAddress { get; set; }
        public float LocationLongitude { get; set; }
        public float LocationLatitude { get; set; }

        public string Accepts { get; set; }
    }
}