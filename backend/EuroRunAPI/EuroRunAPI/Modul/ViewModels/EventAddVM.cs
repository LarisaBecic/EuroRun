using EuroRunAPI.Modul.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuroRunAPI.Modul.ViewModels
{
    public class EventAddVM
    {
        public string Name { get; set; }

        public int Location_id { get; set; }

        public int EventType_id { get; set; }

        public DateTime DateTime { get; set; }

        public string Description { get; set; }

        public DateTime RegistrationDeadline { get; set; }

    }
}
