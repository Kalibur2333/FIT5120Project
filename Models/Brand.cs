using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FIT5120Project.Models
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }
        [DisplayName("Brand Name")]
        public string BrandName { get; set; }
        [DisplayName("Year")]
        public int Year { get; set; }
        [DisplayName("Scores")]
        public float Scores { get; set; }
        public int RoundedScores
        {
            get { return (int)Math.Round(Scores); }
        }
        public float Percentage { get; set; }
        public float ChangeBetween { get; set; }
    }
}