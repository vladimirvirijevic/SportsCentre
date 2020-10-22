using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCentre.Domain.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int SeatNumber { get; set; }

        public Match Match { get; set; }
        public Guest Guest { get; set; }
    }
}
