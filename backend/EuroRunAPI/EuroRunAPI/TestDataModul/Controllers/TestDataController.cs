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

            Dictionary<string, int> data = new Dictionary<string, int>();
            data.Add("Countries", _dbContext.Countries.Count());
            data.Add("Cities", _dbContext.Cities.Count());
            data.Add("Locations", _dbContext.Locations.Count());
            data.Add("Roles", _dbContext.Roles.Count());
            data.Add("UserAccounts", _dbContext.UserAccounts.Count());



            return Ok(data);
        }
    }
}
