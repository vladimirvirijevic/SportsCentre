using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCentre.Domain.Models
{
    public class Guest
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
