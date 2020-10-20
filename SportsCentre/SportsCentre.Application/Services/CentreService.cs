﻿using Microsoft.EntityFrameworkCore;
using SportsCentre.Data;
using SportsCentre.Domain.Interfaces;
using SportsCentre.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsCentre.Services
{
    public class CentreService : ICentreService
    {
        private SportsCentreDbContext _context;

        public CentreService()
        {
            _context = new SportsCentreDbContext();
        }

        public Centre Add(Centre centre)
        {
            var result = _context.Centres.Add(centre);

            _context.SaveChanges();

            return result.Entity;
        }

        public bool Delete(int id)
        {
            try
            {
                var centre = _context.Centres.Find(id);
                _context.Centres.Remove(centre);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public List<Centre> GetAll()
        {
            List<Centre> centres = _context.Centres.ToList();

            return centres;
        }
    }
}
