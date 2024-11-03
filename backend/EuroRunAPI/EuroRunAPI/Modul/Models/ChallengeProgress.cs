using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuroRunAPI.Modul.Models
{
    public class ChallengeProgress
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public int User_id { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(Challenge))]
        public int Challenge_id { get; set; }
        public Challenge Challenge { get; set; }

        [ForeignKey(nameof(Award))]
        public int Award_id { get; set; }
        public Award Award { get; set; }

        public int Number_of_verifications { get; set; }
    }
}
