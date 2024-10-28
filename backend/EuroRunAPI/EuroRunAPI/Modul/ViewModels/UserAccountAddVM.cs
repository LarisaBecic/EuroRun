namespace EuroRunAPI.Modul.ViewModels
{
    public class UserAccountAddVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public byte[]? Picture { get; set; }
        public bool Active { get; set; }

        public int Role_id { get; set; }
    }
}
