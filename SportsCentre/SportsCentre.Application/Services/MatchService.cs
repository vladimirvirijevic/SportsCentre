using SportsCentre.Data;
using SportsCentre.Domain.Interfaces;
using SportsCentre.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsCentre.Services
{
    public class MatchService : IMatchService
    {
        private SportsCentreDbContext _context;

        public MatchService()
        {
            _context = new SportsCentreDbContext();
        }

        public Match Add(Match entity)
        {
            var result = _context.Matches.Add(entity);

            _context.SaveChanges();

            return result.Entity;
        }

        public bool Delete(int id)
        {
            try
            {
                var entity = _context.Matches.Find(id);
                _context.Matches.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public Match Get(int id)
        {
            return _context.Matches.Find(id);
        }

        public List<Match> GetAll()
        {
            List<Match> entities = _context.Matches.ToList();

            return entities;
        }
    }
}
