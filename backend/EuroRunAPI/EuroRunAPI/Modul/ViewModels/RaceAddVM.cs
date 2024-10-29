using EuroRunAPI.Modul.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuroRunAPI.Modul.ViewModels
{
    public class RaceAddVM
    {
        public string Name { get; set; }

        public double Length { get; set; }
   
        public int Event_id { get; set; }
        
    }
}
