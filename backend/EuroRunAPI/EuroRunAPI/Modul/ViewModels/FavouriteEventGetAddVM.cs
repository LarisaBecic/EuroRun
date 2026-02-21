using EuroRunAPI.Modul.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuroRunAPI.Modul.ViewModels
{
    public class FavouriteEventGetAddVM
    {
        public int User_Id { get; set; }
        public int Event_Id { get; set; }
    }
}
