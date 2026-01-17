using EuroRunAPI.Modul.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuroRunAPI.Modul.ViewModels
{
    public class EventRegistrationGetVM
    {
        public int Id { get; set; }
        public DateTime RegistrationDate { get; set; }
        public UserAccount UserAccount { get; set; }
        public Event Event { get; set; }
        public string? Club { get; set; }
        public string? ShirtSize { get; set; }
        public int? NumberOfFinishedRaces { get; set; }
        public string? EventDiscoverySource { get; set; }
        public string? Note { get; set; }
    }
}
