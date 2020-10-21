using System.Collections.Generic;

namespace SportsCentre.Domain.Models
{
    public class Coach : Person
    {
        public string Position { get; set; }
        public string Type { get; set; }

        public Club Club { get; set; }
    }
}
