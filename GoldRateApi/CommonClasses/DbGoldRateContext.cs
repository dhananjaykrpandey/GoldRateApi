using GoldRateApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldRateApi.CommonClasses
{
    public class DbGoldRateContext : DbContext
    {
        public DbSet<mGoldRate> mGoldRates { get; set; }
        //  public DbSet<mDiseaseSurveillance> mDiseaseSurveillances { get; set; }
        public DbGoldRateContext(DbContextOptions<DbGoldRateContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(Configuration.GetConnectionString("ArnikaInfotechDBConnection")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
