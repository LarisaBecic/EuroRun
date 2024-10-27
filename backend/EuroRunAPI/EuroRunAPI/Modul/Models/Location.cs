using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EuroRunAPI.Modul.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }


        [ForeignKey(nameof(City))]
        public int City_id { get; set; }
        public City City { get; set; }
    }
}
