using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Xunit;
using CountryList.Countries;
using System.IO;
using CountryList.Services;
using System.Threading.Tasks;

namespace CountryListTest.Services
{
    public class CountryListServiceTest
    {
        private CountryListService _countriesPathFinder;

        public CountryListServiceTest()
        {
            var countriesConfig = JsonSerializer.Deserialize<CountriesConfiguration>(
                File.ReadAllText("countries.json")
            );
            var countriesGraph = CountriesGraph.CreateCountriesGraph(countriesConfig);
            _countriesPathFinder = CountryListService.CreateCountryListSerivce(countriesGraph, countriesConfig.StartingPoint);

        }

        [Fact]
        public async Task CheckCountriesConfig()
        {
            Assert.True(await _countriesPathFinder.CountryExistsAsync("CAN"), "Canada is in config");
            Assert.True(await _countriesPathFinder.CountryExistsAsync("USA"), "United States is in config");
            Assert.True(await _countriesPathFinder.CountryExistsAsync("MEX"), "Belize is in config");
            Assert.True(await _countriesPathFinder.CountryExistsAsync("BLZ"), "Guatemala is in config");
            Assert.True(await _countriesPathFinder.CountryExistsAsync("GTM"), "Guatemala is in config");
            Assert.True(await _countriesPathFinder.CountryExistsAsync("SLV"), "El Salvador is in config");
            Assert.True(await _countriesPathFinder.CountryExistsAsync("HND"), "Honduras is in config");
            Assert.True(await _countriesPathFinder.CountryExistsAsync("NIC"), "Nicaragua is in config");
            Assert.True(await _countriesPathFinder.CountryExistsAsync("CRI"), "Costa Rica is in config");
            Assert.True(await _countriesPathFinder.CountryExistsAsync("PAN"), "Panama is in config");
        }

        [Fact]
        public async Task CheckShortestPaths()
        {
            Assert.Equal(
                new List<string> { "USA", "CAN"},
                await _countriesPathFinder.GetCountryListAsync("CAN")
            );
            Assert.Equal(
                new List<string> { "USA" },
                await _countriesPathFinder.GetCountryListAsync("USA")
            );
            Assert.Equal(
                new List<string> { "USA", "MEX" },
                await _countriesPathFinder.GetCountryListAsync("MEX")
            );
            Assert.Equal(
                new List<string> { "USA", "MEX", "BLZ" },
                await _countriesPathFinder.GetCountryListAsync("BLZ")
            );
            Assert.Equal(
                new List<string> { "USA", "MEX", "GTM" },
                await _countriesPathFinder.GetCountryListAsync("GTM")
            );
            Assert.Equal(
                new List<string> { "USA", "MEX", "GTM", "SLV" },
                await _countriesPathFinder.GetCountryListAsync("SLV")
            );
            Assert.Equal(
                new List<string> { "USA", "MEX", "GTM", "HND" },
                await _countriesPathFinder.GetCountryListAsync("HND")
            );
            Assert.Equal(
                new List<string> { "USA", "MEX", "GTM", "HND", "NIC" },
                await _countriesPathFinder.GetCountryListAsync("NIC")
            );
            Assert.Equal(
                new List<string> { "USA", "MEX", "GTM", "HND", "NIC", "CRI" },
                await _countriesPathFinder.GetCountryListAsync("CRI")
            );
            Assert.Equal(
                new List<string> { "USA", "MEX", "GTM", "HND", "NIC", "CRI", "PAN"},
                await _countriesPathFinder.GetCountryListAsync("PAN")
            );
        }
    }
}
