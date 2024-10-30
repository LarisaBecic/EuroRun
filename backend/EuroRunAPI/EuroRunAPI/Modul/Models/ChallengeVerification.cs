using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuroRunAPI.Modul.Models
{
    public class ChallengeVerification
    {
        [Key]
        public int Id { get; set; }

        public bool Verified { get; set; }

        public byte[] MedalPicture { get; set; }

        [ForeignKey(nameof(User))]
        public int User_id { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(Challenge))]
        public int Challenge_id { get; set; }
        public Challenge Challenge { get; set; }

        [ForeignKey(nameof(Event))]
        public int Event_id { get; set; }
        public Event Event { get; set; }
    }
}
