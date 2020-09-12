using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountryGWP.Business.Layer.Services.Interfaces;
using CountryGWP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CountryGWP.Controllers
{
    [ApiController]
    [Route("server/api")]
    public class CountryGwpController : ControllerBase
    {
        private readonly ICountryGwpService _countryGwpService;

        public CountryGwpController(ICountryGwpService countryGwpService)
        {
            _countryGwpService = countryGwpService ??
                throw new ArgumentNullException(nameof(countryGwpService));
        }
        
        [HttpPost("gwp/avg")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Business.Layer.Models.CountryGwp),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> AverageGwpAsync([FromBody]CountryGwpRequest request)
        {
            var coutriesGwp = await _countryGwpService.AverageGwpAsync(request.Country,request.Lob);
           
            if(coutriesGwp == null)
            {
                return NotFound();
            }
            
            return Ok(coutriesGwp);
        }
    }
}
