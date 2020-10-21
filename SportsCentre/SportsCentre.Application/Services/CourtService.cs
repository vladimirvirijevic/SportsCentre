using Microsoft.EntityFrameworkCore;
using SportsCentre.Data;
using SportsCentre.Domain.Interfaces;
using SportsCentre.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsCentre.Services
{
    public class CourtService : ICourtService
    {
        private SportsCentreDbContext _context;

        public CourtService()
        {
            _context = new SportsCentreDbContext();
        }

        public Court Add(Court court)
        {
            _context.Attach(court.Centre);
            var result = _context.Courts.Add(court);

            _context.SaveChanges();

            return result.Entity;
        }

        public bool Delete(int id)
        {
            try
            {
                var entity = _context.Courts.Find(id);
                _context.Courts.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public List<Court> GetAll()
        {
            List<Court> entities = _context.Courts.Include(court => court.Centre).ToList();

            return entities;
        }
    }
}
