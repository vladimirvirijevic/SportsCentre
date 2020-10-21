using System.Collections.Generic;

namespace SportsCentre.Domain.Models
{
    public class Player : Person
    {
        public string Position { get; set; }
        public Club Club { get; set; }
    }
}
