using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GetWatch.Services.Movies
{
    public class MovieVideosService
    {
        public async Task<string> GetMovieVideosAsync(int movieId, HttpClient httpClient)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.themoviedb.org/3/movie/{movieId}/videos?language=en-US"),
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
                var result = JsonConvert.DeserializeObject<MovieVideos>(body);
              
                    var video = result?.Results
                        ?.FirstOrDefault(x =>
                            x.Site == "YouTube" &&
                            x.Type == "Trailer");

        if (video != null && !string.IsNullOrEmpty(video.Key))
        {
            return $"https://www.youtube.com/embed/{video.Key}";
        }
        else
        {
            return string.Empty;
        }

            }
        }
        
    }
}