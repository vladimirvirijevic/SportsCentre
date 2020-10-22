using System.Collections.Generic;

namespace SportsCentre.Domain.Models
{
    public class Training : Activity
    {
        public string Description { get; set; }

        public List<TrainingPlayer> TrainingPlayers { get; set; }
        public List<Permit> Permits { get; set; }
    }
}
