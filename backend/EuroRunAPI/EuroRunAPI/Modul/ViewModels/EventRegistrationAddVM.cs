using EuroRunAPI.Modul.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuroRunAPI.Modul.ViewModels
{
    public class EventRegistrationAddVM
    {
        public int UserAccount_id { get; set; }
        public int Event_id { get; set; }
        public string? Club { get; set; }
        public string? ShirtSize { get; set; }
        public int? NumberOfFinishedRaces { get; set; }
        public string? EventDiscoverySource { get; set; }
        public string? Note { get; set; }
    }
}
