using System.Collections.Generic;

namespace SportsCentre.Domain.Models
{
    public class Court : DomainObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public Centre Centre { get; set; }
    }
}
