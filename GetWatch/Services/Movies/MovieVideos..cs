using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GetWatch.Services.Movies
{
        public class MovieVideos
        {
            [JsonProperty("id")]
            public int? Id { get; set; }

            [JsonProperty("results")]
            public List<MovieVideo> Results { get; set; } = new List<MovieVideo>();
        }

    public class MovieVideo
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("key")]
        public string? Key { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("site")]
        public string? Site { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("official")]
        public bool Official { get; set; }
        }

        

    
}