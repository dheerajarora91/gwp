using CountryGWP.Business.Layer.Services.Interfaces;
using CountryGWP.DataAccess.Layer.Entities;
using CountryGWP.DataAccess.Layer.Repositories.Interfaces;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CountryGWP.UnitTest.ServiceTests
{
    public class CountryGwpServiceTest
    {
        private readonly ICountryGwpService _countryGwpService;
        private readonly ICountryGwpRepository _countryGwpRepository;
        private readonly IEnumerable<CountryGwp> _countryGwplist;
        public CountryGwpServiceTest()
        {
            _countryGwplist = new List<CountryGwp>()
            {
                new CountryGwp
                {
                    Country = "test",
                    Lob = "test",
                    Y2008 = 20.0,
                    Y2009 = 20.0,
                    Y2010 = 20.0,
                    Y2011 = 20.0,
                    Y2012 = 20.0,
                    Y2013 = 20.0,
                    Y2014 = 20.0,
                    Y2015 = 20.0,
                },
                new CountryGwp
                {
                    Country = "test1",
                    Lob = "test1",
                    Y2008 = 20.0,
                    Y2009 = 20.0,
                    Y2010 = 20.0,
                    Y2011 = 20.0,
                    Y2012 = 20.0,
                    Y2013 = 20.0,
                    Y2014 = 20.0,
                    Y2015 = 20.0,
                },
            };

            _countryGwpRepository = Substitute.For<ICountryGwpRepository>();
               
            _countryGwpService = Substitute.For<ICountryGwpService>();
        }

        [Theory]
        [InlineData("test",new string[] { "test" })]
        public async void AverageGwpValid(string country, IEnumerable<string> lob)
        {
            _countryGwpRepository.AverageGwpAsync(Arg.Any<string>(), Arg.Any<IEnumerable<string>>()).Returns(Task.FromResult(_countryGwplist));

            var result = await _countryGwpService.AverageGwpAsync(country, lob);

            Assert.NotNull(result);
        }
    }
}
