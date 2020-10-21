﻿using SportsCentre.Domain.Models;
using System.Collections.Generic;

namespace SportsCentre.Domain.Interfaces
{
    public interface ICentreService
    {
        List<Centre> GetAll();
        Centre Get(int id);
        Centre Add(Centre centre);
        bool Delete(int id);
    }
}
