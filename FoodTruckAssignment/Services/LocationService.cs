using FoodTruckAssignment.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckAssignment.Services
{
    public class LocationService: ILocationService
    {
        private readonly IOptions<ApplicationSettings> _appSettings;
        private readonly ICsvImporterService _csvImporterService;
        private readonly List<Dictionary<string, string>> _csvData;
        private readonly List<FoodFacility> _foodFacilities;
        private readonly int _numberFoodFacilitiesToReturn;
        public LocationService(IOptions<ApplicationSettings> appSettings, ICsvImporterService csvImporterService)
        {
            _appSettings = appSettings;
            _csvImporterService = csvImporterService;
            string csvFileName = _appSettings.Value.CsvFileName;
            _numberFoodFacilitiesToReturn = _appSettings.Value.NumberFoodFacilitiesToReturn;
            _csvData = _csvImporterService.ReadCsvFileToDictionaryList(csvFileName);
            _foodFacilities = CreateFacilityInfo(_csvData);
        }

        private static List<FoodFacility> CreateFacilityInfo(List<Dictionary<string, string>> csvData)
        {
            List<FoodFacility> facilities = new List<FoodFacility>();
            foreach (var row in csvData)
            {
                row.TryGetValue("Latitude", out string latitudeStr);
                row.TryGetValue("Longitude", out string longitudeStr);
                if (string.IsNullOrEmpty(latitudeStr) || string.IsNullOrEmpty(longitudeStr))
                {
                    continue;
                }

                double.TryParse(latitudeStr, out double lat);
                double.TryParse(longitudeStr, out double lon);
                if (lat == 0 || lon == 0)
                {
                    continue;
                }

                row.TryGetValue("Applicant", out string applicant);
                row.TryGetValue("FacilityType", out string facilityType);
                row.TryGetValue("LocationDescription", out string locationDescription);
                row.TryGetValue("Address", out string address);
                row.TryGetValue("FoodItems", out string foodItems);
                row.TryGetValue("Schedule", out string schedule);
                row.TryGetValue("dayshours", out string daysHours);


                FoodFacility foodFacility = new FoodFacility
                {
                    Name = applicant,
                    FacilityType = facilityType,
                    LocationDescription = locationDescription,
                    Address = address,
                    FoodItems = foodItems,
                    Schedule = schedule,
                    DaysHours = daysHours,
                    Latitude = lat,
                    Longitude = lon
                };
                facilities.Add(foodFacility);
            }
            return facilities;
        }


        public List<FoodFacilityWithDistance> GetClosestFoodFacilities(double latitude, double longitude)
        {
            SortedList<double, FoodFacility> nearestFoodFacilities = new SortedList<double, FoodFacility>();

            foreach (var facility in _foodFacilities)
            {
                double dist = distanceBetweenCoordinates(latitude, facility.Latitude, longitude, facility.Longitude);
                if (nearestFoodFacilities.ContainsKey(dist))
                {
                    // SortedList can't have duplicate keys, so if two food facilities have
                    // the same coordinates, and one of them is already in the SortedList,
                    // we'll add a very small fraction to the distance of the other one (the same as the first one)
                    // so that we could add the other one to the SortedList without any impact to the user.
                    dist += 0.00000001;
                }
                if (nearestFoodFacilities.Count < _numberFoodFacilitiesToReturn)
                {
                    nearestFoodFacilities.Add(dist, facility);
                }
                else if (dist < nearestFoodFacilities.Keys[_numberFoodFacilitiesToReturn - 1])
                {
                    nearestFoodFacilities.Add(dist, facility);
                    nearestFoodFacilities.RemoveAt(_numberFoodFacilitiesToReturn);
                }
            }

            var facilitiesWithDistances = nearestFoodFacilities.Select(f => new FoodFacilityWithDistance { Distance = Math.Round(f.Key, 4), FoodFacility = f.Value }).ToList();
            return facilitiesWithDistances;
        }

        static double toRadians(double angle)
        {
            return (angle * Math.PI) / 180;
        }
        static double distanceBetweenCoordinates(double lat1, double lat2, double lon1, double lon2)
        {
            lon1 = toRadians(lon1);
            lon2 = toRadians(lon2);
            lat1 = toRadians(lat1);
            lat2 = toRadians(lat2);

            // Haversine formula  
            double dlon = lon2 - lon1;
            double dlat = lat2 - lat1;
            double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Pow(Math.Sin(dlon / 2), 2);

            double c = 2 * Math.Asin(Math.Sqrt(a));

            // Radius of earth in miles
            double r = 3956;

            // calculate the result 
            return (c * r);
        }

    }
}
