using System.Collections.Generic;

namespace SportsCentre.Domain.Models
{
    public class Centre : DomainObject
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public IEnumerable<Court> Courts { get; set; }
    }
}
