using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuroRunAPI.Modul.Models
{
    public class Race
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public double Length { get; set; }

        [ForeignKey(nameof(Event))]
        public int Event_id { get; set; }
        public Event Event { get; set; }
    }
}
