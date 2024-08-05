using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FIT5120Project.Models
{
    public class Fabric
    {
        [Key]
        public int FabricId { get; set; }
        [DisplayName("Fabric Name")]
        public string FabricName { get; set; }
        [DisplayName("Greenhouse Gas Emission")]
        public float GreenHouse { get; set; }
        [DisplayName("Pollutants Emitted")]
        public float Pollutants { get; set; }
        [DisplayName("Water Consumption")]
        public float WaterConsum { get; set; }
        [DisplayName("Energy Consumption")]
        public float EnergyConsum { get; set; }
        [DisplayName("Waste Generation")]
        public float WasteConsum { get; set; }
        [DisplayName("Sales Revenue")]
        public float SalesReve { get; set; }
        [DisplayName("Overall Score")]
        public float Score { get; set; }

        public int RoundedScore
        {
            get { return (int)Math.Round(Score); }
        }
    }
}