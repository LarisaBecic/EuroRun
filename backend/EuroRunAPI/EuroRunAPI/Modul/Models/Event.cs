using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuroRunAPI.Modul.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateTime { get; set; }

        public string Description { get; set; }

        public DateTime RegistrationDeadline { get; set; }

        [ForeignKey(nameof(EventType))]
        public int EventType_id { get; set; }
        public EventType EventType { get; set; }

        [ForeignKey(nameof(Location))]
        public int Location_id { get; set; }
        public Location Location { get; set; }
    }
}
