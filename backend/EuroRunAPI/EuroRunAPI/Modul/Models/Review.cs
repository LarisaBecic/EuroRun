using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuroRunAPI.Modul.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        [ForeignKey(nameof(User))]
        public int User_id { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(Event))]
        public int Event_id { get; set; }
        public Event Event { get; set; }

    }
}
