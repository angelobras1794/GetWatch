using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GetWatch.Interfaces.Movies;
using Newtonsoft.Json;

namespace GetWatch.Services.Movies
{
    public class MovieRepository : IMovieRepository
    {
        private readonly HttpClient _httpClient;

        public MovieRepository()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<PopularApiMovie>> GetPopularMoviesAsync()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.themoviedb.org/3/movie/popular?language=en-US&page=1"),
                Headers =
                {
                    { "accept", "application/json" },
                    { "Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJhYjAyOTI1YmUxNDIyMjYzMjZhZWYzZmNhYjliYjVkMCIsIm5iZiI6MTc0NTEwMDMwNS4zMzcsInN1YiI6IjY4MDQxZTExMDMzNDRhZWU3MDg5YWYwNCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.HZBMqlW0ES1oqXZ6LAoEtaomBNTxl3awG5aUTqowh1w" },
                },
            };

            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                // Deserialize the response into a list of ApiMovie objects
                var result = JsonConvert.DeserializeObject<TmdbResponse>(body);
                return result?.Results ?? new List<PopularApiMovie>();
            }
        }

        //fetch movie by id
        public async Task<PopularApiMovie> GetMovieByIdAsync(int id)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.themoviedb.org/3/movie/{id}?language=en-US&append_to_response=credits"),
                Headers =
                {
                    { "accept", "application/json" },
                    { "Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJhYjAyOTI1YmUxNDIyMjYzMjZhZWYzZmNhYjliYjVkMCIsIm5iZiI6MTc0NTEwMDMwNS4zMzcsInN1YiI6IjY4MDQxZTExMDMzNDRhZWU3MDg5YWYwNCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.HZBMqlW0ES1oqXZ6LAoEtaomBNTxl3awG5aUTqowh1w" },
                },
            };

            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);

                // Deserialize the response into a ApiMovie object
                var result = JsonConvert.DeserializeObject<PopularApiMovie>(body);
                return result ?? new PopularApiMovie();
            }
        }

    }


}