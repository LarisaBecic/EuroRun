using System.ComponentModel.DataAnnotations;

namespace EuroRunAPI.Modul.Models
{
    public class EventType
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


    }
}
