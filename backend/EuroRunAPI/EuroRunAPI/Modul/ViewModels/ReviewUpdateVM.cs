using EuroRunAPI.Modul.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EuroRunAPI.Modul.ViewModels
{
    public class ReviewUpdateVM
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
