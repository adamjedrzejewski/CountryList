using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CountryList.Models
{
    public class GetCountryListResponse
    {
        [JsonPropertyName("destination")]
        public string Destination { get; set; }
        
        [JsonPropertyName("list")]
        public List<string> List { get; set; }
    }
}
