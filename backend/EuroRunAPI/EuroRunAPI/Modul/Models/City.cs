using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuroRunAPI.Modul.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey(nameof(Country))]
        public int Country_id { get; set; }
        public Country Country { get; set; }

    }
}
