using SportsCentre.Data;
using SportsCentre.Domain.Interfaces;
using SportsCentre.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsCentre.Services
{
    public class TrainingService : ITrainingService
    {
        private SportsCentreDbContext _context;

        public TrainingService()
        {
            _context = new SportsCentreDbContext();
        }

        public Training Add(Training entity)
        {
            var result = _context.Trainings.Add(entity);

            _context.SaveChanges();

            return result.Entity;
        }

        public bool Delete(int id)
        {
            try
            {
                var entity = _context.Trainings.Find(id);
                _context.Trainings.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public Training Get(int id)
        {
            return _context.Trainings.Find(id);
        }

        public List<Training> GetAll()
        {
            List<Training> entities = _context.Trainings.ToList();

            return entities;
        }
    }
}
