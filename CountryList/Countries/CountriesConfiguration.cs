using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CountryList.Countries
{
    public class CountriesConfiguration
    {
        [JsonPropertyName("startingPoint")]
        public string StartingPoint { get; set; }

        [JsonPropertyName("countries")]
        public List<CountryConfigurationDescription> Countries { get; set; }
    }
}
