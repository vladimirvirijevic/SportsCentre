using System.Collections.Generic;

namespace SportsCentre.Domain.Models
{
    public class Club
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public string Sport { get; set; }
        public string Founded { get; set; }

        public List<Player> Players { get; set; }
    }
}
