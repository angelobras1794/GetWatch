using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Newtonsoft.Json;
using GetWatch.Interfaces.Movies;

namespace GetWatch.Services.Movies.Strategy
{
    public class GenreMovieFetchStrategy: IMovieFetchStrategy
    {
        private readonly int _genreId;
        public GenreMovieFetchStrategy(int genreId){
            _genreId = genreId;
        }
         public async Task<List<PopularApiMovie>> FetchMoviesAsync(HttpClient httpClient)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.themoviedb.org/3/discover/movie?with_genres={_genre_id}&language=en-US&sort_by=popularity.desc&include_adult=false&include_video=false&page=1"),
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

                // Deserialize the response into a list of ApiMovie objects
                var result = JsonConvert.DeserializeObject<TmdbResponse>(body);
                return result?.Results ?? new List<PopularApiMovie>();
            }
        }
        
    }
}