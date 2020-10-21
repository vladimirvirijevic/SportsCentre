using SportsCentre.Domain.Models;
using System.Collections.Generic;

namespace SportsCentre.Domain.Interfaces
{
    public interface ITrainingService
    {
        List<Training> GetAll();
        Training Add(Training court);
        bool Delete(int id);
    }
}
