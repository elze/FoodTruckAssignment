using System;
using Xunit;

using Microsoft.Extensions.Options;
using FoodTruckAssignment.Models;
using FoodTruckAssignment.Services;
using Moq;
using System.Collections.Generic;

namespace FoodTruckAssignment.Tests
{
    public class LocationServiceTests
    {
        private IOptions<ApplicationSettings> _options;
        private ApplicationSettings _appSettings;
        private Mock<ICsvImporterService> _mockCsvImporterService;
        private string name0 = "Rita's Catering";
        private string name1 = "Tacos y pupusas los trinos";
        private string name2 = "Treats by the Bay LLC";
        private string name3 = "El Calamar Perubian Food Truck";
        private string name4 = "Mike's Catering";
        private string name5 = "DO UC US Mobile Catering";
        private string name6 = "Golden Gate Halal Food";
        private string name7 = "Authentic India";
        private string name8 = "San Francisco's Hometown Creamery";
        private string name9 = "Plaza Garibaldy";
        private string name10 = "Eva's Catering";
        private string name11 = "Truck With Invalid Coordinates";
        Dictionary<string, string> ff0_Ritas_Catering;
        Dictionary<string, string> ff1_Tacos_Y_Pupusas;
        Dictionary<string, string> ff2_Treats_By_Bay;
        Dictionary<string, string> ff3_El_Calamar;
        Dictionary<string, string> ff4_Mikes_Catering;
        Dictionary<string, string> ff5_DO_UC_US;
        Dictionary<string, string> ff6_Golden_Gate_Halal_Food;
        Dictionary<string, string> ff7_Authentic_India;
        Dictionary<string, string> ff8_San_Francisco_Hometown_Creamery;
        Dictionary<string, string> ff9_Plaza_Garibaldy;
        Dictionary<string, string> ff10_Evas_Catering;
        Dictionary<string, string> ff11_TruckWithInvalidCoordinates;

        public LocationServiceTests()
        {
            ff0_Ritas_Catering = new Dictionary<string, string>()
            {
                { "Applicant", name0 },
                { "FacilityType", "Truck" },
                { "LocationDescription", "MISSION ST: 06TH ST to 07TH ST (1000 - 1099)" },
                { "Address", "1028 MISSION ST" },
                { "FoodItems", "Filipino Food" },
                { "Latitude", "37.7806943774082" },
                { "Longitude", "-122.409668813219" },
                { "Schedule", "19MFF-00047_schedule.pdf" },
                { "dayshours", "" }
            };
            ff1_Tacos_Y_Pupusas = new Dictionary<string, string>()
            {
                { "Applicant", name1 },
                { "FacilityType", "" },
                { "LocationDescription", "MISSION ST: AVALON AVE to COTTER ST (4368 - 4439)" },
                { "Address", "4384 MISSION ST" },
                { "FoodItems", "" },
                { "Latitude", "37.7275665375917" },
                { "Longitude", "-122.432969701989" },
                { "Schedule", "19MFF-00048_schedule.pdf" },
                { "dayshours", "" }
            };
            ff2_Treats_By_Bay = new Dictionary<string, string>()
            {
                { "Applicant", name2 },
                { "FacilityType", "Truck" },
                { "LocationDescription", "HOWARD ST: MALDEN ALY to 02ND ST (574 - 599)" },
                { "Address", "201 02ND ST" },
                { "FoodItems", "Prepackaged Kettlecorn" },
                { "Latitude", "37.7868016505971" },
                { "Longitude", "-122.397871635003" },
                { "Schedule", "19MFF-00111_schedule.pdf" },
                { "dayshours", "" }
            };
            ff3_El_Calamar = new Dictionary<string, string>()
            {
                { "Applicant", name3 },
                { "FacilityType", "Truck" },
                { "LocationDescription", "24TH ST: UTAH ST to POTRERO AVE (2600 - 2699)" },
                { "Address", "2615 24TH ST" },
                { "FoodItems", "Lomo Saltado: Jalea: Ceviche: Calamari Tilapia Plates: Chicken: Soda: Water" },
                { "Latitude", "37.7868016505971" },
                { "Longitude", "-122.397871635003" },
                { "Schedule", "19MFF-00111_schedule.pdf" },
                { "dayshours", "" }
            };
            ff4_Mikes_Catering = new Dictionary<string, string>()
            {
                { "Applicant", name4 },
                { "FacilityType", "Truck" },
                { "LocationDescription", "INDIANA ST: 26TH ST to CESAR CHAVEZ ST (1500 - 1599)" },
                { "Address", "1575 INDIANA ST" },
                { "FoodItems", "Cold Truck: packaged sandwiches: snacks: candy: hot and cold drinks" },
                { "Latitude", "37.7508496068125" },
                { "Longitude", "-122.390027615501" },
                { "Schedule", "19MFF-00054_schedule.pdf" },
                { "dayshours", "" }
            };
            ff5_DO_UC_US = new Dictionary<string, string>()
            {
                { "Applicant", name5 },
                { "FacilityType", "Truck" },
                { "LocationDescription", "MARIN ST: KANSAS ST to HWY 101 N ON RAMP (2500 - 2599)" },
                { "Address", "2590 MARIN ST" },
                { "FoodItems", "Cold truck: sandwiches: salads: beverages: chips: candy: cookies: coffee: tea: drinks" },
                { "Latitude", "37.7483758257779" },
                { "Longitude", "-122.403200626496" },
                { "Schedule", "19MFF-00050_schedule.pdf" },
                { "dayshours", "" }
            };
            ff6_Golden_Gate_Halal_Food = new Dictionary<string, string>()
            {
                { "Applicant", name6 },
                { "FacilityType", "Push Cart" },
                { "LocationDescription", "MARKET ST: 07TH ST \\ CHARLES J BRENHAM PL to 08TH ST \\ GROVE ST \\ HYDE ST (1101 - 1199) -- SOUTH --" },
                { "Address", "1169 MARKET ST" },
                { "FoodItems", "Pulao Plates & Sandwiches: Various Drinks" },
                { "Latitude", "0" },
                { "Longitude", "0" },
                { "Schedule", "19MFF-00126_schedule.pdf" },
                { "dayshours", "" }
            };
            ff7_Authentic_India = new Dictionary<string, string>()
            {
                { "Applicant", name7 },
                { "FacilityType", "Push Cart" },
                { "LocationDescription", "MARKET ST: 09TH ST \\ LARKIN ST to 10TH ST \\ FELL ST \\ POLK ST (1301 - 1399) -- SOUTH --" },
                { "Address", "1355 MARKET ST" },
                { "FoodItems", "Indian Street Food" },
                { "Latitude", "37.7767362127501" },
                { "Longitude", "-122.416394930077" },
                { "Schedule", "19MFF-00093_schedule.pdf" },
                { "dayshours", "" }
            };
            ff8_San_Francisco_Hometown_Creamery = new Dictionary<string, string>()
            {
                { "Applicant", name8 },
                { "FacilityType", "Truck" },
                { "LocationDescription", "POST ST: MONTGOMERY ST to LICK PL (1 - 40)" },
                { "Address", "1 MONTGOMERY ST" },
                { "FoodItems", "Ice Cream: Waffle Cones" },
                { "Latitude", "37.7892495340751" },
                { "Longitude", "-122.402418597294" },
                { "Schedule", "19MFF-00008_schedule.pdf" },
                { "dayshours", "" }
            };
            ff9_Plaza_Garibaldy = new Dictionary<string, string>()
            {
                { "Applicant", name9 },
                { "FacilityType", "Truck" },
                { "LocationDescription", "MONTGOMERY ST: POST ST to SUTTER ST (1 - 99)" },
                { "Address", "1 MONTGOMERY ST" },
                { "FoodItems", "Tacos: burritos: quesadillas" },
                { "Latitude", "37.7892495340751" },
                { "Longitude", "-122.402418597294" },
                { "Schedule", "19MFF-00100_schedule.pdf" },
                { "dayshours", "" }
            };

            ff10_Evas_Catering = new Dictionary<string, string>()
            {
                { "Applicant", name10 },
                { "FacilityType", "Truck" },
                { "LocationDescription", "ORTEGA ST: 18TH AVE to 19TH AVE (1100 - 1199)" },
                { "Address", "1900 19TH AVE" },
                { "FoodItems", "Cold Truck: Burrito: Corn Dog: Salads: Sandwiches: Quesadilla: Tacos: Fried Rice: Cow Mein: Chinese Rice: Noodle Plates: Soup: Bacon: Eggs: Ham: Avacado: Sausages: Beverages" },
                { "Latitude", "37.7521241631187" },
                { "Longitude", "-122.475872322222" },
                { "Schedule", "19MFF-00085_schedule.pdf" },
                { "dayshours", "" }
            };

            ff11_TruckWithInvalidCoordinates = new Dictionary<string, string>()
            {
                { "Applicant", name11 },
                { "FacilityType", "Truck" },
                { "LocationDescription", "Nonexistent street in Neverland" },
                { "Address", "000 Neverland" },
                { "FoodItems", "Breatharian food" },
                { "Latitude", "foo" },
                { "Longitude", "bar" },
                { "Schedule", "" },
                { "dayshours", "" }
            };
        }


        [Fact]
        public void LocationService_GivenCoordinates_ShouldReturnFiveClosestFoodFacilities()
        {
            string csvFileName = "testfile.csv";
            double latitude = 37.7521241631190;
            double longitude = -122.475872322456;
            List<Dictionary<string, string>> csvData = new List<Dictionary<string, string>>() {
                ff0_Ritas_Catering, ff1_Tacos_Y_Pupusas, ff2_Treats_By_Bay, ff3_El_Calamar,
                ff4_Mikes_Catering, ff5_DO_UC_US, ff6_Golden_Gate_Halal_Food, ff7_Authentic_India,
                ff8_San_Francisco_Hometown_Creamery,
                ff9_Plaza_Garibaldy,
                ff10_Evas_Catering
            };
            var app = new ApplicationSettings
            {
                CsvFileName = csvFileName,
                NumberFoodFacilitiesToReturn = 5
            };
            _options = Options.Create(app);
            _appSettings = _options.Value;
            _mockCsvImporterService = new Mock<ICsvImporterService>();
            _mockCsvImporterService.Setup(s => s.ReadCsvFileToDictionaryList(csvFileName)).Returns(csvData);
            LocationService locationProviderService = new LocationService(_options, _mockCsvImporterService.Object);
            List<FoodFacilityWithDistance> ff = locationProviderService.GetClosestFoodFacilities(latitude, longitude);
            Assert.Equal(app.NumberFoodFacilitiesToReturn, ff.Count);
            Assert.Equal(0, ff[0].Distance);
            Assert.Equal(name10, ff[0].FoodFacility.Name);
            Assert.Equal(2.8918, ff[1].Distance);
            Assert.Equal(name1, ff[1].FoodFacility.Name);
            Assert.Equal(3.6643, ff[2].Distance);
            Assert.Equal(name7, ff[2].FoodFacility.Name);
            Assert.Equal(3.9758, ff[3].Distance);
            Assert.Equal(name5, ff[3].FoodFacility.Name);
            Assert.Equal(4.1168, ff[4].Distance);
            Assert.Equal(name0, ff[4].FoodFacility.Name);
        }

        [Fact]
        public void LocationProviderService_SameCoordinates_ShouldReturnBothFoodFacilities()
        {
            string csvFileName = "testfile.csv";
            double latitude = 37.7892495340760;
            double longitude = -122.402418597300;
            List<Dictionary<string, string>> csvData = new List<Dictionary<string, string>>() {
                ff0_Ritas_Catering, ff1_Tacos_Y_Pupusas, ff2_Treats_By_Bay, ff3_El_Calamar,
                ff4_Mikes_Catering, ff5_DO_UC_US, ff6_Golden_Gate_Halal_Food, ff7_Authentic_India,
                ff8_San_Francisco_Hometown_Creamery,
                ff9_Plaza_Garibaldy,
                ff10_Evas_Catering
            };
            var app = new ApplicationSettings
            {
                CsvFileName = csvFileName,
                NumberFoodFacilitiesToReturn = 5
            };
            _options = Options.Create(app);
            _appSettings = _options.Value;
            _mockCsvImporterService = new Mock<ICsvImporterService>();
            _mockCsvImporterService.Setup(s => s.ReadCsvFileToDictionaryList(csvFileName)).Returns(csvData);
            LocationService locationProviderService = new LocationService(_options, _mockCsvImporterService.Object);
            List<FoodFacilityWithDistance> ff = locationProviderService.GetClosestFoodFacilities(latitude, longitude);
            Assert.Equal(app.NumberFoodFacilitiesToReturn, ff.Count);
            Assert.Equal(0, ff[0].Distance);
            Assert.Equal(name8, ff[0].FoodFacility.Name);
            Assert.Equal(0, ff[1].Distance);
            Assert.Equal(name9, ff[1].FoodFacility.Name);
            Assert.Equal(0.3002, ff[2].Distance);
            Assert.Equal(name2, ff[2].FoodFacility.Name);
            Assert.Equal(0.3002, ff[3].Distance);
            Assert.Equal(name3, ff[3].FoodFacility.Name);
            Assert.Equal(0.7109, ff[4].Distance);
            Assert.Equal(name0, ff[4].FoodFacility.Name);

        }

        [Fact]
        public void LocationProviderService_ShouldFilterOutInvalidCoordinates()
        {
            List<Dictionary<string, string>> csvData = new List<Dictionary<string, string>>() {
                ff0_Ritas_Catering, ff3_El_Calamar,
                ff11_TruckWithInvalidCoordinates, ff5_DO_UC_US, ff6_Golden_Gate_Halal_Food
            };

            string csvFileName = "testfile.csv";
            double latitude = 37.7892495340760;
            double longitude = -122.402418597300;
            var app = new ApplicationSettings
            {
                CsvFileName = csvFileName,
                NumberFoodFacilitiesToReturn = 5
            };
            _options = Options.Create(app);
            _appSettings = _options.Value;
            _mockCsvImporterService = new Mock<ICsvImporterService>();
            _mockCsvImporterService.Setup(s => s.ReadCsvFileToDictionaryList(csvFileName)).Returns(csvData);
            LocationService locationProviderService = new LocationService(_options, _mockCsvImporterService.Object);
            List<FoodFacilityWithDistance> ff = locationProviderService.GetClosestFoodFacilities(latitude, longitude);
            Assert.Equal(3, ff.Count);
            Assert.Equal(0.3002, ff[0].Distance);
            Assert.Equal(name3, ff[0].FoodFacility.Name);
            Assert.Equal(0.7109, ff[1].Distance);
            Assert.Equal(name0, ff[1].FoodFacility.Name);
            Assert.Equal(2.8225, ff[2].Distance);
            Assert.Equal(name5, ff[2].FoodFacility.Name);
        }

    }
}
