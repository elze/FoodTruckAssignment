using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckAssignment.Models
{
    public class FoodFacility
    {
        public string Name { get; set; }
        public string FacilityType { get; set; }
        public string LocationDescription { get; set; }
        public string Address { get; set; }
        public string FoodItems { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Schedule { get; set; }
        public string DaysHours { get; set; }
    }
}
