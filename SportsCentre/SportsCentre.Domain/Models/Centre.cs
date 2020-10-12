using System.Collections.Generic;

namespace SportsCentre.Domain.Models
{
    public class Centre
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public IEnumerable<Court> Courts { get; set; }
    }
}
