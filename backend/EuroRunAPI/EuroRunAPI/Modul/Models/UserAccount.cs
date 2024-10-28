﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EuroRunAPI.Modul.Models
{
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public byte[]? Picture { get; set; }
        public bool Active { get; set; }

        [ForeignKey(nameof(Role))]
        public int Role_id { get; set; }
        public Role Role { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        //public string PasswordHash { get; set; }
        //public string PasswordSalt { get; set; }
    }
}
