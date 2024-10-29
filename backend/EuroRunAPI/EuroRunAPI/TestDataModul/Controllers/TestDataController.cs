using EuroRunAPI.Data;
using EuroRunAPI.Modul.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace EuroRunAPI.TestDataModul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class TestDataController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public TestDataController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
               Name="Vojanski most,Potoci",
                Latitude=43.40812,
                Longitude=17.87184,
                City_id=city2.Id,
            };

            await _dbContext.Locations.AddAsync(location1);
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
                PhoneNumber = "061123456",
                Email = "larisa.becic@edu.fit.ba",
                UserName = "larisa.becic",
                Active = true,
                Role_id = role1.Id,
                Password = "123456",

    };

            await _dbContext.UserAccounts.AddAsync(useraccount1);
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

            var event1 = new Event
            {
                Name = "Mostar Half marathon",
                DateTime = new DateTime(2025, 3, 22, 8, 0, 0),
                Description = "",
                RegistrationDeadline = new DateTime(2025,3,20,0,0,0),
                EventType_id=eventtype2.Id,
                Location_id=location1.Id,

            };

            await _dbContext.Events.AddAsync(event1);
            await _dbContext.SaveChangesAsync();

            Dictionary<string, int> data = new Dictionary<string, int>();
            data.Add("Countries", _dbContext.Countries.Count());
            data.Add("Cities", _dbContext.Cities.Count());
            data.Add("Locations", _dbContext.Locations.Count());
            data.Add("Roles", _dbContext.Roles.Count());
            data.Add("UserAccounts", _dbContext.UserAccounts.Count());
            data.Add("EventTypes", _dbContext.EventTypes.Count());
            data.Add("Events", _dbContext.Events.Count());
            //data.Add("Races", _dbContext.Races.Count());


            return Ok(data);
        }
    }
}
