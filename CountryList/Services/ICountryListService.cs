using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryList.Services
{
    public interface ICountryListService
    {
        /// <summary>
        /// Check if country exists
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns>true if exists false otherwise</returns>
        public Task<bool> CountryExistsAsync(string countryCode);

        /// <summary>
        /// Get list of countries you need to travel throught to get to destination.
        /// </summary>
        /// <param name="destinationCountryCode"></param>
        /// <returns>List of country codes</returns>
        public Task<List<string>> GetCountryListAsync(string destinationCountryCode);

    }
}
