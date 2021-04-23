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
        [HttpGet("{id}")]
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
    }
}
