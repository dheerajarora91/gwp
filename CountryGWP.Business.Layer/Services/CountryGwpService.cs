using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CountryGWP.Business.Layer.Models;
using CountryGWP.Business.Layer.Services.Interfaces;
using CountryGWP.DataAccess.Layer.Repositories.Interfaces;

namespace CountryGWP.Business.Layer.Services
{
    public class CountryGwpService : ICountryGwpService
    {
        private readonly ICountryGwpRepository _countryGwpRepository;
        private readonly IMapper _mapper;

        public CountryGwpService(ICountryGwpRepository countryGwpRepository, IMapper mapper)
        {
            _countryGwpRepository = countryGwpRepository ??
                throw new ArgumentNullException(nameof(countryGwpRepository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<CountryGwp>> AverageGwpAsync(string country, IEnumerable<string> lob)
        {
            try
            {
                var entities = await _countryGwpRepository.AverageGwpAsync(country, lob);
                return _mapper.Map<IEnumerable<CountryGwp>>(entities);
            }
            catch(Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<CountryGwp>> GetCountriesGwp()
        {
            try
            {
                var entities = await _countryGwpRepository.GetCountriesGwpAsync();
                return _mapper.Map<IEnumerable<CountryGwp>>(entities);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
