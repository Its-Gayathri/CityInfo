using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpPut("{id}")]
        public IActionResult UpdatePointOfInterest(int cityId,int id, [FromBody] PointOfInterestForUpdateDto pointOfInterest)
        {
            if (pointOfInterest.Description.ToLower() == pointOfInterest.Name.ToLower())
            {
                ModelState.AddModelError("Description", "The description must be different from Name");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(a => a.Id == id);
            if(pointOfInterestFromStore == null)
            {
                return NotFound();
            }
            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;

            return NoContent(); //204
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id, 
            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(a => a.Id == id);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }
            var pointOfInterestToPatch = new PointOfInterestForUpdateDto() //get the exisitng point of  Interest
            {
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Description
            };

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (pointOfInterestToPatch.Description?.ToLower() == pointOfInterestToPatch.Name?.ToLower())
            {
                ModelState.AddModelError("Description", "The description must be different from Name");
            }
            if(!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }
            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent(); //204
        }
    }
}
// [FromBody] attribute is used to specify that the value should be read from the body of the request.
//get city based on cityId, then get PoointofInterest based on the city and pointOfInterest Id