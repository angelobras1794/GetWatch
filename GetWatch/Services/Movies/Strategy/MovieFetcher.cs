using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.Movies;

namespace GetWatch.Services.Movies.Strategy
{
    public class MovieFetcher
{
    private readonly IMovieFetchStrategy _strategy;

    public MovieFetcher(IMovieFetchStrategy strategy)
    {
        _strategy = strategy;
    }

    public async Task<List<PopularApiMovie>> FetchMoviesAsync(HttpClient httpClient)
    {
        return await _strategy.FetchMoviesAsync(httpClient);
    }
}
}