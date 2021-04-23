using CityInfo.API.Models;
using System.Collections.Generic;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();//can work on the same instance till we restart web server
        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            //init dummy data
            Cities = new List<CityDto>()
            {
               new CityDto()
               {
                   Id = 1,
                   Name ="New York",
                   Description = "The one with big park",
                   PointsOfInterest = new List<PointOfInterestDto>()
                   {
                       new PointOfInterestDto()
                       {
                           Id = 1,
                           Name ="Central Park",
                           Description = "The most visited Park"
                       },
                       new PointOfInterestDto()
                       {
                           Id = 2,
                           Name ="Empire State Building",
                           Description = "A 102-story skyscrapper"
                       }
                   }
               },
               new CityDto()
               {
                   Id =2 ,
                   Name = "Antwerp",
                   Description = "The one that was never finished",
                    PointsOfInterest = new List<PointOfInterestDto>()
                   {
                       new PointOfInterestDto()
                       {
                           Id = 3,
                           Name ="Cathedral of our Lady",
                           Description = "A Gothic style cathedral"
                       },
                       new PointOfInterestDto()
                       {
                           Id = 4,
                           Name ="Antwerp central station",
                           Description = "Railway Station"
                       }
                   }
               },
               new CityDto()
               {
                   Id = 3,
                   Name = "Paris",
                   Description = "The one with the Eiffel Tower",
                    PointsOfInterest = new List<PointOfInterestDto>()
                   {
                       new PointOfInterestDto()
                       {
                           Id = 5,
                           Name ="Eiffel Tower",
                           Description = "Iron lattice tower"
                       },
                       new PointOfInterestDto()
                       {
                           Id = 6,
                           Name ="The Louvre",
                           Description = "The world's largest Museum"
                       }
                   }
               }      
            };
        }
    }
}
