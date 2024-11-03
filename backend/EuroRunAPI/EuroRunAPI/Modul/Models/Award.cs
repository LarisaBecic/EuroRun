using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuroRunAPI.Modul.Models
{
    public class Award
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

      
    }
}
