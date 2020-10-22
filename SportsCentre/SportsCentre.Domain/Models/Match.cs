using System.Collections.Generic;

namespace SportsCentre.Domain.Models
{
    public class Match : Activity
    {
        public string Type { get; set; }

        public int FirstClubId { get; set; }
        public int SecondClubId { get; set; }

        public List<Ticket> Tickets { get; set; }
    }
}
