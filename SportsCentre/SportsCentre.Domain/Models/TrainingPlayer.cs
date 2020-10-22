using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCentre.Domain.Models
{
    public class TrainingPlayer
    {
        public int TrainingId { get; set; }
        public Training Training { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
