using System;
using System.Collections.Generic;
using FoodTruckAssignment.Models;

namespace FoodTruckAssignment.Services
{
    public interface ILocationService
    {
        List<FoodFacilityWithDistance> GetClosestFoodFacilities(double latitude, double longitude);
    }
}
