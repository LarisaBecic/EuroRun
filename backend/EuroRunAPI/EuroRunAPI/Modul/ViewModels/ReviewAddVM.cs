using EuroRunAPI.Modul.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EuroRunAPI.Modul.ViewModels
{
    public class ReviewAddVM
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int User_id { get; set; }
        public int Event_id { get; set; }
    }
}
