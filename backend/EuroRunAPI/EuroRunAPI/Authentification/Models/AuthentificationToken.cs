using EuroRunAPI.Modul.Models;
using EuroRunAPI.Modul.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace EuroRunAPI.Authentification.Models
{
    [Table("AuthentificationToken")]
    public class AuthentificationToken
    {
        [Key]
        public int Id { get; set; }
        public string Value {  get; set; }
        [ForeignKey(nameof(UserAccount))]
        public int UserId { get; set; }
        [JsonIgnore]
        public UserAccount UserAccount { get; set; }
        public DateTime TimeOfLogin { get; set; }
        public string IpAddress { get; set; }

    }
}
