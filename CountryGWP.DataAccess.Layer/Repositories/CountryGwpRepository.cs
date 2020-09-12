using CountryGWP.DataAccess.Layer.Context;
using CountryGWP.DataAccess.Layer.Entities;
using CountryGWP.DataAccess.Layer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryGWP.DataAccess.Layer.Repositories
{
    public class CountryGwpRepository : ICountryGwpRepository, IDisposable
    {
        private CountryGwpDbContext _context;

        public CountryGwpRepository(CountryGwpDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<CountryGwp>> AverageGwpAsync(string country, IEnumerable<string> lob)
        {
            try
            {
                var countriesGwp = await _context.CountriesGwp
                     .Where(c => c.Country.Equals(country) && lob.Contains(c.Lob))
                     .Select(c => new CountryGwp
                     {
                         Country = c.Country,
                         Lob = c.Lob,
                         Average = (c.Y2008 + c.Y2009 + c.Y2010 + c.Y2011 + c.Y2012 + c.Y2013 + c.Y2014 + c.Y2015) / 8
                     })
                     .ToListAsync();

                return countriesGwp;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<CountryGwp>> GetCountriesGwpAsync()
        {
            try
            {
                return await _context.CountriesGwp.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
    }
}
