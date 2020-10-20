using System.Collections.Generic;

namespace SportsCentre.Domain.Models
{
    public class Centre
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public List<Court> Courts { get; set; }
    }
}
