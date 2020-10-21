using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCentre.Domain.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int Duration { get; set; }
    }
}
