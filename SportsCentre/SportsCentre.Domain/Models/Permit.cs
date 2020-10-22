using System.Collections.Generic;

namespace SportsCentre.Domain.Models
{
    public class Permit
    {
        public int Id { get; set; }
        public Training Training { get; set; }

        public Member Member{ get; set; }
    }
}
