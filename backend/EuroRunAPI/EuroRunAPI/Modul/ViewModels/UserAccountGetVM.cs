using EuroRunAPI.Modul.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EuroRunAPI.Modul.ViewModels
{
    public class UserAccountGetVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string? Picture { get; set; }
        public bool Active { get; set; }
        public string Role { get; set; }
    }
}
