using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GetWatch.Services.Movies
{
    public class PopularApiMovie
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

       [JsonProperty("poster_path")]
        public string? PosterPath { get; set; }

        [JsonProperty("release_date")]
        public string? ReleaseDate { get; set; }


    }
}