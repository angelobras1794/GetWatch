using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Newtonsoft.Json;
using GetWatch.Interfaces.Movies;

namespace GetWatch.Services.Movies.Strategy
{
    public class SingleMovieFetcher
    {
        private readonly ISingleMovieFetchStrategy _strategy;

        public SingleMovieFetcher(ISingleMovieFetchStrategy strategy)
        {
            _strategy = strategy;
        }

        public async Task<PopularApiMovie> FetchMovieAsync(HttpClient httpClient)
        {
            return await _strategy.FetchMovieAsync(httpClient);
        }
        
    }
}