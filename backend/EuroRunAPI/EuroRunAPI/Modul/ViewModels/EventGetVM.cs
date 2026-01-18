using EuroRunAPI.Modul.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuroRunAPI.Modul.ViewModels
{
    public class EventGetVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public DateTime RegistrationDeadline { get; set; }
        public int EventType_id { get; set; }
        public EventType EventType { get; set; }
        public int Location_id { get; set; }
        public Location Location { get; set; }
        public string? Picture { get; set; }
    }
}
