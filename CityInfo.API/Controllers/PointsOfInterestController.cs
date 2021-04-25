using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]
    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPointsOfInterest(int cityId)//returns all points for interest from one city
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            //even empty collection is valid in this case(200 Ok), so we will return not found(404) if city == null           
            return Ok(city.PointsOfInterest);
        }
        [HttpGet("{id}",Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id) //returns only one point of interest from a city
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            //even empty collection is valid in this case(200 Ok), so we will return not found(404) if city == null
            var pointsOfInterest = city.PointsOfInterest.FirstOrDefault(a => a.Id == id);
            if(pointsOfInterest == null)
            {
                return NotFound();
            }
            return Ok(pointsOfInterest);
        }

        [HttpPost] //POST Point of Interest
        public IActionResult CreatePointOfInterest(int cityId,[FromBody] PointOfInterestForCreation pointOfInterest)
        {
            if(pointOfInterest.Description.ToLower() == pointOfInterest.Name.ToLower())
            {
                ModelState.AddModelError("Description", "The description must be different from Name");
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(city == null)
            {
                return NotFound();
            }

            //demo purpose, to be refactored later 
            var maxpointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(a => a.PointsOfInterest).Max(p => p.Id);

            var finalpointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxpointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };
            city.PointsOfInterest.Add(finalpointOfInterest);
            return CreatedAtRoute("GetPointOfInterest", new { cityId, id = finalpointOfInterest.Id }, finalpointOfInterest);
        }
    }
}
// [FromBody] attribute is used to specify that the value should be read from the body of the request.