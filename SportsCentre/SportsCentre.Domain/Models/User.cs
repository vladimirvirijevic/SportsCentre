using System.Collections.Generic;

namespace SportsCentre.Domain.Models
{
    public class User : DomainObject
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
