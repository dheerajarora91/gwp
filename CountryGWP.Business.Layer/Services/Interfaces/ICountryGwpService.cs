using CountryGWP.Business.Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CountryGWP.Business.Layer.Services.Interfaces
{
    public interface ICountryGwpService
    {
        Task<IEnumerable<CountryGwp>> AverageGwpAsync(string country, IEnumerable<string> lob);

        Task<IEnumerable<CountryGwp>> GetCountriesGwp();
    }
}
