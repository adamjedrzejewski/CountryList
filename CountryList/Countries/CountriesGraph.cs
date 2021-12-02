﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryList.Countries
{
    public class CountriesGraph
    {
        public Dictionary<string, HashSet<string>> AdjacencyList { get; } = new Dictionary<string, HashSet<string>>();

        private CountriesGraph() { }
        public bool CountryExists(string countryCode)
            => AdjacencyList.ContainsKey(countryCode);

        public static CountriesGraph CreateCountriesGraph(CountriesConfiguration configuration)
        {
            var graph = new CountriesGraph();

            graph.CreateVertices(configuration);
            graph.CreateEdges(configuration);

            return graph;
        }

        private void CreateEdges(CountriesConfiguration configuration)
        {
            foreach (var country in configuration.Countries)
            {
                var countryCode = country.Code;
                foreach (var neighbourCode in country.Neighbours)
                {
                    var countryCodeExists = AdjacencyList.ContainsKey(countryCode);
                    var neighbourCodeExists = AdjacencyList.ContainsKey(neighbourCode);
                    if (countryCodeExists && neighbourCodeExists)
                    {
                        AdjacencyList[countryCode].Add(neighbourCode);
                        AdjacencyList[neighbourCode].Add(countryCode);
                    }
                    else if (!countryCodeExists && !neighbourCodeExists)
                    {
                        throw new ArgumentException($"Invalid border: {countryCode} and {neighbourCode} doesn't exist.");
                    }
                    else if (!countryCodeExists)
                    {
                        throw new ArgumentException($"Invalid border: {countryCode} doesn't exist.");
                    }
                    else if (!neighbourCodeExists)
                    {
                        throw new ArgumentException($"Invalid border: {neighbourCode} doesn't exist.");
                    }
                }
            }
        }

        private void CreateVertices(CountriesConfiguration configuration)
        {
            foreach (var country in configuration.Countries)
            {
                AdjacencyList[country.Code] = new HashSet<string>();
            }
        }

    }
}
