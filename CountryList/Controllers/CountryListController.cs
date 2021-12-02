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

        /// <summary>
        /// Create country list controller
        /// </summary>
        /// <param name="countryListService"></param>
        public CountryListController(ICountryListService countryListService)
        {
            _countryListService = countryListService;
        }

        /// <summary>
        /// Get list of countries you need to travel through to get to destination.
        /// </summary>
        /// <param name="destinationCountryCode">Destination country code</param>
        /// <returns></returns>
        /// <response code="200">List of countries is being returned</response>
        /// <response code="404">Invalid destination country code.</response>
        [HttpGet("{destinationCountryCode}")]
        [ProducesResponseType(typeof(GetCountryListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCountryListAsync(string destinationCountryCode)
        {
            var exists = await _countryListService.CountryExistsAsync(destinationCountryCode);
            if (!exists)
            {
                return NotFound();
            }

            var path = await _countryListService.GetCountryListAsync(destinationCountryCode);
            return Ok(
                new GetCountryListResponse
                {
                    Destination = destinationCountryCode,
                    List = path
                }
            );
        }

        /// <summary>
        /// Get a number
        /// </summary>
        /// <returns>Ok response with a number</returns>
        /// <response code="200">A number</response>
        [HttpGet]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public ActionResult<int> GetNumber()
        {
            return Ok(0);
        }

    }
}
