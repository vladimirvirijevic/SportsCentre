using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCentre.Domain.Models
{
    public class Member : Person
    {
        public string Address { get; set; }
        public string Phone { get; set; }

        public List<Permit> Permits { get; set; }
    }
}
