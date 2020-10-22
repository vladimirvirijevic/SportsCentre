using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCentre.Domain.Models
{
    public class MatchInfo : Match
    {
        public Club FirstClub { get; set; }
        public Club SecondClub { get; set; }

        public MatchInfo(Match match)
        {
            Id = match.Id;
            Date = match.Date;
            Time = match.Time;
            Duration = match.Duration;
            Type = match.Type;
        }
    }
}
