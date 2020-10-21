using SportsCentre.Domain.Models;
using System.Collections.Generic;

namespace SportsCentre.Domain.Interfaces
{
    public interface IMatchService
    {
        List<Match> GetAll();
        Match Add(Match entity);
        bool Delete(int id);
    }
}
