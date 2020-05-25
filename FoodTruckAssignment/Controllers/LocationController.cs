using System;
using System.Collections.Generic;
using FoodTruckAssignment.Exceptions;
using FoodTruckAssignment.Models;
using FoodTruckAssignment.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodTruckAssignment.Controllers
{
    [ApiExceptionFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationProviderService;
        public LocationController(ILocationService locationProviderService)
        {
            _locationProviderService = locationProviderService;
        }

        [HttpGet("{latitude}/{longitude}", Name = "Get")]
        public ActionResult<List<FoodFacilityWithDistance>> Get(double latitude, double longitude)
        {
            List<FoodFacilityWithDistance> closestFoodFacilities = _locationProviderService.GetClosestFoodFacilities(latitude, longitude);
            return Ok(closestFoodFacilities);
        }
    }
}
