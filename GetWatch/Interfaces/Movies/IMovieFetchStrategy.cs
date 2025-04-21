using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Services.Movies;

namespace GetWatch.Interfaces.Movies
{
    public interface IMovieFetchStrategy
    {
        Task<List<PopularApiMovie>> FetchMoviesAsync(HttpClient httpClient);
    }
}