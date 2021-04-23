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
                   Description = "The one with big park"
               },
               new CityDto()
               {
                   Id =2 ,
                   Name = "Antwerp",
                   Description = "The one that was never finished"
               },
               new CityDto()
               {
                   Id = 3,
                   Name = "Paris",
                   Description = "The one with the Eiffel Tower"
               }      
            };
        }
    }
}
