using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EuroRunAPI.Modul.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public double Amount { get; set; }
        public string StripePaymentIntendId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
