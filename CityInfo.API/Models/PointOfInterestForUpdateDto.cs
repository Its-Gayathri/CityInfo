using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class PointOfInterestForUpdateDto
    {

        [Required(ErrorMessage = "You should Provide a Name Value")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "You should Provide a Description Value")]
        [MaxLength(100)]
        public string Description { get; set; }
    }
}
