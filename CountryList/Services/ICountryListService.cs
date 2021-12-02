using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryList.Services
{
    public interface ICountryListService
    {
        public Task<bool> CountryExistsAsync(string countryCode);

        public Task<List<string>> GetCountryListAsync(string countryCode);

    }
}
