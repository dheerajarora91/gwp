using CountryGWP.DataAccess.Layer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CountryGWP.DataAccess.Layer.Repositories.Interfaces
{
    public interface ICountryGwpRepository
    {
        Task<IEnumerable<CountryGwp>> GetCountriesGwpAsync();
        Task<IEnumerable<CountryGwp>> AverageGwpAsync(string country, IEnumerable<string> lob);
    }
}
