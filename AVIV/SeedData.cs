using System;
using System.Linq;
using AVIV.Infrastructure.Data;
using AVIV.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AVIV.API
{
    public class Seeder
    {
        public DbContextOptions<AppDbContext> _contextOpt { get; set; }
        private IDateTime _dateTime;

        public Seeder(DbContextOptions<AppDbContext> contextOpt, IDateTime datetime)
        {
            _contextOpt = contextOpt;
            _dateTime = datetime;
        }


        public async Task Seed()
        {
            try
            {
                using (var dbContext = new AppDbContext(
                    _contextOpt,
                    null,
                    _dateTime)
                )
                {
                    dbContext.Database.MigrateAsync().Wait();


                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
