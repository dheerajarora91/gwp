using CountryGWP.Business.Layer.Services.Interfaces;
using CountryGWP.Controllers;
using CountryGWP.DataAccess.Layer.Entities;
using CountryGWP.DataAccess.Layer.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CountryGWP.UnitTest
{
    public class EndToEndTest
    {
        [Theory]
        [InlineData("ae",new string[] { "transport" })]
        public async void AverageGwp(string country, IEnumerable<string> lob)
        {
            var repository = Substitute.For<ICountryGwpRepository>();
            repository.AverageGwpAsync(country, lob).Returns(Task.FromResult(FakeData()));

            var service = Substitute.For<ICountryGwpService>();

            var controller = new CountryGwpController(service);

            var result = await controller.AverageGwpAsync(new Models.CountryGwpRequest() { Country = country, Lob = lob });

            var statusCode = (result as OkObjectResult).StatusCode;

            Assert.Equal(200, statusCode);
        }

        private IEnumerable<CountryGwp> FakeData()
        {
            var list = new List<CountryGwp>()
            {
                new CountryGwp()
                {
                    Country = "ae",
                    Lob = "transport",
                    Average = 20.0
                }
            };

            return list;
        }
    }
}
