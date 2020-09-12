using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountryGWP.Business.Layer.Services.Interfaces;
using CountryGWP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CountryGWP.Controllers
{
    [ApiController]
    [Route("server/api")]
    public class CountryGwp : ControllerBase
    {
        private readonly ICountryGwpService _countryGwpService;

        public CountryGwp(ICountryGwpService countryGwpService)
        {
            _countryGwpService = countryGwpService ??
                throw new ArgumentNullException(nameof(countryGwpService));
        }

        [HttpPost("gwp/avg")]
        public async Task<IActionResult> AverageGwp(CountryGwpRequest request)
        {
            var coutriesGwp = await _countryGwpService.AverageGwpAsync(request.Country,request.Lob);
           
            if(coutriesGwp == null)
            {
                return NotFound();
            }
            
            return Ok(coutriesGwp);
        }

        [HttpGet]
        public async Task<IActionResult> GetCountriesGwp()
        {
            var countiresGwp = await _countryGwpService.GetCountriesGwp();
            return Ok(countiresGwp);
        }
    }
}
