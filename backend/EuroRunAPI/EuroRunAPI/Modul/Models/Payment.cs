using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EuroRunAPI.Modul.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        public int Total { get; set; }

        [ForeignKey(nameof(EventRegistration))]
        public int EventRegistration_id { get; set; }
        public EventRegistration EventRegistration { get; set; }

        [ForeignKey(nameof(CreditCard))]
        public int CreditCard_id { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}
