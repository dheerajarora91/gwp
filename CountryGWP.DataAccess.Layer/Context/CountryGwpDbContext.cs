using CountryGWP.DataAccess.Layer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CountryGWP.DataAccess.Layer.Context
{
    public class CountryGwpDbContext : DbContext
    {
        public DbSet<CountryGwp> CountriesGwp { get; set; }

        public CountryGwpDbContext(DbContextOptions<CountryGwpDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var countriesGwp = new List<CountryGwp>();
            int counter = 0;

            using (var reader = new StreamReader(@"D:\\gwpByCountry.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (counter > 0)
                    {
                        countriesGwp.Add(FromCsv(line,counter));
                    }

                    counter++;
                }
            }

            foreach (var countryGwp in countriesGwp)
            {
                modelBuilder.Entity<CountryGwp>().HasData(
                    new CountryGwp
                    {
                        CountryGwpId = countryGwp.CountryGwpId,
                        Country = countryGwp.Country,
                        Lob = countryGwp.Lob,
                        Y2008 = countryGwp.Y2008,
                        Y2009 = countryGwp.Y2009,
                        Y2010 = countryGwp.Y2010,
                        Y2011 = countryGwp.Y2011,
                        Y2012 = countryGwp.Y2012,
                        Y2013 = countryGwp.Y2013,
                        Y2014 = countryGwp.Y2014,
                        Y2015 = countryGwp.Y2015,
                    });
            }
        }

        public static CountryGwp FromCsv(string csvLine, int counter)
        {
            string[] values = csvLine.Split(',');
            
            CountryGwp countryGwp = new CountryGwp();

            countryGwp.CountryGwpId = counter;
            countryGwp.Country = values[0];
            countryGwp.Lob = values[3];
            countryGwp.Y2008 = string.IsNullOrEmpty(values[12]) ? 0.0 :Convert.ToDouble(values[12]);
            countryGwp.Y2009 = string.IsNullOrEmpty(values[13]) ? 0.0 : Convert.ToDouble(values[13]);
            countryGwp.Y2010 = string.IsNullOrEmpty(values[14]) ? 0.0 : Convert.ToDouble(values[14]);
            countryGwp.Y2011 = string.IsNullOrEmpty(values[15]) ? 0.0 : Convert.ToDouble(values[15]);
            countryGwp.Y2012 = string.IsNullOrEmpty(values[16]) ? 0.0 : Convert.ToDouble(values[16]);
            countryGwp.Y2013 = string.IsNullOrEmpty(values[17]) ? 0.0 : Convert.ToDouble(values[17]);
            countryGwp.Y2014 = string.IsNullOrEmpty(values[18]) ? 0.0 : Convert.ToDouble(values[18]);
            countryGwp.Y2015 = string.IsNullOrEmpty(values[19]) ? 0.0 : Convert.ToDouble(values[19]);

            return countryGwp;
        }
    }
}
