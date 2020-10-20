using SportsCentre.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportsCentre.Domain.Interfaces
{
    public interface ICentreService
    {
        Task<List<Centre>> GetAll();
        Task<Centre> Add(Centre centre);
    }
}
