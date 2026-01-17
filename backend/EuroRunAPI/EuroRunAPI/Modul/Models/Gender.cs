using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EuroRunAPI.Modul.Models
{
    [Table("Genders")]
    public class Gender
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
