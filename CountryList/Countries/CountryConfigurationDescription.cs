using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CountryList.Countries
{
    public class CountryConfigurationDescription
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
        
        [JsonPropertyName("neighbours")]
        public List<string> Neighbours { get; set; }
    }
}
