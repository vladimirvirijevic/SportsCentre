using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCentre.Data
{
    public class SportsCentreDbContextFactory : IDesignTimeDbContextFactory<SportsCentreDbContext>
    {
        public SportsCentreDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<SportsCentreDbContext>();
            options.UseSqlServer("Server=DESKTOP-D1AVCJU;Database=SportsCentreDb;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new SportsCentreDbContext(options.Options);
        }
    }
}
