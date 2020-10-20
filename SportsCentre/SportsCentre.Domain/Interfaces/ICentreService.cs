using SportsCentre.Domain.Models;
using System.Collections.Generic;

namespace SportsCentre.Domain.Interfaces
{
    public interface ICentreService
    {
        List<Centre> GetAll();
        Centre Add(Centre centre);
        bool Delete(int id);
    }
}
