using EuroRunAPI.Modul.Models;
using EuroRunAPI.Modul.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EuroRunAPI.Authentification.ViewModels
{
    public class AuthentificationTokenGetVM
    {
        public string Value { get; set; }
        public UserAccountGetVM UserAccount { get; set; }
        public DateTime TimeOfLogin { get; set; }
        public string IpAddress { get; set; }
    }
}
