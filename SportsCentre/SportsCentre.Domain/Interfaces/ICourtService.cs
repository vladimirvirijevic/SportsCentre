using SportsCentre.Domain.Models;
using System.Collections.Generic;

namespace SportsCentre.Domain.Interfaces
{
    public interface ICourtService
    {
        List<Court> GetAll();
        Court Add(Court court);
        bool Delete(int id);
    }
}
