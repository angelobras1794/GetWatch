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

        [JsonProperty("genres")]
        public List<Genre> Genres { get; set; } = new List<Genre>();

        [JsonProperty("overview")]
        public string? Overview { get; set; } = string.Empty;

        [JsonProperty("credits")]
        public Credits? Credits { get; set; } = new Credits();

        [JsonProperty("popularity")]
        public double? Popularity { get; set; }

        [JsonProperty("runtime")]
        public int? Runtime { get; set; }


    }
}