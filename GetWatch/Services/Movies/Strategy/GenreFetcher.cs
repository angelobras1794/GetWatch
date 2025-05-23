using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.Movies;
using Newtonsoft.Json;

namespace GetWatch.Services.Movies.Strategy
{
    public class GenreFetcher
    {
         public async Task<List<Genre>> FetchGenresAsync(HttpClient httpClient)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.themoviedb.org/3/genre/movie/list?language=en"),
                Headers =
                {
                    { "accept", "application/json" },
                    { "Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJhYjAyOTI1YmUxNDIyMjYzMjZhZWYzZmNhYjliYjVkMCIsIm5iZiI6MTc0NTEwMDMwNS4zMzcsInN1YiI6IjY4MDQxZTExMDMzNDRhZWU3MDg5YWYwNCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.HZBMqlW0ES1oqXZ6LAoEtaomBNTxl3awG5aUTqowh1w" },
                },
            };

            using (var response = await httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                
                var result = JsonConvert.DeserializeObject<TmdbGenresResponse>(body);
                return result?.Genres ?? new List<Genre>();
            }
        }
        
    }
}