using EuroRunAPI.Modul.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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
        public UserAccount UserAccount { get; set; }
        public DateTime TimeOfLogin { get; set; }
        public string IpAddress { get; set; }

    }
}
