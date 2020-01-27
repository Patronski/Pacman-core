using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pacman.Models
{
    public class User
    {
        public enum Roles
        {
            user,
            admin
        }

        public User()
        {
            this.Friends = new HashSet<User>();
            this.Statistics = new HashSet<Statistic>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public virtual Country Country { get; set; }
        public int? CountryId { get; set; }
        
        public virtual City City { get; set; }
        public int? CityId { get; set; }

        public string Password { get; set; }
        
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public ICollection<Statistic> Statistics { get; set; }

        public virtual PlayerStatistic PlayerStatistic { get; set; }

        public virtual ICollection<User> Friends { get; set; }
    }
}