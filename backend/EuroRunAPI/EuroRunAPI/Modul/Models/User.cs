using System.ComponentModel.DataAnnotations.Schema;

namespace EuroRunAPI.Modul.Models
{
    public class User : UserAccount
    {
        [ForeignKey(nameof(City))]
        public int City_id { get; set; }
        public City CIty { get; set; }
    }
}