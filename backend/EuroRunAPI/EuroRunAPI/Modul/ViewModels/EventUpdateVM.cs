namespace EuroRunAPI.Modul.ViewModels
{
    public class EventUpdateVM
    {
        public string Name { get; set; }
        public int Location_id { get; set; }
        public int EventType_id { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public DateTime RegistrationDeadline { get; set; }
        public string? Picture { get; set; }
    }
}
