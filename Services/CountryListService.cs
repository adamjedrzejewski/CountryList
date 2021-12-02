using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryList.Services
{
    public class CountryListService : ICountryListService
    {
        public Task<bool> CountryExistsAsync(string countryCode)
        {
            return Task.FromResult(true);
        }

        public Task<List<string>> GetCountryListAsync(string countryCode)
        {
            return Task.FromResult(new List<string> { countryCode } );
        }
    }
}
