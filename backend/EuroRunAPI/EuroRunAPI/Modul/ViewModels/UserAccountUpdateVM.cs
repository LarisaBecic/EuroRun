namespace EuroRunAPI.Modul.ViewModels
{
    public class UserAccountUpdateVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string? Picture { get; set; }
        public bool Active { get; set; }

        public int Role_id { get; set; }
    }
}
