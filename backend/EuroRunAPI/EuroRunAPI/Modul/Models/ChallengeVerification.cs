using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuroRunAPI.Modul.Models
{
    public class ChallengeVerification
    {
        [Key]
        public int Id { get; set; }

        public bool IsVerified { get; set; }

        public byte[] MedalPicture { get; set; }


        [ForeignKey(nameof(ChallengeProgress))]
        public int ChallengeProgress_id { get; set; }
        public ChallengeProgress ChallengeProgress { get; set; }

        [ForeignKey(nameof(Event))]
        public int Event_id { get; set; }
        public Event Event { get; set; }
    }
}
