using CountryList.Countries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryList.Services
{
    public class CountryListService : ICountryListService
    {
        private readonly Dictionary<string, List<string>> _shortestPaths;

        private CountryListService(Dictionary<string, List<string>> shortestPathsDictionary)
        {
            _shortestPaths = shortestPathsDictionary;
        }

        public Task<bool> CountryExistsAsync(string countryCode)
            => Task.FromResult(_shortestPaths.ContainsKey(countryCode));

        public Task<List<string>> GetCountryListAsync(string countryCode)
            => Task.FromResult(_shortestPaths[countryCode]);

        public static CountryListService CreateCountriesPathFinder(CountriesGraph countriesGraph, string startingPointCode)
        {
            if (!countriesGraph.CountryExists(startingPointCode))
            {
                throw new ArgumentException($"Country {startingPointCode} is not in country graph.");
            }

            var shortestPathsDictionary = GetShortestPathsDictionary(countriesGraph, startingPointCode);

            return new CountryListService(shortestPathsDictionary);
        }

        private static Dictionary<string, List<string>> GetShortestPathsDictionary(CountriesGraph countriesGraph, string startingPointCode)
        {
            var previous = new Dictionary<string, string>();

            var queue = new Queue<string>();
            queue.Enqueue(startingPointCode);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                foreach (var neighbor in countriesGraph.AdjacencyList[vertex])
                {
                    if (previous.ContainsKey(neighbor))
                        continue;

                    previous[neighbor] = vertex;
                    queue.Enqueue(neighbor);
                }
            }

            var paths = new Dictionary<string, List<string>>();

            foreach (var vertex in countriesGraph.AdjacencyList.Keys)
            {
                var path = new List<string> { };

                var current = vertex;
                while (!current.Equals(startingPointCode))
                {
                    path.Add(current);
                    current = previous[current];
                };

                path.Add(startingPointCode);
                path.Reverse();

                paths[vertex] = path;
            }

            return paths;
        }
    }
}
