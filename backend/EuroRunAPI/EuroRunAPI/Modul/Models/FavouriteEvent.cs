using System.ComponentModel.DataAnnotations.Schema;

namespace EuroRunAPI.Modul.Models
{
    public class FavouriteEvent
    {
        [ForeignKey(nameof(User))]
        public int User_Id { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(Event))]
        public int Event_Id { get; set; }
        public Event Event { get; set; }
    }
}
