using SportsCentre.Data;
using SportsCentre.Domain.Interfaces;
using SportsCentre.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportsCentre.Services
{
    public class CentreService : ICentreService
    {
        SportsCentreDbContext context;

        public CentreService()
        {
            context = new SportsCentreDbContext();
        }

        public async Task<Centre> Add(Centre centre)
        {
            var result = await context.Centres.AddAsync(centre);

            await context.SaveChangesAsync();

            return result.Entity;
        }

        public Task<List<Centre>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
