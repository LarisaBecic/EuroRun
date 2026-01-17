using EuroRunAPI.Data;
using EuroRunAPI.Modul.Models;
using Microsoft.AspNetCore.Mvc;
using EuroRunAPI.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace EuroRunAPI.TestDataModul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class TestDataController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly PasswordHasher _passwordHasher;

        public TestDataController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _passwordHasher = new PasswordHasher();
        }


        [HttpPost]
        public async Task<ActionResult> Generate()
        {
            var country1 = new Country
            {
                Name = "Bosnia and Herzegovina",             
            };

            await _dbContext.Countries.AddAsync(country1);
            await _dbContext.SaveChangesAsync();

            var country2 = new Country
            {
                Name = "Germany",
            };

            await _dbContext.Countries.AddAsync(country2);
            await _dbContext.SaveChangesAsync();

            var city1 = new City
            {
                Country_id=country1.Id,
                Name = "Sarajevo",            
            };

            await _dbContext.Cities.AddAsync(city1);
            await _dbContext.SaveChangesAsync();

            var city2 = new City
            {
                Country_id=country1.Id,
                Name = "Mostar",
            };

            await _dbContext.Cities.AddAsync(city2);
            await _dbContext.SaveChangesAsync();

            var location1 = new Location
            {
               Name="Vojanski most, Potoci",
                Latitude=43.4081,
                Longitude=17.87184,
                City_id=city2.Id,
            };

            await _dbContext.Locations.AddAsync(location1);
            await _dbContext.SaveChangesAsync();

            var location2 = new Location
            {
                Name = "Planinica, Mostar",
                Latitude = 43.3866,
                Longitude = 17.7700,
                City_id = city2.Id,
            };

            await _dbContext.Locations.AddAsync(location2);
            await _dbContext.SaveChangesAsync();

            var location3 = new Location
            {
                Name = "Marijin Dvor (ulica Zmaja od Bosne), Sarajevo",
                Latitude = 43.8541,
                Longitude = 18.3927,
                City_id = city1.Id,
            };

            await _dbContext.Locations.AddAsync(location3);
            await _dbContext.SaveChangesAsync();

            var female = new Gender
            {
                Name = "Female",

            };

            await _dbContext.Genders.AddAsync(female);
            await _dbContext.SaveChangesAsync();

            var male = new Gender
            {
                Name = "Male",

            };

            await _dbContext.Genders.AddAsync(male);
            await _dbContext.SaveChangesAsync();

            var role1 = new Role
            {
                Name = "User",
                
            };

            await _dbContext.Roles.AddAsync(role1);
            await _dbContext.SaveChangesAsync();

            var role2 = new Role
            {
                Name = "Administrator",

            };

            await _dbContext.Roles.AddAsync(role2);
            await _dbContext.SaveChangesAsync();

            var useraccount1 = new UserAccount
            {
                FirstName = "Larisa",
                LastName = "Becic",
                PhoneNumber = "061336554",
                Email = "larisa.becic@edu.fit.ba",
                UserName = "lara",
                Active = true,
                DateOfBirth = new DateTime(2000, 2, 23),
                Gender_id = female.Id,
                Role_id = role2.Id
            };

            string useraccount1password = _passwordHasher.HashPassword(useraccount1, "123456");
            useraccount1.Password = useraccount1password;

            await _dbContext.UserAccounts.AddAsync(useraccount1);
            await _dbContext.SaveChangesAsync();

            var useraccount2 = new UserAccount
            {
                FirstName = "Esref",
                LastName = "Pivcic",
                PhoneNumber = "063111254",
                Email = "esref.pivcic@edu.fit.ba",
                UserName = "eshi",
                Active = true,
                DateOfBirth = new DateTime(2001, 3, 21),
                Gender_id = male.Id,
                Role_id = role1.Id
            };

            string useraccount2password = _passwordHasher.HashPassword(useraccount2, "123456");
            useraccount2.Password = useraccount2password;

            await _dbContext.UserAccounts.AddAsync(useraccount2);
            await _dbContext.SaveChangesAsync();

            var eventtype1 = new EventType
            {
                Name="Marathon",
                Description= "The marathon is a long-distance foot race with a distance of 42.195 km, usually run as a road race, but the distance can be covered on trail routes. The marathon can be completed by running or with a run/walk strategy. More than 800 marathons are held worldwide each year, with the vast majority of competitors being recreational athletes, as larger marathons can have tens of thousands of participants."

            };

            await _dbContext.EventTypes.AddAsync(eventtype1);
            await _dbContext.SaveChangesAsync();

            var eventtype2 = new EventType
            {
                Name = "Half marathon",
                Description = "A half marathon is a road running event of 21.0975 kilometres —half the distance of a marathon. It is common for a half marathon event to be held concurrently with a marathon or a 5K race, using almost the same course with a late start, an early finish or shortcuts.If finisher medals are awarded, the medal or ribbon may differ from those for the full marathon."

            };

            await _dbContext.EventTypes.AddAsync(eventtype2);
            await _dbContext.SaveChangesAsync();

            var eventtype3 = new EventType
            {
                Name = "Trail",
                Description = "A trail race is a pedestrian competition open to everyone, which takes place in a natural environment, with the minimum possible of paved roads (20% maximum). The course can range from a few kilometers for short distances all the way to 80 kilometers and beyond for ultra-trail races."

            };

            await _dbContext.EventTypes.AddAsync(eventtype3);
            await _dbContext.SaveChangesAsync();

            var event1 = new Event
            {
                Name = "Mostar Half marathon",
                DateTime = new DateTime(2027, 3, 21, 8, 0, 0),
                Description = "",
                RegistrationDeadline = new DateTime(2027, 3,20,0,0,0),
                EventType_id=eventtype2.Id,
                Location_id=location1.Id,

            };

            await _dbContext.Events.AddAsync(event1);
            await _dbContext.SaveChangesAsync();

            var event2 = new Event
            {
                Name = "Mostar Challenge",
                DateTime = new DateTime(2027, 5, 10, 8, 0, 0),
                Description = "",
                RegistrationDeadline = new DateTime(2027, 5, 8, 0, 0, 0),
                EventType_id = eventtype3.Id,
                Location_id = location2.Id,

            };

            await _dbContext.Events.AddAsync(event2);
            await _dbContext.SaveChangesAsync();

            var event3 = new Event
            {
                Name = "Sarajevo Half marathon",
                DateTime = new DateTime(2027, 9, 21, 8, 30, 0),
                Description = "",
                RegistrationDeadline = new DateTime(2027, 9, 19, 0, 0, 0),
                EventType_id = eventtype2.Id,
                Location_id = location3.Id,

            };

            await _dbContext.Events.AddAsync(event3);
            await _dbContext.SaveChangesAsync();

            var award1 = new Award
            {
                Name = "Gold",

            };

            await _dbContext.Awards.AddAsync(award1);
            await _dbContext.SaveChangesAsync();

            var award2 = new Award
            {
                Name = "Silver",

            };

            await _dbContext.Awards.AddAsync(award2);
            await _dbContext.SaveChangesAsync();

            var award3 = new Award
            {
                Name = "Bronze",

            };

            await _dbContext.Awards.AddAsync(award3);
            await _dbContext.SaveChangesAsync();

            Dictionary<string, int> data = new Dictionary<string, int>();
            data.Add("Countries", _dbContext.Countries.Count());
            data.Add("Cities", _dbContext.Cities.Count());
            data.Add("Locations", _dbContext.Locations.Count());
            data.Add("Roles", _dbContext.Roles.Count());
            data.Add("Genders", _dbContext.Genders.Count());
            data.Add("UserAccounts", _dbContext.UserAccounts.Count());
            data.Add("EventTypes", _dbContext.EventTypes.Count());
            data.Add("Events", _dbContext.Events.Count());
            data.Add("Awards", _dbContext.Awards.Count());


            return Ok(data);
        }
    }
}
