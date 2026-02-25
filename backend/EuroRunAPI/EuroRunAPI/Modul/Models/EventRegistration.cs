using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuroRunAPI.Modul.Models
{
    [Table("EventRegistrations")]
    public class EventRegistration
    {
        [Key]
        public int Id { get; set; }

        public DateTime RegistrationDate { get; set; }

        [ForeignKey(nameof(UserAccount))]
        public int UserAccount_id { get; set; }
        public UserAccount UserAccount { get; set; }

        [ForeignKey(nameof(Event))]
        public int Event_id { get; set; }
        public Event Event { get; set; }

        [ForeignKey(nameof(Payment))]
        public int Payment_id { get; set; }
        public Payment Payment { get; set; }

        public string? Club { get; set; }
        public string? ShirtSize { get; set; }
        public int? NumberOfFinishedRaces { get; set; }
        public string? EventDiscoverySource { get; set; }
        public string? Note { get; set; }
    }
}
