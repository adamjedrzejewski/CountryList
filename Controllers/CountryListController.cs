using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryList.Controllers
{
    [Route("/")]
    [ApiController]
    public class CountryListController : ControllerBase
    {
        [HttpGet("{countryCode}")]
        public async Task<IActionResult> GetCountryListAsync(string countryCode)
        {
            return Ok(countryCode);
        }
    }
}
