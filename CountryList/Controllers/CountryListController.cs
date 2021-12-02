using CountryList.Models;
using CountryList.Countries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountryList.Services;

namespace CountryList.Controllers
{
    [Route("/")]
    [ApiController]
    public class CountryListController : ControllerBase
    {
        private readonly ICountryListService _countryListService;

        public CountryListController(ICountryListService countryListService)
        {
            _countryListService = countryListService;
        }

        [HttpGet("{countryCode}")]
        public async Task<IActionResult> GetCountryListAsync(string countryCode)
        {
            var exists = await _countryListService.CountryExistsAsync(countryCode);
            if (!exists)
            {
                return NotFound();
            }

            var path = await _countryListService.GetCountryListAsync(countryCode);

            return Ok(
                new GetCountryListResponse
                {
                    Destination = countryCode,
                    List = path
                }
            );
        }
    }
}
