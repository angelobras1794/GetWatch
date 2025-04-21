using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Newtonsoft.Json;
using GetWatch.Interfaces.Movies;
using GetWatch.Services.Movies;

namespace GetWatch.Interfaces.Movies
{
    public interface ISingleMovieFetchStrategy
    {
         Task<PopularApiMovie> FetchMovieAsync(HttpClient httpClient);
    }
}