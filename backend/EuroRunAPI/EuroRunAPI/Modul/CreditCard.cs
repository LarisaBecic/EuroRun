using EuroRunAPI.Modul.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EuroRunAPI.Modul
{
    public class CreditCard
    {
        [Key]
        public int Id { get; set; }

        [JsonIgnore]
        public string CardNumber { get; set; }

        [JsonIgnore]
        public string ExpirationDate { get; set; }

        [JsonIgnore]
        public string SecurityNumber { get; set; }


        [ForeignKey(nameof(User))]
        public int User_id { get; set; }
        public User User { get; set; }
    }
}
